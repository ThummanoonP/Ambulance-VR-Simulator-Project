using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlackSphereAnimation : MonoBehaviour //���ʡ����� Animation ���������ͧ��������� Tutorial
{
    private Animator ObjectAnimator;
    private bool BipL = false;
    private bool BipR = false;

    void Awake()
    {
        ObjectAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (BipL == true && BipR == true) Bip();
        else CancleBip();
    }

    public void SetBipLTrue() => BipL = true; //�� Status ����ͧ������ͫ�������㹵��˹� 
    public void SetBipLFalse() => BipL = false; //�� Status ����ͧ������ͫ�������㹵��˹�
    public void SetBipRTrue() => BipR = true; //�� Status ����ͧ������͢������㹵��˹� 
    public void SetBipRFalse() => BipR = false; //�� Status ����ͧ������͢������㹵��˹�

    public void Bip()//��� Animation ��û�������
    {
        ObjectAnimator.SetBool("isIdle", false);
        ObjectAnimator.SetBool("isBip", true);
    }

    public void CancleBip()//��Ѻ�ʶҹ� Idle
    {
        ObjectAnimator.SetBool("isBip", false);
        ObjectAnimator.SetBool("isIdle", true);
    }
}
