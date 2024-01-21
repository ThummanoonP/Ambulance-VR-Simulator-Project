using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Destroy : MonoBehaviour //คลาสการ ลบ Item เมื่อถูกปล่อยนอก Socket
{
    [SerializeField] GameObject EventMessage;

    private TextMeshProUGUI TextEvent;
    private OnSocket onSocket;

    void Awake()
    {
        onSocket = this.GetComponent<OnSocket>();
        TextEvent = EventMessage.GetComponent<TextMeshProUGUI>();
    }

    public void DestroyObject()
    {
        if (onSocket.onSocketStatus.GetStatus() == false)
        {
            ShowMessage();
            Invoke("HidenMessage", .2f);
            Destroy(this.gameObject, 0.25f);
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
