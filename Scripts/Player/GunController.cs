using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunController : MonoBehaviour
{

    public Gun newGun;

    // Use this for initialization
    void Start()
    {
        //newGun.BulletMax = 30;    // 최대 총알 수
        //newGun.BulletNow = 30;    // 현재 총알 수
        newGun.BetweenShots = 100f; // 발사간격(ms)
        //newGun.ReloadTime = 2f;     // 재장전 속도(s)
        newGun.muzzleVelocity = 40f;// 총알 속도
        newGun.ReloadValue = false; // 장전중?
    }

    void Update()
    {
        //if (newGun.ReloadValue == true && Time.time >= newGun.nextReloadTime)
        //{
        //    newGun.ReloadValue = false;
        //    newGun.BulletNow = newGun.BulletMax;

        //    Debug.Log("RELOAD GUN!");
        //}
    }

    public void Shoot()
    {
        newGun.Shoot();
    }

    //public void Reload()
    //{
    //    newGun.Reload();
    //}
}