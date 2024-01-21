using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSocketAnimation : MonoBehaviour //�����ʴ� Animation �ͧ����� Item � Tutorial 
{
    private Animator ObjectAnimator;

    void Awake()
    {
        ObjectAnimator = this.GetComponent<Animator>();
    }

    public void InsertAirWay()
    {
        ObjectAnimator.SetBool("isIdle", false);
        ObjectAnimator.SetBool("isAirWay", true);
    }

    public void RemoveAirWay()
    {
        ObjectAnimator.SetBool("isAirWay", false);
        ObjectAnimator.SetBool("isIdle", true);
    }
}
