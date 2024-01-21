using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticPulse : MonoBehaviour // คลาสการเช็คชีพจร
{
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    [SerializeField] private GameObject Character; // ผู้ป่วย

    private CharacterStatus HeartRate;
    bool LeftCheck = false;
    bool RightCheck = false;
    private float duration = 0.0f;
    private float timer = 0.0f;
    private ActionBasedController LeftController = null;
    private ActionBasedController RightController = null;

    private Animator LeftHandAnimator;
    private Animator RightHandAnimator;

    void Awake()
    {
        LeftHandAnimator = LeftHand.GetComponentInChildren<Animator>();
        RightHandAnimator = RightHand.GetComponentInChildren<Animator>();
        HeartRate = Character.GetComponent<CharacterStatus>();
        RightController = RightHand.GetComponent<ActionBasedController>();
        LeftController = LeftHand.GetComponent<ActionBasedController>();
    }

    private void OnTriggerEnter(Collider other) // เช็คมืออยู่ในตำแหน่ง
    {
        Debug.Log(other.gameObject.name);
        if ((other.gameObject.name == "Right Controller") && (LeftCheck == false))
        {
            RightHandPulseCheck();
            RightCheck = true;

        }
        else if ((other.gameObject.name == "Left Controller") && (RightCheck == false))
        {
            LeftHandPulseCheck();
            LeftCheck = true;
        }

    }

    private void OnTriggerExit(Collider other) // เช็คมือออกจากตำแหน่ง
    {
        if (other.gameObject.name == "Right Controller")
        {
            CancleRightHandPulseCheck();
            RightCheck = false;
        }
        else if (other.gameObject.name == "Left Controller")
        {
            CancleLeftHandPulseCheck();
            LeftCheck = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= (60.0f / HeartRate.GetHeartRate()))
        {
            duration = (HeartRate.GetHeartRate() / 120) * 0.2f; // กำหนดค่าการสั่น
            if (RightCheck) RightController.SendHapticImpulse(0.02f, duration); // สั่ง Controller ขวาสั่น 
            if (LeftCheck) LeftController.SendHapticImpulse(0.02f, duration); // สั่ง Controller ซ้ายสั่น
            timer = 0.0f;
        }

    }

    public void LeftHandPulseCheck() // เปลี่ยนมือซ้ายเป็นท่าจับชีพจร
    {
        LeftHandAnimator.SetBool("isIdle", false);
        LeftHandAnimator.SetBool("isPulseCheck", true);
    }

    public void CancleLeftHandPulseCheck() // เปลี่ยนมือซ้ายเป็นปกติ
    {
        LeftHandAnimator.SetBool("isPulseCheck", false);
        LeftHandAnimator.SetBool("isIdle", true);
    }

    public void RightHandPulseCheck() // เปลี่ยนมือขวาเป็นท่าจับชีพจร
    {
        RightHandAnimator.SetBool("isIdle", false);
        RightHandAnimator.SetBool("isPulseCheck", true);
    }

    public void CancleRightHandPulseCheck() // เปลี่ยนมือขวาเป็นปกติ
    {
        RightHandAnimator.SetBool("isPulseCheck", false);
        RightHandAnimator.SetBool("isIdle", true);
    }
}
