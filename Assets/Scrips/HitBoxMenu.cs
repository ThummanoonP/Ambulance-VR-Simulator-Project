using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HitBoxMenu : MonoBehaviour //คลาสเปลี่ยนมือเป็นการชี้เมื่ออยู่่ในพื้นที่หน้า Menu
{
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;

    private Animator LeftHandAnimator;
    private Animator RightHandAnimator;

    void Awake()
    {
        LeftHandAnimator = LeftHand.GetComponentInChildren<Animator>();
        RightHandAnimator = RightHand.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Right Controller")
        {
            RightHandPoint();
        }
        else if (other.gameObject.name == "Left Controller")
        {
            LeftHandPoint();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Right Controller")
        {
            CancleRightHandPoint();
        }
        else if (other.gameObject.name == "Left Controller")
        {
            CancleLeftHandPoint();
        }
    }

    public void LeftHandPoint()
    {
        LeftHandAnimator.SetBool("isIdle", false);
        LeftHandAnimator.SetBool("isPoint", true);
    }

    public void CancleLeftHandPoint()
    {
        LeftHandAnimator.SetBool("isPoint", false);
        LeftHandAnimator.SetBool("isIdle", true);
    }

    public void RightHandPoint()
    {
        RightHandAnimator.SetBool("isIdle", false);
        RightHandAnimator.SetBool("isPoint", true);
    }

    public void CancleRightHandPoint()
    {
        RightHandAnimator.SetBool("isPoint", false);
        RightHandAnimator.SetBool("isIdle", true);
    }
}
