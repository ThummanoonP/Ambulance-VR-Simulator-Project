using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reposition : MonoBehaviour // �������¡ Item 仵��˹��������
{
    [SerializeField] GameObject EventMessage; //��ͤ����͡��� Item �١���¡��Ѻ

    private Vector3 rootPosition;
    private Quaternion rootRotation;
    private Vector3 Position;
    private Quaternion Rotation;
    private OnSocket onSocket;
    private TextMeshProUGUI TextEvent;

    void Awake()
    {
        RootPosition();
        onSocket = this.GetComponent<OnSocket>();
        TextEvent = EventMessage.GetComponent<TextMeshProUGUI>();
    }

    public void RootPosition() // ��˹����˹��������
    {
        rootPosition = this.gameObject.transform.position;
        rootRotation = this.gameObject.transform.rotation;
    }

    public void SplitPosition() // ��˹����˹����������ѧ Split ��Ѻ�ػ�ó�����ͧ corpuls3
    {
        if (onSocket.onSocketStatus.GetStatus() == false)
        {
            Position = this.gameObject.transform.position;
            Rotation = this.gameObject.transform.rotation;
        }
    }

    public void SetRoot() //���¡��Ѻ���˹��������
    {
        if (onSocket.onSocketStatus.GetStatus() == false)
        {
            this.transform.position = rootPosition;
            this.transform.rotation = rootRotation;
            ShowMessage();
            Invoke("HidenMessage", .50f);
        }
    }

    public void SetSplit() //���¡��Ѻ���˹����������ѧ Split ��Ѻ�ػ�ó�����ͧ corpuls3
    {
        if (onSocket.onSocketStatus.GetStatus() == false)
        {
            this.transform.position = Position;
            this.transform.rotation = Rotation;
            ShowMessage();
            Invoke("HidenMessage", .50f);
        }
    }

    private void ShowMessage() 
    {
        TextEvent.enabled = true;
    }

    private void HidenMessage()
    {
        TextEvent.enabled = false;
    }
}
