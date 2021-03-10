using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Player : MonoBehaviour
{
    private int _maxHp = 150;
    private int _nowHp = 150;
    private bool isDead = false;

    public float moveSpeed = 0.5f;     //플레이어의 속도
    public float spin = 1f;
    GunController GunCtrl;

    public int PlayerHP = 50;
    private float PlayerInvin;  // 무적 시간

    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean MoveUp = SteamVR_Input.GetBooleanAction("Up");
    public SteamVR_Action_Boolean MoveDown = SteamVR_Input.GetBooleanAction("Down");

    public SteamVR_Action_Boolean TriggerClick;
    private SteamVR_Input_Sources inputSource;

    public Camera cam; //메인카메라

    public int maxHp
    {
        get
        {
            return _maxHp;
        }
    }
    public int nowHp
    {
        get
        {
            return _nowHp;
        }
        set
        {
            _nowHp = value;
        }
    }

    public void Hit(int damage)
    {
        if(Time.time > PlayerInvin)
        {
            nowHp -= damage;

            if (nowHp <= 0)
            {
                isDead = true;
            }
        }
        
    }
    private void Start()
    {
        GunCtrl = GetComponent<GunController>();

        PlayerInvin = Time.time;
    }
    private void Update()
    {
        if (isDead)
        {
            gameObject.SetActive(false);
        }

        if (MoveUp.GetState(hand))
        {
            Vector3 dir = cam.transform.localRotation * Vector3.forward;
            transform.localRotation = cam.transform.localRotation;
            gameObject.transform.Translate(dir * 0.2f);
        }

        if (MoveDown.GetState(hand))
        {
            Vector3 dir = cam.transform.localRotation * Vector3.back;
            transform.localRotation = cam.transform.localRotation;
            gameObject.transform.Translate(dir * 0.2f);
        }

        TriggerClick.AddOnStateDownListener(Press, inputSource);
    }

    private void Press(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        GunCtrl.Shoot();
    }
}
