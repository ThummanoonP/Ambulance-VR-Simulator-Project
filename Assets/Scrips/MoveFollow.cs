using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFollow : MonoBehaviour //�����������ǹ Corpuls 3 ��Ѻ����ҹ���� Grab 
{
    private GameObject Leader;// �ҹ Corpuls3

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
