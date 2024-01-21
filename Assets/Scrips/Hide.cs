using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour //���ʡ�ë�͹����ͧ�������㨷�������躹���
{
    [SerializeField] public GameObject Target;
    [SerializeField] public GameObject AnotherTarget;

    private MeshRenderer MeshRen;

    void Awake() 
    {
        MeshRen = this.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other) //��ʶҹ� Socket ��͹����ͧ�������㨺����
    {
        if (other.gameObject.name == Target.name || other.gameObject.name == AnotherTarget.name)
        {
            MeshRen.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)  //��ʶҹ� Socket �ʴ�����ͧ�������㨺����
    {
        if (other.gameObject.name == Target.name || other.gameObject.name == AnotherTarget.name)
        {
            MeshRen.enabled = true;
        }
    }
}
