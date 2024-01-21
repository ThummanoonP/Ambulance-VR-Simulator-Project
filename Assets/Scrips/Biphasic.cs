using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biphasic : MonoBehaviour //���ʡ���������ʶҹе��˹�����ͧ������������ͪ� Socket
{
    [SerializeField] public GameObject HumanBody;//������

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
            MeshRen.enabled = true; // Render ����ͧ�������� Mock Up
            RightCheck = true;
            PlaySoc.SetBipRTrue(); //�� Status ����ͧ������͢������㹵��˹� 
        }
        else if ((other.gameObject.name == "handGrabL") && (RightCheck == false))
        {
            MeshRen.enabled = true; // Render ����ͧ�������� Mock Up
            LeftCheck = true;
            PlaySoc.SetBipLTrue(); //�� Status ����ͧ������ͫ�������㹵��˹�
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "handGrabR" && (RightCheck == true))
        {
            MeshRen.enabled = false; // ¡��ԡ Render ����ͧ�������� Mock Up
            RightCheck = false;
            PlaySoc.SetBipRFalse(); //�� Status ����ͧ������͢���͡�ҡ���˹�
        }
        else if (other.gameObject.name == "handGrabL" && (LeftCheck == true))
        {
            MeshRen.enabled = false; // ¡��ԡ Render ����ͧ�������� Mock Up
            LeftCheck = false;
            PlaySoc.SetBipLFalse(); //�� Status ����ͧ������ͫ����͡�ҡ���˹�
        }
    }
}
