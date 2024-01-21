using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBip : MonoBehaviour //���ʡ���������ʶҹе��˹�����ͧ������������ͪ� Socket � Tutorial
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
            MeshRen.enabled = true; // Render ����ͧ�������� Mock Up
            MeshVisR.enabled = false; // ¡��ԡ Render Socket ����ͧ��������
            RightCheck = true;
            BlackSphereAnima.SetBipRTrue(); //�� Status ����ͧ������͢������㹵��˹� 
        } 
        else if ((other.gameObject.name == "handGrabL") && (RightCheck == false))
        {
            MeshRen.enabled = true; // Render ����ͧ�������� Mock Up
            MeshVisL.enabled = false; // ¡��ԡ Render Socket ����ͧ��������
            LeftCheck = true;
            BlackSphereAnima.SetBipLTrue();//�� Status ����ͧ������ͫ�������㹵��˹�
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "handGrabR" && (RightCheck == true))
        {
            MeshRen.enabled = false; // ¡��ԡ Render ����ͧ�������� Mock Up
            MeshVisR.enabled = true; // Render Socket ����ͧ��������
            RightCheck = false;
            BlackSphereAnima.SetBipRFalse(); //�� Status ����ͧ������͢���͡�ҡ���˹�
        }
        else if (other.gameObject.name == "handGrabL" && (LeftCheck == true))
        {
            MeshRen.enabled = false; // ¡��ԡ Render ����ͧ�������� Mock Up
            MeshVisL.enabled = true; // Render Socket ����ͧ��������
            LeftCheck = false;
            BlackSphereAnima.SetBipLFalse(); //�� Status ����ͧ������ͫ����͡�ҡ���˹�
        }
    }


}
