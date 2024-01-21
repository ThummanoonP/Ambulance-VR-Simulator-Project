using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayBVMPumpAnimation : MonoBehaviour // ���ʡ�á�����������ͧ BVM
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

    private void OnTriggerEnter(Collider other) // ������������㹵��˹觻�����
    {
        if (onSocket.onSocketStatus.GetStatus() == true) 
        {
            if (other.gameObject.name == "Right Controller")
            {
                R_Clash = true;
                TextPump.enabled = true; // �ʴ���ͤ�����顴��������
            }
            else if (other.gameObject.name == "Left Controller")
            {
                L_Clash = true;
                TextPump.enabled = true; // �ʴ���ͤ�����顴��������
            }
        }
        
    }

    private void OnTriggerExit(Collider other) // ���������͡�ҡ���˹觻�����
    {
        if (onSocket.onSocketStatus.GetStatus() == true)
        {
            if (other.gameObject.name == "Right Controller")
            {
                R_Clash = false;
                if (L_Clash == false)
                {
                    TextPump.enabled = false; // ��͹��ͤ�����顴��������
                }
            }
            else if (other.gameObject.name == "Left Controller")
            {
                L_Clash = false;
                if (L_Clash == false)
                {
                    TextPump.enabled = false; // ��͹��ͤ�����顴��������
                }
            }
        }
        else if (onSocket.onSocketStatus.GetStatus() == false)
        {
            
            TextPump.enabled = false; // ��͹��ͤ�����顴��������
        }
    }

    public void BVMPump() //�ʴ���û���
    {
        ObjectAnimator.SetBool("isPuase", false);
        ObjectAnimator.SetBool("isPump", true);
        sound.Play(0);
    }

    public void BVMPuase()//��ش��û���
    {
        ObjectAnimator.SetBool("isPump", false);
        ObjectAnimator.SetBool("isPuase", true);
    }

    private void LeftHandTriggerPress(InputAction.CallbackContext obj) //����������ͫ���
    {
        if (L_Clash == true) BVMPump();
    }

    private void LeftHandTriggerCancle(InputAction.CallbackContext obj) //��ش����������ͫ���
    {
        if (L_Clash == true) BVMPuase();
    }

    private void RightHandTriggerPress(InputAction.CallbackContext obj) //����������͢��
    {
        if (R_Clash == true) BVMPump();

    }

    private void RightHandTriggerCancle(InputAction.CallbackContext obj) //��ش����������͢��
    {
        if (R_Clash == true) BVMPuase();
    }
}