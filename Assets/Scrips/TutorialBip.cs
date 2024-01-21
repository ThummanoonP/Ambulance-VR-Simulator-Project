using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBip : MonoBehaviour //คลาสการเช็คและเซ็ตสถานะตำแหน่งเครื่องปั้มหัวใจเมื่อชน Socket ใน Tutorial
{
    [SerializeField] public GameObject BlackSphere;
    [SerializeField] public GameObject VisL;
    [SerializeField] public GameObject VisR;

    private TutorialBlackSphereAnimation BlackSphereAnima;
    private MeshRenderer MeshRen;
    private MeshRenderer MeshVisL;
    private MeshRenderer MeshVisR;
    bool LeftCheck = false;
    bool RightCheck = false;


    void Awake()
    {
        BlackSphereAnima = BlackSphere.GetComponent<TutorialBlackSphereAnimation>();
        MeshRen = this.GetComponent<MeshRenderer>();
        MeshVisL = VisL.GetComponent<MeshRenderer>();
        MeshVisR = VisR.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "handGrabR") && (LeftCheck == false))
        {
            MeshRen.enabled = true; // Render เครื่องปั้มหัวใจ Mock Up
            MeshVisR.enabled = false; // ยกเลิก Render Socket เครื่องปั้มหัวใจ
            RightCheck = true;
            BlackSphereAnima.SetBipRTrue(); //เซ็ต Status เครื่องปั้มมือขวาอยู่ในตำแหน่ง 
        } 
        else if ((other.gameObject.name == "handGrabL") && (RightCheck == false))
        {
            MeshRen.enabled = true; // Render เครื่องปั้มหัวใจ Mock Up
            MeshVisL.enabled = false; // ยกเลิก Render Socket เครื่องปั้มหัวใจ
            LeftCheck = true;
            BlackSphereAnima.SetBipLTrue();//เซ็ต Status เครื่องปั้มมือซ้ายอยู่ในตำแหน่ง
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "handGrabR" && (RightCheck == true))
        {
            MeshRen.enabled = false; // ยกเลิก Render เครื่องปั้มหัวใจ Mock Up
            MeshVisR.enabled = true; // Render Socket เครื่องปั้มหัวใจ
            RightCheck = false;
            BlackSphereAnima.SetBipRFalse(); //เซ็ต Status เครื่องปั้มมือขวาออกจากตำแหน่ง
        }
        else if (other.gameObject.name == "handGrabL" && (LeftCheck == true))
        {
            MeshRen.enabled = false; // ยกเลิก Render เครื่องปั้มหัวใจ Mock Up
            MeshVisL.enabled = true; // Render Socket เครื่องปั้มหัวใจ
            LeftCheck = false;
            BlackSphereAnima.SetBipLFalse(); //เซ็ต Status เครื่องปั้มมือซ้ายออกจากตำแหน่ง
        }
    }


}
