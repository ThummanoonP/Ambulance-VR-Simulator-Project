using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSocketStatus : MonoBehaviour//����ʶҹ� Socket
{
    private bool Status = false;
    private bool StatusForTurnOnMonitor = false;

    public void SetStatusTrue() //��ʶҹ� ����� Socket �� true
    {
        Status = true;
    }

    public void SetStatusFalse() //��ʶҹ� ����� Socket �� False
    {
        Status = false;
    }

    public void SetStatusForTurnOnMonitorTrue() //��ʶҹ������Դ monitor
    {
        StatusForTurnOnMonitor = true;
    }

    public void SetStatusForTurnOnMonitorFalse() //��ʶҹ����ͻԴ monitor
    {
        StatusForTurnOnMonitor = false;
    }

    public bool GetStatus() //��ʶҹ� Socket
    {
        return Status;
    }

    public bool GetStatusForTurnOnMonitor() //��ʶҹС�ûԴ Monitor
    {
        return StatusForTurnOnMonitor;
    }

}
