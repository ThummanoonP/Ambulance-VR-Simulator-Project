using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using TMPro;


public class CorpulsFunction : MonoBehaviour//คลาสการทำงานของเครื่อง Corpuls3
{
    [SerializeField] InputActionReference LeftTrigger; //ปุ่ม Trigger มือซ้าย
    [SerializeField] InputActionReference RightTrigger; //ปุ่ม Trigger มือขวา
    [SerializeField] GameObject Base; //ฐาน Corpuls3
    [SerializeField] GameObject SplitMessage; //UI Massage Split
    [SerializeField] GameObject MergeMessage; //UI Massage Merge

    private XRGrabInteractable[] GrabPoint; //ตำแหน่งยก
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

    private void OnTriggerEnter(Collider other) //แสดง Massage Split/Merge และเซ็ต Status มือซ้าย/ขวา อยู่ในตำแหน่ง
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

    private void OnTriggerExit(Collider other) //ซ่อน Massage Split/Merge และเซ็ต Status มือซ้าย/ขวา ออกจากตำแหน่ง
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

    private void LeftHandTriggerPress(InputAction.CallbackContext obj) //Split/Merge ด้วยมือซ้าย
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

    private void RightHandTriggerPress(InputAction.CallbackContext obj) //Split/Merge ด้วยมือขวา
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

    public void Split() //เล่น Animation การ Split
    {
        ObjectAnimator.SetBool("isMerge", false);
        ObjectAnimator.SetBool("isSplit", true);
        turnOffGrabBase();
        TextSplit.enabled = false;
        TextMerge.enabled = true;
    }

    public void Merge() //เล่น Animation การ Merge
    {
        ObjectAnimator.SetBool("isSplit", false);
        ObjectAnimator.SetBool("isMerge", true);
        turnOnGrabBase();
        TextMerge.enabled = false;
        TextSplit.enabled = true;
    }

    private void CallBack() //Reposition ชิ้นส่วน Corpuls 3 ทั้งหมด
    {
        foreach (var Reposition in reposition)
        {
            Reposition.SetRoot();
        }
    }

    public void SetNewRoot()//กำหนด position เริ่มต้นชิ้นส่วน Corpuls 3 ใหม่ทั้งหมด
    {
        foreach (var Reposition in reposition)
        {
            Reposition.RootPosition();
        }
    }


    private void turnOnGrabInteract() //เปิดการทำงาน XRGrabInteractable ชิ้นส่วน Corpuls 3
    {
        foreach (var XRGrabInteractable in GrabPoint)
        {
            XRGrabInteractable.enabled = true;
        }
    }

    private void turnOffGrabInteract() //ปิดการทำงาน XRGrabInteractable ชิ้นส่วน Corpuls 3
    {
        foreach (var XRGrabInteractable in GrabPoint)
        {
            XRGrabInteractable.enabled = false;
        }
    }

    private void turnOnGrabBase() //เปิดการทำงาน XRGrabInteractable ฐาน Corpuls 3
    {
            GrabBase.enabled = true;
    }

    private void turnOffGrabBase() //ปิดการทำงาน XRGrabInteractable ฐาน Corpuls 3
    {
            GrabBase.enabled = false;
    }
}
