using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biphasic : MonoBehaviour //คลาสการเช็คและเซ็ตสถานะตำแหน่งเครื่องปั้มหัวใจเมื่อชน Socket
{
    [SerializeField] public GameObject HumanBody;//ผู้ป่วย

    private PlaySocketAnimations PlaySoc;
    private MeshRenderer MeshRen;
    bool LeftCheck = false;
    bool RightCheck = false;

    void Awake()
    {
        PlaySoc = HumanBody.GetComponent<PlaySocketAnimations>();
        MeshRen = this.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.name == "handGrabR") && (LeftCheck == false))
        {
            MeshRen.enabled = true; // Render เครื่องปั้มหัวใจ Mock Up
            RightCheck = true;
            PlaySoc.SetBipRTrue(); //เซ็ต Status เครื่องปั้มมือขวาอยู่ในตำแหน่ง 
        }
        else if ((other.gameObject.name == "handGrabL") && (RightCheck == false))
        {
            MeshRen.enabled = true; // Render เครื่องปั้มหัวใจ Mock Up
            LeftCheck = true;
            PlaySoc.SetBipLTrue(); //เซ็ต Status เครื่องปั้มมือซ้ายอยู่ในตำแหน่ง
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "handGrabR" && (RightCheck == true))
        {
            MeshRen.enabled = false; // ยกเลิก Render เครื่องปั้มหัวใจ Mock Up
            RightCheck = false;
            PlaySoc.SetBipRFalse(); //เซ็ต Status เครื่องปั้มมือขวาออกจากตำแหน่ง
        }
        else if (other.gameObject.name == "handGrabL" && (LeftCheck == true))
        {
            MeshRen.enabled = false; // ยกเลิก Render เครื่องปั้มหัวใจ Mock Up
            LeftCheck = false;
            PlaySoc.SetBipLFalse(); //เซ็ต Status เครื่องปั้มมือซ้ายออกจากตำแหน่ง
        }
    }
}
