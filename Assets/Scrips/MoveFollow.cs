using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFollow : MonoBehaviour //คลาสให้ชิ้นส่วน Corpuls 3 ขยับตามฐานเวลา Grab 
{
    private GameObject Leader;// ฐาน Corpuls3

    void Awake()
    {
        Leader = GameObject.Find("Corpuls 3 Full Body Type");
    }

    void Update()
    {
        this.transform.position = Leader.transform.position;
        this.transform.rotation = Leader.transform.rotation;
    }
}
