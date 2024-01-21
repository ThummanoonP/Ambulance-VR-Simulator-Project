using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySocketAnimations : MonoBehaviour//���ʡ����� Animation ��������ػ�ó���ᾷ��Ѻ�����·�����
{
    [SerializeField] private GameObject EZIONeedle;//�������ͧ���䢡�д١

    private Animator ObjectAnimator;
    private bool BipL = false;//��Ǻ͡ Status ����ͧ������ͫ���
    private bool BipR = false;//��Ǻ͡ Status ����ͧ������͢��

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

    public void InsertACLS()//��� Animation ����� ACLS
    {
        ObjectAnimator.SetBool("isRemoveACLS", false);
        ObjectAnimator.SetBool("isACLS", true);
    }

    public void RemoveACLS()//¡��ԡ ACLS ��Ѻ�ʶҹ� Idle
    {
        ObjectAnimator.SetBool("isACLS", false);
        ObjectAnimator.SetBool("isRemoveACLS", true);
    }

    public void InsertAirWay()//��� Animation ����� AirWay
    {
        ObjectAnimator.SetBool("isRemoveAirWay", false);
        ObjectAnimator.SetBool("isAirWay", true);
    }

    public void RemoveAirWay()//¡��ԡ AirWay ��Ѻ�ʶҹ� Idle
    {
        ObjectAnimator.SetBool("isAirWay", false);
        ObjectAnimator.SetBool("isRemoveAirWay", true);
    }

    public void InsertBVM()//��� Animation ����� BVM
    {
        ObjectAnimator.SetBool("isRemoveBVM", false);
        ObjectAnimator.SetBool("isBVM", true);
    }

    public void RemoveBVM()//¡��ԡ BVM ��Ѻ�ʶҹ� Idle
    {
        ObjectAnimator.SetBool("isBVM", false);
        ObjectAnimator.SetBool("isRemoveBVM", true);
    }

    public void InsertEZIO()//��� Animation ����� EZIO
    {
        ObjectAnimator.SetBool("isRemoveEZIO", false);
        ObjectAnimator.SetBool("isEZIO", true);
    }

    public void RemoveEZIO()//¡��ԡ EZIO ��Ѻ�ʶҹ� Idle
    {
        ObjectAnimator.SetBool("isEZIO", false);
        ObjectAnimator.SetBool("isRemoveEZIO", true);
        Invoke("RemoveEZIONeedle", 1.3f);
    }

    private void RemoveEZIONeedle() => EZIONeedle.SetActive(false); //��͹����������ͧ���䢡�д١

    public void Bip()//��� Animation ��û�������
    {
        ObjectAnimator.SetBool("isCancleBip", false);
        ObjectAnimator.SetBool("isBip", true);
    }

    public void CancleBip()//¡��ԡ��û������㨡�Ѻ�ʶҹ� Idle
    {
        ObjectAnimator.SetBool("isBip", false);
        ObjectAnimator.SetBool("isCancleBip", true);
    }
}
