﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPosition : MonoBehaviour
{
    public Transform fixedTarget;
    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = fixedTarget.position + pos;
        transform.rotation = fixedTarget.rotation;
    }
}
