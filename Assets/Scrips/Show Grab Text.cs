using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ShowGrabText : MonoBehaviour // คลาสแสดง GrabMessage ใน Tutorial
{
    [SerializeField] GameObject GrabMessage;
    private TextMeshProUGUI GrabText;
    bool L_Clash = false;
    bool R_Clash = false;

    private void Awake()
    {
        GrabText = GrabMessage.GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Right Controller")
        {
            R_Clash = true;
            GrabText.enabled = true;
        }
        else if (other.gameObject.name == "Left Controller")
        {
            L_Clash = true;
            GrabText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Right Controller")
        {
            R_Clash = false;
            if (L_Clash == false)
            {
                GrabText.enabled = false;
            }
        }
        else if (other.gameObject.name == "Left Controller")
        {
            L_Clash = false;
            if (R_Clash == false)
            {
                GrabText.enabled = false;
            }
        }
    }
}