using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour //คลาสการ Respawn Item
{
    private GameObject Prefab;
    private Vector3 Position;
    private Quaternion Rotation;
    private OnSocket onSocket;

    void Awake()
    {
        Prefab = this.gameObject;
        Position = this.gameObject.transform.position;
        Rotation = this.gameObject.transform.rotation;
        onSocket = this.GetComponent<OnSocket>();
    }

    public void RespawnObject()
    {

        if (onSocket.onSocketStatus.GetStatus() == false)
        {
            Instantiate(Prefab, Position, Rotation);
        }
    }
}