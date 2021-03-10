using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour
{
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean MoveUp = SteamVR_Input.GetBooleanAction("Up");
    public SteamVR_Action_Boolean MoveDown = SteamVR_Input.GetBooleanAction("Down");
    public SteamVR_Action_Boolean MoveLeft = SteamVR_Input.GetBooleanAction("Left");
    public SteamVR_Action_Boolean MoveRight = SteamVR_Input.GetBooleanAction("Right");

    public float moveSpeed = 1f;

    void Update()
    {
        if (MoveLeft.GetState(hand))
        {
            transform.Translate(Vector3.left * moveSpeed);
        }

        if (MoveRight.GetState(hand))
        {
            transform.Translate(-Vector3.left * moveSpeed);
        }

        if (MoveUp.GetState(hand))
        {
            transform.Translate(Vector3.forward * moveSpeed);
        }

        if (MoveDown.GetState(hand))
        {
            transform.Translate(-Vector3.forward * moveSpeed);
        }
    }
}
