using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    //public int BulletMax;           //총알 최대 수
    //public int BulletNow;           //현재 총알 수

    public float BetweenShots;      //발사간격 (ms)
    //public float ReloadTime;        //재장전 속도 (s)
    public float muzzleVelocity;    //총알 속도 
    public float nextShotTime = 0f;     //총알 발사 간격을 위한 변수
    //public float nextReloadTime = 0f;      //총  재장전 간격을 위한 변수

    public bool ReloadValue = false;    //장전중
    public GameObject Bullet;       //총알 오브젝트
    public Transform muzzle;        //총알 발사 위치

    public AudioSource audioSource;

    public GameObject muzzleFlashPrefab;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //audioSource.clip = bgm; //오디오에 bgm이라는 파일 연결

    }
    //총 발사 함수

    public void Shoot()
    {
        //if (BulletNow > 0)
        //{
            //총알 수가 1개 이상인 경우

            if (ReloadValue == false && Time.time > nextShotTime) //ReloadValue 장전중?    nextShotTime 연사속도
            {
                //재장전 시간, 총 발사 간격의 시간이 지난 경우
                nextShotTime = Time.time + BetweenShots / 1000;

                GameObject newBullet = Instantiate(Bullet, muzzle.position, muzzle.rotation) as GameObject;
                newBullet.GetComponent<Bullet>().SetSpeed(muzzleVelocity);
                audioSource.Play(); //오디오 재생

            var muzzleflash = Instantiate(muzzleFlashPrefab, muzzle.position, muzzle.rotation);

            
            Destroy(muzzleflash.gameObject, 0.5f);
                

        }

        //}
        //else
        //{
        //    if (ReloadValue == false)
        //    {
        //        //총알 수 0개, 재장전하지 않음
        //        Reload();
        //    }
        //}
    }

    ////총 재장전 함수
    //public void Reload()
    //{
    //    nextReloadTime = Time.time + 2; //무한 장전 방지?(안씀
    //    ReloadValue = true; //
    //}
}