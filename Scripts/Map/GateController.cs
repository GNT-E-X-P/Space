using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private Scene1Manager scene1Manager;
    private void Start()
    {
        scene1Manager = GameObject.Find("Scene1Manager").GetComponent<Scene1Manager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            scene1Manager.Move();
        }
    }

}
