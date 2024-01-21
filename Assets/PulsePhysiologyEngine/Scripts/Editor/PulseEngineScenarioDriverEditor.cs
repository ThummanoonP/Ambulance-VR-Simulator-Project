/* Distributed under the Apache License, Version 2.0.
   See accompanying NOTICE file for details.*/

using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(PulseEngineScenarioDriver), true)]
public class PulseEngineScenarioDriverEditor : Editor
{
  SerializedProperty scenarioFileProperty;   // serialized scenario file

  void OnEnable()
  {
    scenarioFileProperty = serializedObject.FindProperty("scenarioJson");
  }

  public override void OnInspectorGUI()
  {
    // Ensure serialized properties are up to date with component
    serializedObject.Update();

    // Draw UI to select scenario file
    EditorGUILayout.PropertyField(scenarioFileProperty,
                                  new GUIContent("Scenario File"));
    var state = scenarioFileProperty.objectReferenceValue as TextAsset;
    if (state == null)
    {
      string message = "A scenario file to initialize the Pulse engine.";
      EditorGUILayout.HelpBox(message, MessageType.Warning);
      return;
    }

    // Show the default inspector property editor without the script field
    DrawPropertiesExcluding(serializedObject, "m_Script", "scenarioJson");

    // Apply modifications back to the component
    serializedObject.ApplyModifiedProperties();
  }
}
