using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayBVMPumpAnimation : MonoBehaviour // คลาสการกดปั้มลมเครื่อง BVM
{
    [SerializeField] InputActionReference LeftTrigger;
    [SerializeField] InputActionReference RightTrigger;
    [SerializeField] GameObject PumpMessage;


    private OnSocket onSocket;
    private Animator ObjectAnimator;
    private AudioSource sound;
    private TextMeshProUGUI TextPump;
    bool L_Clash = false;
    bool R_Clash = false;

    private void Awake()
    {
        onSocket = this.GetComponent<OnSocket>();
        ObjectAnimator = this.GetComponent<Animator>();
        sound = this.GetComponent<AudioSource>();
        TextPump = PumpMessage.GetComponent<TextMeshProUGUI>();
        LeftTrigger.action.performed += LeftHandTriggerPress;
        RightTrigger.action.performed += RightHandTriggerPress;
        LeftTrigger.action.canceled += LeftHandTriggerCancle;
        RightTrigger.action.canceled += RightHandTriggerCancle;

    }

    private void OnTriggerEnter(Collider other) // เช็คว่ามืออยู่ในตำแหน่งปั้มลม
    {
        if (onSocket.onSocketStatus.GetStatus() == true) 
        {
            if (other.gameObject.name == "Right Controller")
            {
                R_Clash = true;
                TextPump.enabled = true; // แสดงข้อความให้กดปุ่มปั้ม
            }
            else if (other.gameObject.name == "Left Controller")
            {
                L_Clash = true;
                TextPump.enabled = true; // แสดงข้อความให้กดปุ่มปั้ม
            }
        }
        
    }

    private void OnTriggerExit(Collider other) // เช็คว่ามือออกจากตำแหน่งปั้มลม
    {
        if (onSocket.onSocketStatus.GetStatus() == true)
        {
            if (other.gameObject.name == "Right Controller")
            {
                R_Clash = false;
                if (L_Clash == false)
                {
                    TextPump.enabled = false; // ซ่อนข้อความให้กดปุ่มปั้ม
                }
            }
            else if (other.gameObject.name == "Left Controller")
            {
                L_Clash = false;
                if (L_Clash == false)
                {
                    TextPump.enabled = false; // ซ่อนข้อความให้กดปุ่มปั้ม
                }
            }
        }
        else if (onSocket.onSocketStatus.GetStatus() == false)
        {
            
            TextPump.enabled = false; // ซ่อนข้อความให้กดปุ่มปั้ม
        }
    }

    public void BVMPump() //แสดงการปั้ม
    {
        ObjectAnimator.SetBool("isPuase", false);
        ObjectAnimator.SetBool("isPump", true);
        sound.Play(0);
    }

    public void BVMPuase()//หยุดการปั้ม
    {
        ObjectAnimator.SetBool("isPump", false);
        ObjectAnimator.SetBool("isPuase", true);
    }

    private void LeftHandTriggerPress(InputAction.CallbackContext obj) //ปั้มด้วยมือซ้าย
    {
        if (L_Clash == true) BVMPump();
    }

    private void LeftHandTriggerCancle(InputAction.CallbackContext obj) //หยุดปั้มด้วยมือซ้าย
    {
        if (L_Clash == true) BVMPuase();
    }

    private void RightHandTriggerPress(InputAction.CallbackContext obj) //ปั้มด้วยมือขวา
    {
        if (R_Clash == true) BVMPump();

    }

    private void RightHandTriggerCancle(InputAction.CallbackContext obj) //หยุดปั้มด้วยมือขวา
    {
        if (R_Clash == true) BVMPuase();
    }
}