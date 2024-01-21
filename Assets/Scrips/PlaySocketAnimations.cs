using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySocketAnimations : MonoBehaviour//คลาสการเล่น Animation เมื่อใช้อุปกรณ์การแพทย์กับผู้ป่วยทั้งหมด
{
    [SerializeField] private GameObject EZIONeedle;//เข็มเครื่องเจาะไขกระดูก

    private Animator ObjectAnimator;
    private bool BipL = false;//ตัวบอก Status เครื่องปั้มมือซ้าย
    private bool BipR = false;//ตัวบอก Status เครื่องปั้มมือขวา

    void Awake()
    {
        ObjectAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (BipL == true && BipR == true) Bip();
        else CancleBip();
    }

    public void SetBipLTrue() => BipL = true; //เซ็ต Status เครื่องปั้มมือซ้ายอยู่ในตำแหน่ง 
    public void SetBipLFalse() => BipL = false; //เซ็ต Status เครื่องปั้มมือซ้ายอยู่ในตำแหน่ง
    public void SetBipRTrue() => BipR = true; //เซ็ต Status เครื่องปั้มมือขวาอยู่ในตำแหน่ง 
    public void SetBipRFalse() => BipR = false; //เซ็ต Status เครื่องปั้มมือขวาอยู่ในตำแหน่ง

    public void InsertACLS()//เล่น Animation การใช้ ACLS
    {
        ObjectAnimator.SetBool("isRemoveACLS", false);
        ObjectAnimator.SetBool("isACLS", true);
    }

    public void RemoveACLS()//ยกเลิก ACLS กลับไปสถานะ Idle
    {
        ObjectAnimator.SetBool("isACLS", false);
        ObjectAnimator.SetBool("isRemoveACLS", true);
    }

    public void InsertAirWay()//เล่น Animation การใช้ AirWay
    {
        ObjectAnimator.SetBool("isRemoveAirWay", false);
        ObjectAnimator.SetBool("isAirWay", true);
    }

    public void RemoveAirWay()//ยกเลิก AirWay กลับไปสถานะ Idle
    {
        ObjectAnimator.SetBool("isAirWay", false);
        ObjectAnimator.SetBool("isRemoveAirWay", true);
    }

    public void InsertBVM()//เล่น Animation การใช้ BVM
    {
        ObjectAnimator.SetBool("isRemoveBVM", false);
        ObjectAnimator.SetBool("isBVM", true);
    }

    public void RemoveBVM()//ยกเลิก BVM กลับไปสถานะ Idle
    {
        ObjectAnimator.SetBool("isBVM", false);
        ObjectAnimator.SetBool("isRemoveBVM", true);
    }

    public void InsertEZIO()//เล่น Animation การใช้ EZIO
    {
        ObjectAnimator.SetBool("isRemoveEZIO", false);
        ObjectAnimator.SetBool("isEZIO", true);
    }

    public void RemoveEZIO()//ยกเลิก EZIO กลับไปสถานะ Idle
    {
        ObjectAnimator.SetBool("isEZIO", false);
        ObjectAnimator.SetBool("isRemoveEZIO", true);
        Invoke("RemoveEZIONeedle", 1.3f);
    }

    private void RemoveEZIONeedle() => EZIONeedle.SetActive(false); //ซ่อนหัวเข็มเครื่องเจาะไขกระดูก

    public void Bip()//เล่น Animation การปั้มหัวใจ
    {
        ObjectAnimator.SetBool("isCancleBip", false);
        ObjectAnimator.SetBool("isBip", true);
    }

    public void CancleBip()//ยกเลิกการปั้มหัวใจกลับไปสถานะ Idle
    {
        ObjectAnimator.SetBool("isBip", false);
        ObjectAnimator.SetBool("isCancleBip", true);
    }
}
