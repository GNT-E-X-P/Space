using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    private int damage = 10;
    float Speed;
    float time;

    // Use this for initialization
    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
        time = Time.time + 2;   // 총알 생존 시간
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        if (Time.time > time) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MonsterController>())
        {
            other.GetComponent<MonsterController>().Hit(damage);
            GameObject newnhit = Instantiate(hitEffect, this.transform.position, this.transform.rotation) as GameObject;

            Destroy(this);   // 총알 파괴
        }
        else if (other.GetComponent<BossController>())
        {
            other.GetComponent<BossController>().Hit(damage);
            GameObject newnhit = Instantiate(hitEffect, this.transform.position, this.transform.rotation) as GameObject;

            Destroy(this);
        }
    }
}