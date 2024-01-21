using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour //คลาสการซ่อนเครื่องปั้มหัวใจที่ถืออยู่บนมือ
{
    [SerializeField] public GameObject Target;
    [SerializeField] public GameObject AnotherTarget;

    private MeshRenderer MeshRen;

    void Awake() 
    {
        MeshRen = this.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other) //เช็คสถานะ Socket ซ่อนเครื่องปั้มหัวใจบนมือ
    {
        if (other.gameObject.name == Target.name || other.gameObject.name == AnotherTarget.name)
        {
            MeshRen.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)  //เช็คสถานะ Socket แสดงเครื่องปั้มหัวใจบนมือ
    {
        if (other.gameObject.name == Target.name || other.gameObject.name == AnotherTarget.name)
        {
            MeshRen.enabled = true;
        }
    }
}
