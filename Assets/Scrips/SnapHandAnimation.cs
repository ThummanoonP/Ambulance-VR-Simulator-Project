using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class SnapHandAnimation : MonoBehaviour // คลาสเปิด/ปิดการทำงาน Animation
{
    [SerializeField] private Animator handAnimator;
    [SerializeField] private InputActionReference triggerActionRef;
    [SerializeField] private InputActionReference gripActionRef;

    private static readonly int TriggerAnimation = Animator.StringToHash("Trigger");
    private static readonly int GripAnimation = Animator.StringToHash("Grip");

    private void OnEnable()
    {
        triggerActionRef.action.performed += TriggerAction_performed;
        triggerActionRef.action.canceled += TriggerAction_canceled;

        gripActionRef.action.performed += GripAction_performed;
        gripActionRef.action.canceled += GripAction_canceled;

    }

    private void TriggerAction_performed(InputAction.CallbackContext obj) => handAnimator.SetFloat(TriggerAnimation, 1);


    private void TriggerAction_canceled(InputAction.CallbackContext obj) => handAnimator.SetFloat(TriggerAnimation, 0);


    private void GripAction_performed(InputAction.CallbackContext obj) => handAnimator.SetFloat(GripAnimation, 1);
 


    private void GripAction_canceled(InputAction.CallbackContext obj) => handAnimator.SetFloat(GripAnimation, 0);


    private void OnDisable()
    {
        triggerActionRef.action.performed -= TriggerAction_performed;
        triggerActionRef.action.canceled -= TriggerAction_canceled;

        gripActionRef.action.performed -= GripAction_performed;
        gripActionRef.action.canceled -= GripAction_canceled;
    }

}