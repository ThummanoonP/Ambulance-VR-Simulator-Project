using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;


public class CorpulsFunction : MonoBehaviour//���ʡ�÷ӧҹ�ͧ����ͧ Corpuls3
{
    [SerializeField] InputActionReference LeftTrigger; //���� Trigger ��ͫ���
    [SerializeField] InputActionReference RightTrigger; //���� Trigger ��͢��
    [SerializeField] GameObject Base; //�ҹ Corpuls3
    [SerializeField] GameObject SplitMessage; //UI Massage Split
    [SerializeField] GameObject MergeMessage; //UI Massage Merge

    private XRGrabInteractable[] GrabPoint; //���˹�¡
    private Reposition[] reposition; 
    private Animator ObjectAnimator;
    private XRGrabInteractable GrabBase;
    private TextMeshProUGUI TextSplit;
    private TextMeshProUGUI TextMerge;
    bool L_Clash = false;
    bool R_Clash = false;
    bool SplitMode = false;

    void Awake()
    {
        GrabPoint = this.GetComponentsInChildren<XRGrabInteractable>();
        GrabBase = Base.GetComponent<XRGrabInteractable>();
        reposition = this.GetComponentsInChildren<Reposition>();
        ObjectAnimator = this.GetComponent<Animator>();
        TextSplit = SplitMessage.GetComponent<TextMeshProUGUI>();
        TextMerge = MergeMessage.GetComponent<TextMeshProUGUI>();
        LeftTrigger.action.performed += LeftHandTriggerPress;
        RightTrigger.action.performed += RightHandTriggerPress;
    }

    private void OnTriggerEnter(Collider other) //�ʴ� Massage Split/Merge ����� Status ��ͫ���/��� ����㹵��˹�
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Right Controller")
        {
            R_Clash = true;
            if (SplitMode == false)
            {
                SetNewRoot();
                TextSplit.enabled = true;
            }
            else if (SplitMode == true)
            {
                TextMerge.enabled = true;
            }

        }
        else if (other.gameObject.name == "Left Controller")
        {
            L_Clash = true;
            if (SplitMode == false) 
            { 
                SetNewRoot();
                TextSplit.enabled = true;
            }
            else if (SplitMode == true)
            {
                TextMerge.enabled = true;
            }

        }
    }

    private void OnTriggerExit(Collider other) //��͹ Massage Split/Merge ����� Status ��ͫ���/��� �͡�ҡ���˹�
    {
        if (other.gameObject.name == "Right Controller")
        {
            R_Clash = false;
            if (L_Clash == false) 
            {
                TextSplit.enabled = false;
                TextMerge.enabled = false;
            }
           
        }
        else if (other.gameObject.name == "Left Controller")
        {
            L_Clash = false;
            if (R_Clash == false)
            {
                TextSplit.enabled = false;
                TextMerge.enabled = false;
            }

        }
    }

    private void LeftHandTriggerPress(InputAction.CallbackContext obj) //Split/Merge ������ͫ���
    {
        if (L_Clash == true && SplitMode == false)
        {
            SplitMode = true;
            turnOnGrabInteract();
            Split();
        }
        else if (L_Clash == true && SplitMode == true)
        {
            SplitMode = false;
            turnOffGrabInteract();
            Merge();
            Invoke("CallBack", .30f);
        }
    }

    private void RightHandTriggerPress(InputAction.CallbackContext obj) //Split/Merge ������͢��
    {
        if (R_Clash == true && SplitMode == false)
        {
            SplitMode = true;
            turnOnGrabInteract();
            Split();
        }
        else if (R_Clash == true && SplitMode == true)
        {
            SplitMode = false;
            turnOffGrabInteract();
            Merge();
            Invoke("CallBack", .30f);
        }

    }

    public void Split() //��� Animation ��� Split
    {
        ObjectAnimator.SetBool("isMerge", false);
        ObjectAnimator.SetBool("isSplit", true);
        turnOffGrabBase();
        TextSplit.enabled = false;
        TextMerge.enabled = true;
    }

    public void Merge() //��� Animation ��� Merge
    {
        ObjectAnimator.SetBool("isSplit", false);
        ObjectAnimator.SetBool("isMerge", true);
        turnOnGrabBase();
        TextMerge.enabled = false;
        TextSplit.enabled = true;
    }

    private void CallBack() //Reposition �����ǹ Corpuls 3 ������
    {
        foreach (var Reposition in reposition)
        {
            Reposition.SetRoot();
        }
    }

    public void SetNewRoot()//��˹� position ������鹪����ǹ Corpuls 3 ���������
    {
        foreach (var Reposition in reposition)
        {
            Reposition.RootPosition();
        }
    }


    private void turnOnGrabInteract() //�Դ��÷ӧҹ XRGrabInteractable �����ǹ Corpuls 3
    {
        foreach (var XRGrabInteractable in GrabPoint)
        {
            XRGrabInteractable.enabled = true;
        }
    }

    private void turnOffGrabInteract() //�Դ��÷ӧҹ XRGrabInteractable �����ǹ Corpuls 3
    {
        foreach (var XRGrabInteractable in GrabPoint)
        {
            XRGrabInteractable.enabled = false;
        }
    }

    private void turnOnGrabBase() //�Դ��÷ӧҹ XRGrabInteractable �ҹ Corpuls 3
    {
            GrabBase.enabled = true;
    }

    private void turnOffGrabBase() //�Դ��÷ӧҹ XRGrabInteractable �ҹ Corpuls 3
    {
            GrabBase.enabled = false;
    }
}
