using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterM : MonoBehaviour
{
    private GameObject S1_Spawn;
    private GameObject S1_Enemy;
    private int S1_HP = 100;
    private int S1_Power = 0;
    public GameObject S1_EnemyPrefabs;

    private GameObject[] S2_SpawnPool;
    private List<GameObject> S2_EnemyPool = new List<GameObject>();
    private int S2_EnemyCnt = 18;
    private int S2_HP = 30;
    private int S2_power = 10;
    public GameObject S2_EnemyPrefabs;

    private GameObject[] S3_SpawnPool;
    private List<GameObject> S3_EnemyPool = new List<GameObject>();
    private int S3_EnemyCnt = 19;
    private int S3_HP = 50;
    private int S3_power = 15;
    public GameObject S3_EnemyPrefabs;

    private GameObject boss;
    private Transform bossSpawn;
    private int bossHp = 150;
    private int bossPower = 25;
    public GameObject bossPrefab;

    public void StartMonsterLoad()
    {
        StartCoroutine(MonsterLoad());
    }

    public void StartStage(int stage)
    {
        if(stage == 0)
        {
            Stage1Start();
            StartCoroutine(MonsterOff(stage));
        }
        else if(stage == 1)
        {
            Stage2Start();
            StartCoroutine(MonsterOff(stage));
        }
        else if(stage == 2)
        {
            Stage3Start();
            StartCoroutine(MonsterOff(stage));
        }
    }

    private void Stage1Start()
    {
        S1_Enemy.transform.position = S1_Spawn.transform.position;
        S1_Enemy.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        S1_Enemy.SetActive(true);
    }

    private void Stage2Start()
    {
        int i = 0;
        foreach (GameObject Enemy in S2_EnemyPool)
        {
            Enemy.transform.position = S2_SpawnPool[i].transform.position;
            Enemy.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            Enemy.SetActive(true);
            i++;
        }
    }

    private void Stage3Start()
    {

        int i = 0;
        foreach (GameObject Enemy in S3_EnemyPool)
        {
            Enemy.transform.position = S3_SpawnPool[i].transform.position;
            Enemy.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            Enemy.SetActive(true);
            i++;
        }
        boss.SetActive(true);
    }

    IEnumerator MonsterLoad()
    {
        S1_Spawn = GameObject.FindGameObjectWithTag("S1Respawn");
        S1_Enemy = (GameObject)Instantiate(S1_EnemyPrefabs);
        S1_Enemy.GetComponent<MonsterController>().enemyHp = S1_HP;
        S1_Enemy.GetComponent<MonsterController>().enemyDamages = S1_Power;
        S1_Enemy.SetActive(false);


        for (int i = 0; i < S2_EnemyCnt; i++)
        {
            GameObject Enemy = (GameObject)Instantiate(S2_EnemyPrefabs);
            Enemy.GetComponent<MonsterController>().enemyHp = S2_HP;
            Enemy.GetComponent<MonsterController>().enemyDamages = S2_power;
            Enemy.name = "S2Enemy_" + i.ToString();
            Enemy.SetActive(false);
            S2_EnemyPool.Add(Enemy);
        }
        S2_SpawnPool = GameObject.FindGameObjectsWithTag("S2Respawn");


        for (int i = 0; i < S3_EnemyCnt; i++)
        {
            GameObject Enemy = (GameObject)Instantiate(S3_EnemyPrefabs);
            Enemy.GetComponent<MonsterController>().enemyHp = S3_HP;
            Enemy.GetComponent<MonsterController>().enemyDamages = S3_power;
            Enemy.name = "S3Enemy_" + i.ToString();
            Enemy.SetActive(false);
            S3_EnemyPool.Add(Enemy);
        }
        S3_SpawnPool = GameObject.FindGameObjectsWithTag("S3Respawn");

        boss = (GameObject)Instantiate(bossPrefab);
        boss.GetComponent<BossController>().enemyHp = bossHp;
        boss.GetComponent<BossController>().enemyDamages = bossPower;
        boss.transform.position = new Vector3(1726f, 0f, 73f);
        boss.SetActive(false);

        yield return null;
    }

    IEnumerator MonsterOff(int stage)
    {
        if(stage == 0)
        {
            foreach (GameObject Enemy in S2_EnemyPool)
            {
                Enemy.SetActive(false);
            }
            foreach (GameObject Enemy in S3_EnemyPool)
            {
                Enemy.SetActive(false);
            }
            if(boss != null)    boss.SetActive(false);
        }
        else if(stage == 1)
        {
            S1_Enemy.SetActive(false);
            foreach (GameObject Enemy in S3_EnemyPool)
            {
                Enemy.SetActive(false);
            }
            if (boss != null) boss.SetActive(false);

        }
        else if (stage == 2)
        {
            S1_Enemy.SetActive(false);
            foreach (GameObject Enemy in S2_EnemyPool)
            {
                Enemy.SetActive(false);
            }
        }
        
        yield return null;
    }
}
