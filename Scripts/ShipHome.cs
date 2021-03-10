using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ShipHome : MonoBehaviour
{
    private float X_Pos;
    private float Y_Pos;
    private float Z_Pos;

    private Rigidbody rigidbody;
    private BoxCollider box;

    void Start()
    {
        X_Pos = transform.position.x;
        Y_Pos = transform.position.y;
        Z_Pos = transform.position.z;

        rigidbody = this.GetComponent<Rigidbody>();
        box = this.GetComponent<BoxCollider>();

        box.size = new Vector3(4f, 3f, 14f);
        rigidbody.useGravity = false;
        rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Y_Pos += 1;        
        if (gameObject.transform.position.y >= 320)
        {
            Z_Pos += 0.5f;
            gameObject.transform.position = new Vector3(X_Pos, Y_Pos, Z_Pos);
        }
        else if (gameObject.transform.position.y < 320)
        {
            Y_Pos += 0.5f;
            gameObject.transform.position = new Vector3(X_Pos, Y_Pos, Z_Pos);
        }
    }
}
