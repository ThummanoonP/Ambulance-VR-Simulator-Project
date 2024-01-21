using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour//���ʡ�÷ӧҹ�ͧ Monitor
{
    [SerializeField] public GameObject PatientBoxSocket;//���˹������������
    [SerializeField] public GameObject MonitorSocket;//���˹� Monitor
    [SerializeField] public GameObject CorPatchRightSocket;//���˹�������͡���
    [SerializeField] public GameObject CorPatchLeftSocket;//���˹���������ǫ���
    [SerializeField] public GameObject SPO2Socket;//���˹觡��ͧ�Ǻ���

    private OnSocketStatus PatientBoxStatus;
    private OnSocketStatus MonitorStatus;
    private OnSocketStatus CorPatchRightStatus;
    private OnSocketStatus CorPatchLeftStatus;
    private OnSocketStatus SPO2Status;
    private GameObject IdleMonitor;//UI �Դ˹�Ҩ�Ẻ Idle
    private GameObject WorkingMonitor;//UI �Դ˹�Ҩ�Ẻ�ա�÷ӧҹ

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
