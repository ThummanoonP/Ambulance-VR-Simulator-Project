/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using System;
using System.Collections.Generic;
using UnityEngine;

using Pulse;
using Pulse.CDM;

// This is very similar to PulseEngineDriver, only we will use a SEScenario to hold data requests and engine initialization data
[ExecuteInEditMode]
public class PulseEngineScenarioDriver : PulseDataSource
{
  public TextAsset scenarioJson;  // Scenario file to use 

  [Range(0.02f, 2.0f)]
  public double sampleRate = 0.02;    // How often you wish to get data from Pulse

  [NonSerialized]
  public PulseEngine engine;          // Pulse engine to drive
  protected SEScenario scenario;         // A scenario file

  [NonSerialized]
  public bool pauseUpdate = false;    // Do not advance any time

  protected double pulseTime;
  protected double pulseTimeStep = 0.02;
  protected double pulseSampleTime;
  protected bool   pullAllData = true;

  // We need to define data requests we want available in the Editor up front
  // These data requests are needed to hook up the vitals monitor
  // If you are not connecting data via the Editor (such as the vitals monitor), you don't need this
  // But we demonstrate this class in a scene with the vitals monitor
  // So we still need this list up front in order to hook up our vitals monitor components in the editor
  readonly List<SEDataRequest> vitals_monitor_data_requests = new List<SEDataRequest>
    {
        SEDataRequest.CreateECGDataRequest("Lead3ElectricPotential", ElectricPotentialUnit.mV),
        SEDataRequest.CreatePhysiologyDataRequest("HeartRate", FrequencyUnit.Per_min),
        SEDataRequest.CreatePhysiologyDataRequest("ArterialPressure", PressureUnit.mmHg),
        SEDataRequest.CreatePhysiologyDataRequest("MeanArterialPressure", PressureUnit.mmHg),
        SEDataRequest.CreatePhysiologyDataRequest("SystolicArterialPressure", PressureUnit.mmHg),
        SEDataRequest.CreatePhysiologyDataRequest("DiastolicArterialPressure", PressureUnit.mmHg),
        SEDataRequest.CreatePhysiologyDataRequest("OxygenSaturation"),
        SEDataRequest.CreatePhysiologyDataRequest("EndTidalCarbonDioxidePressure", PressureUnit.mmHg),
        SEDataRequest.CreatePhysiologyDataRequest("RespirationRate", FrequencyUnit.Per_min),
        SEDataRequest.CreatePhysiologyDataRequest("SkinTemperature", TemperatureUnit.C),
        SEDataRequest.CreateGasCompartmentDataRequest("Carina", "CarbonDioxide", "PartialPressure", PressureUnit.mmHg)
    };

  // Create a reference to a double[] that will contain the data returned from Pulse
  protected double[] data_values;
  protected List<Tuple<double,SEAction>> actions = new List<Tuple<double, SEAction>>();// The scenario actions and the time they need to be processed
  protected List<Tuple<double, SEAction>> activeActions = new List<Tuple<double, SEAction>>(); // List of actions we are processing and will remove from actions list

  // MARK: Monobehavior methods

  // Called when the inspector inputs are modified
  protected virtual void OnValidate()
  {
    // Round down to closest factor of 0.02. Need to use doubles due to
    // issues with floats multiplication (0.1 -> 0.0999999)
    sampleRate = Math.Round(sampleRate / 0.02) * 0.02;
  }

  // Called when application or editor opens
  protected virtual void Awake()
  {
    // Create our data container
    data = ScriptableObject.CreateInstance<PulseData>();

    // Store data field names
    // The rest of the data values are in order of the data_requests list
    data.fields = new StringList();// Field names
    data.timeStampList = new DoubleList(); // One or more datasets from the engine
    data.valuesTable = new List<DoubleList>();// The values received from the engine
    // The first field is always the simulation time in seconds
    data.fields.Add("Simulation Time(s)");
    data.valuesTable.Add(new DoubleList());
    foreach (var request in vitals_monitor_data_requests)
    {
      data.fields.Add(request.ToString().Replace("/", "\u2215"));
      data.valuesTable.Add(new DoubleList());
    }

    pullAllData = (sampleRate == pulseTimeStep);
  }

