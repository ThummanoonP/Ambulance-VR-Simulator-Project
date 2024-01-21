using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlackSphereAnimation : MonoBehaviour //คลาสการเล่น Animation การใช้เครื่องปั้มหัวใจใน Tutorial
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

    public void SetBipLTrue() => BipL = true; //เซ็ต Status เครื่องปั้มมือซ้ายอยู่ในตำแหน่ง 
    public void SetBipLFalse() => BipL = false; //เซ็ต Status เครื่องปั้มมือซ้ายอยู่ในตำแหน่ง
    public void SetBipRTrue() => BipR = true; //เซ็ต Status เครื่องปั้มมือขวาอยู่ในตำแหน่ง 
    public void SetBipRFalse() => BipR = false; //เซ็ต Status เครื่องปั้มมือขวาอยู่ในตำแหน่ง

    public void Bip()//เล่น Animation การปั้มหัวใจ
    {
        ObjectAnimator.SetBool("isIdle", false);
        ObjectAnimator.SetBool("isBip", true);
    }

    public void CancleBip()//กลับไปสถานะ Idle
    {
        ObjectAnimator.SetBool("isBip", false);
        ObjectAnimator.SetBool("isIdle", true);
    }
}
