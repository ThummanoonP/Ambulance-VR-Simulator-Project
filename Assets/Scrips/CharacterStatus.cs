using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour//คลาสค่าชีพจรผู้ป่วย
{
    [SerializeField] public GameObject Socket;//ตำแหน่งวางหูฟัง

    private float HeartRateMax;
    private float HeartRate;

    private float timer = 0.0f;
    private AudioSource HeartBeat;//เสียงหัวใจเต้น
    private OnSocketStatus onSocketStatus;

    // Start is called before the first frame update
    void Start()
    {
        HeartBeat = this.GetComponent<AudioSource>();
        onSocketStatus = Socket.GetComponent<OnSocketStatus>();
        HeartRateMax = 120.0f;
        HeartRate = 120.0f;//Random.Range(10, 70); //ค่าชีพจร
        HeartBeat.volume = HeartRate / 120.0f;
        HeartBeat.pitch = (HeartRate / 120.0f)*0.0105f;
    }

    public float GetHeartRate()
    {
        return HeartRate;
    }

    public void HeartRatePumping()
    {
        HeartRateUp();
    }

    private void HeartRateDown()
    {
        HeartRate -= 10.0f;
        //Debug.Log("Rate Down : " + HeartRate);
    }

    private void HeartRateUp()
    {
        HeartRate += 10.0f;
        //Debug.Log("Rate Up : " + HeartRate);
    }

    private void RateCheck()
    {
        if ((HeartRate > 0) && (HeartRate <= HeartRateMax))
        {
            if (onSocketStatus.GetStatus())
            {
                //HeartRateDown();
            }
        }
        else if (HeartRate > HeartRateMax)
        {
            HeartRate = HeartRateMax;
        }

        //Debug.Log("Heart Rate : " +HeartRate);
        HeartBeat.volume = HeartRate / 120.0f;
        HeartBeat.pitch = (HeartRate / 120.0f) * (1.25f);
        //Debug.Log("Heart Beat Volume : " + HeartBeat.volume);
        //Debug.Log("Heart Beat Speed : " + HeartBeat.pitch);

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 3.0f)
        {
            RateCheck();
            timer = 0.0f;
        }
    }

}