  // Called at the first frame when the component is enabled
  protected virtual void Start()
  {
    // Ensure we only read data if the application is playing
    // and we have a state file to initialize the engine with
    if (!Application.isPlaying)
      return;

    // Allocate PulseEngine with path to logs and needed data files
    string dateAndTimeVar = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
    string logFilePath = Application.persistentDataPath + "/" +
                                    gameObject.name +
                                    dateAndTimeVar + ".log";
    engine = new PulseEngine();
    engine.SetLogFilename(logFilePath);
    scenario = new SEScenario();

    // Initialize engine state from tje state file content
    if (scenarioJson != null)
    {
      if (!scenario.SerializeFromString(scenarioJson.text, eSerializationFormat.JSON))
        Debug.unityLogger.LogError("PulsePhysiologyEngine", "Unable to load scenario file " + scenarioJson);
    }
    else
    {
      // You do not have to use the Editor control if you don't want to,
      // You could simply specify a file on disk via use of the Streaming Assets folder
      string streamingScenarioFilename = Application.streamingAssetsPath + "/test_scenario.json";
      if (!scenario.SerializeFromFile(streamingScenarioFilename))
      {
        Debug.unityLogger.LogError("PulsePhysiologyEngine", "Unable to load scenario file "+ streamingScenarioFilename);
        return;
      }

      // You could also procedurally create a scenario
      scenario.SetName("Scenario");
      scenario.SetDescription("Simple Scenario to demonstraight building a scenario by the CDM API");
      scenario.GetPatientConfiguration().SetPatientFile("StandardMale.json");
      // Any extra data you want
      SEDataRequest dr = SEDataRequest.CreatePhysiologyDataRequest("BloodVolume", VolumeUnit.mL);
      scenario.GetDataRequestManager().GetDataRequests().Add(dr);
    }

    // !!! NOTE !!!
    // We need to combine any predefined editor data requests with data requests provided in the scenario
    // PLEASE ENSURE THERE ARE NO DUPLICATE DATA REQUESTS, THE ENGINE WILL NOT INITIALIZE
    // IT IS UNABLE TO PROPERLY ORDER THE PullData ARRAY WITH DUPLIATES
    // Push the scenario data requests to the back of the data container
    foreach (var request in scenario.GetDataRequestManager().GetDataRequests())
    {
      data.fields.Add(request.ToString().Replace("/", "\u2215"));
      data.valuesTable.Add(new DoubleList());
    }
    // The vitals monitor is expecting the vitals_monitor_data_requests to be in the beginning of data_values
    // So we need to push these to the front of the scenario data request list in reverse order
    // So data requests in the scenario file, or that you procedurally created will be AFTER
    // the vitals_monitor_data_requests in the data_values array
    for (int i=vitals_monitor_data_requests.Count; i>0; i--)
      scenario.GetDataRequestManager().GetDataRequests().Insert(0, vitals_monitor_data_requests[i-1]);
    // So the file/procedurally created requests will start at index vitals_monitor_data_requests.length
    // If you have duplicates, you are just going to get the same data multiple times

    if (scenario.HasEngineState())
    {
      // This code is assuming the scenario engine state file is relative to the application streaming path
      string state = Application.streamingAssetsPath+"/Data/states/"+scenario.GetEngineState();
      if (!engine.SerializeFromFile(state, scenario.GetDataRequestManager()))
      {
        Debug.unityLogger.LogError("PulsePhysiologyEngine", "Unable to load state file " + state);
        return;
      }
    }
    else if(scenario.HasPatientConfiguration())
    {
      if (!engine.InitializeEngine(scenario.GetPatientConfiguration(), scenario.GetDataRequestManager()))
      {
        Debug.unityLogger.LogError("PulsePhysiologyEngine", "Unable to initialize patient");
        return;
      }
    }
    else
    {
      Debug.unityLogger.LogError("PulsePhysiologyEngine", "Invalid Scenario provided");
      return;
    }

    // Go through the scenario actions and figure out what time they need to be processed
    double simTime_s = 0;
    foreach (SEAction a in scenario.GetActions())
    {
      if (a is SEAdvanceTime)
      {
        simTime_s += ((SEAdvanceTime)a).GetTime().GetValue(TimeUnit.s);
      }
      else
        actions.Add(new Tuple<double, SEAction>(simTime_s, a));
    }

    pulseTime = 0;
    pulseSampleTime = 0;
  }

  // Called before every frame
  protected virtual void Update()
  {
    // Ensure we only broadcast data if the application is playing
    // and there a valid pulse engine to simulate data from
    if (!Application.isPlaying || engine == null || pauseUpdate)
      return;

    double timeElapsed = Time.time - pulseTime;
    if (timeElapsed < pulseTimeStep)
      return;// Not running yet

    // Clear PulseData container
    if (!data.timeStampList.IsEmpty())
    {
      data.timeStampList.Clear();
      for (int j = 0; j < data.valuesTable.Count; ++j)
        data.valuesTable[j].Clear();
    }

    // Iterate over multiple time steps if needed
    int numberOfDataPointsNeeded = (int)Math.Floor(timeElapsed / pulseTimeStep);
    //if (numberOfDataPointsNeeded > 2)
    //  Debug.unityLogger.Log("Big Catchup "+ numberOfDataPointsNeeded + ", timeElapsed = " + timeElapsed);
    for (int i = 0; i < numberOfDataPointsNeeded; ++i)
    {
      // Check to see if we need to process any actions at this time
      foreach (var e in actions)
      {
        if (e.Item1 <= pulseTime)
        {
          activeActions.Add(e);
          if (!engine.ProcessAction(e.Item2))
            Debug.unityLogger.LogError("PulsePhysiologyEngine", "Could not process action " + e.Item2.ToString());
        }
      }
      foreach (var e in activeActions)
        actions.Remove(e);
      activeActions.Clear();

      // Increment pulse time
      pulseTime += pulseTimeStep;
      pulseSampleTime += pulseTimeStep;

      // Advance simulation by time step
      bool success = engine.AdvanceTime_s(pulseTimeStep);
      if (!success)
        continue;

      // Copy simulated data to data container (if its time)
      if (pullAllData || pulseSampleTime >= sampleRate)
      {
        pulseSampleTime = 0;
        data.timeStampList.Add(pulseTime);
        data_values = engine.PullData();
        for (int j = 0; j < data_values.Length; ++j)
          data.valuesTable[j].Add((float)data_values[j]);
      }
    }
  }

  protected virtual void OnApplicationQuit()
  {
    engine = null;
  }
}
