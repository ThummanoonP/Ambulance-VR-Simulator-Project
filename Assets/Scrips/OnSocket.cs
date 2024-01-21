using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSocket : MonoBehaviour //คลาสสำหรับเช็ค Item ว่าอยู่ใน Socket หรือไม่
{
    [SerializeField] public GameObject KeySocket;

    public OnSocketStatus onSocketStatus;//สถานะ Socket

    private void OnTriggerEnter(Collider other)//Item อยู่ใน Socket
    {
        onSocketStatus = KeySocket.GetComponent<OnSocketStatus>();

        if (other.gameObject.name == KeySocket.name)
        {
            onSocketStatus.SetStatusTrue(); //เซ็ตสถานะ อยู่ใน Socket เป็น true
        }   
    }

    private void OnTriggerExit(Collider other)//Item ไม่อยู่ใน Socket
    {
        onSocketStatus = KeySocket.GetComponent<OnSocketStatus>();

        if (other.gameObject.name == KeySocket.name)
        {
            onSocketStatus.SetStatusFalse();//เซ็ตสถานะ อยู่ใน Socket เป็น False
        }
    }
}
