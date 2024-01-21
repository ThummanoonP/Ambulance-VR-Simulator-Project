using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticPulse : MonoBehaviour // ���ʡ���礪վ��
{
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;
    [SerializeField] private GameObject Character; // ������

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

    private void OnTriggerEnter(Collider other) // ���������㹵��˹�
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

    private void OnTriggerExit(Collider other) // ������͡�ҡ���˹�
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
            duration = (HeartRate.GetHeartRate() / 120) * 0.2f; // ��˹���ҡ�����
            if (RightCheck) RightController.SendHapticImpulse(0.02f, duration); // ��� Controller ������ 
            if (LeftCheck) LeftController.SendHapticImpulse(0.02f, duration); // ��� Controller �������
            timer = 0.0f;
        }

    }

    public void LeftHandPulseCheck() // ����¹��ͫ����繷�ҨѺ�վ��
    {
        LeftHandAnimator.SetBool("isIdle", false);
        LeftHandAnimator.SetBool("isPulseCheck", true);
    }

    public void CancleLeftHandPulseCheck() // ����¹��ͫ����繻���
    {
        LeftHandAnimator.SetBool("isPulseCheck", false);
        LeftHandAnimator.SetBool("isIdle", true);
    }

    public void RightHandPulseCheck() // ����¹��͢���繷�ҨѺ�վ��
    {
        RightHandAnimator.SetBool("isIdle", false);
        RightHandAnimator.SetBool("isPulseCheck", true);
    }

    public void CancleRightHandPulseCheck() // ����¹��͢���繻���
    {
        RightHandAnimator.SetBool("isPulseCheck", false);
        RightHandAnimator.SetBool("isIdle", true);
    }
}
