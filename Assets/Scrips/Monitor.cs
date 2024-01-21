using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour//คลาสการทำงานของ Monitor
{
    [SerializeField] public GameObject PatientBoxSocket;//ตำแหน่งเซ็นเซอร์ที่นิ้ว
    [SerializeField] public GameObject MonitorSocket;//ตำแหน่ง Monitor
    [SerializeField] public GameObject CorPatchRightSocket;//ตำแหน่งเซ็นเซอร์อกขวา
    [SerializeField] public GameObject CorPatchLeftSocket;//ตำแหน่งเซ็นเซอร์เอวซ้าย
    [SerializeField] public GameObject SPO2Socket;//ตำแหน่งกล่องควบคุม

    private OnSocketStatus PatientBoxStatus;
    private OnSocketStatus MonitorStatus;
    private OnSocketStatus CorPatchRightStatus;
    private OnSocketStatus CorPatchLeftStatus;
    private OnSocketStatus SPO2Status;
    private GameObject IdleMonitor;//UI เปิดหน้าจอแบบ Idle
    private GameObject WorkingMonitor;//UI เปิดหน้าจอแบบมีการทำงาน

    void Awake()
    {
        PatientBoxStatus = PatientBoxSocket.GetComponent<OnSocketStatus>();
        MonitorStatus = MonitorSocket.GetComponent<OnSocketStatus>();
        CorPatchRightStatus = CorPatchRightSocket.GetComponent<OnSocketStatus>();
        CorPatchLeftStatus = CorPatchLeftSocket.GetComponent<OnSocketStatus>();
        SPO2Status = SPO2Socket.GetComponent<OnSocketStatus>();
        IdleMonitor = this.transform.Find("Idle Monitor").gameObject;
        WorkingMonitor = this.transform.Find("Working Monitor").gameObject;
    }

    void Update()
    {
        TurnOnMonitor();
    }

    private void TurnOnMonitor() 
    {
        if (MonitorStatus.GetStatusForTurnOnMonitor() == true && PatientBoxStatus.GetStatusForTurnOnMonitor() == true)
        {
            if (CorPatchRightStatus.GetStatusForTurnOnMonitor() == true && CorPatchLeftStatus.GetStatusForTurnOnMonitor() == true && SPO2Status.GetStatusForTurnOnMonitor() == true)
            {
                IdleMonitor.SetActive(false);
                WorkingMonitor.SetActive(true);
            }
            else
            {
                IdleMonitor.SetActive(true);
                WorkingMonitor.SetActive(false);
            }
        }
        else
        {
            IdleMonitor.SetActive(false);
            WorkingMonitor.SetActive(false);
        }
    }
}
