using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSocketStatus : MonoBehaviour//คลาสสถานะ Socket
{
    private bool Status = false;
    private bool StatusForTurnOnMonitor = false;

    public void SetStatusTrue() //เซ็ตสถานะ อยู่ใน Socket เป็น true
    {
        Status = true;
    }

    public void SetStatusFalse() //เซ็ตสถานะ อยู่ใน Socket เป็น False
    {
        Status = false;
    }

    public void SetStatusForTurnOnMonitorTrue() //เซ็ตสถานะเพื่อเปิด monitor
    {
        StatusForTurnOnMonitor = true;
    }

    public void SetStatusForTurnOnMonitorFalse() //เซ็ตสถานะเพื่อปิด monitor
    {
        StatusForTurnOnMonitor = false;
    }

    public bool GetStatus() //เช็คสถานะ Socket
    {
        return Status;
    }

    public bool GetStatusForTurnOnMonitor() //เช็คสถานะการปิด Monitor
    {
        return StatusForTurnOnMonitor;
    }

}
