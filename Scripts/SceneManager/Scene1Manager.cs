using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    private GameManager gameM;
    private List<GameObject> SSS = new List<GameObject>();
    private GameObject player;
    private GameObject ship;
    private GameObject gate;
    [SerializeField]
    private MonsterM monsterM;
    private int stage;

    private void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        LoadScene1();
    }

    public void LoadScene1()
    {
        player = GameObject.FindWithTag("Player");
        ship = GameObject.FindWithTag("Ship");
        gate = GameObject.FindWithTag("EndGate");
        ship.SetActive(false);
        gate.SetActive(false);

        stage = 0;

        //이동지점
        for (int i = 1; i < 4; i++)
        {
            SSS.Add(GameObject.FindWithTag("SSS" + i));
        }
   
        monsterM.StartMonsterLoad();
        Move();
    }

    public void Move()
    {
        monsterM.StartStage(stage);
        player.transform.position = SSS[stage].transform.position;
        stage++;
    }

    public void Home()
    {
        ship.SetActive(true);
        player.SetActive(false);
    }

    public void End()
    {
        gate.SetActive(true);
    }

    public void GoHome()
    {
        gameM.LoadScene(GameManager.Stage.Two); //GM으로 씬이동
    }
}
