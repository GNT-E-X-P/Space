using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Scene1Manager scene1Manager;

    public enum CurrentState { idle, trace, attack, dead, PlayerDie };
    public CurrentState curState = CurrentState.idle;   //초기상태

    private Transform _transform;   //몹 위치
    private Transform playerTransform;  //player 위치
    //private NavMeshAgent nvAgent;   //내비
    private Animator _animator; //
    private GameObject player;      // 플레이어 오브젝트
    private Player playerScript;    // 플레이어 스크립트
    private CapsuleCollider capsule;

    //private Random random;
    private int att;


    public float traceDist = 15.0f; // 추적 사정거리
    public float attackDist = 3.2f; // 공격 사정거리
    public int enemyHp;
    public int enemyDamages;

    // 사망 여부
    private bool isDead = false;

    // Use this for initialization
    void Start()
    {
        scene1Manager = GameObject.Find("Scene1Manager").GetComponent<Scene1Manager>();

        _transform = this.gameObject.GetComponent<Transform>(); //내 위치
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();   //지정한 태그를 달고 있는 게임오브젝트를 찾음
        //nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();   //객체에 등록된 Animator를 가져옴
        capsule = this.gameObject.GetComponent<CapsuleCollider>();

        player = GameObject.FindWithTag("Player");  // 플레이어 오브젝트에 플레이어 등록
        playerScript = player.GetComponent<Player>();   // 플레이어 스크립트 가져옴(공격 판정에 사용)

        // 추적 대상의 위치를 설정하면 바로 추적 시작
        // nvAgent.destination = playerTransform.position;

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTransform.position, _transform.position);

            if (curState == CurrentState.dead) yield break;

            if (dist <= attackDist)
            {
                curState = CurrentState.attack;
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.trace;
            }
            else
            {
                curState = CurrentState.idle;
            }
        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    //nvAgent.ResetPath();
                    _animator.SetBool("isTrace", false);
                    _animator.SetBool("isAttack1", false);
                    _animator.SetBool("isAttack2", false);
                    _animator.SetBool("isAttack3", false);
                    break;
                case CurrentState.trace:
                    //nvAgent.SetDestination(playerTransform.position);
                    //_animator.SetBool("Walk", true);
                    _animator.SetBool("isTrace", true);
                    _animator.SetBool("isAttack1", false);
                    _animator.SetBool("isAttack2", false);
                    _animator.SetBool("isAttack3", false);
                    break;
                case CurrentState.attack:
                    //_animator.SetBool("Attack", true);
                    att = Random.Range(1, 3);
                    GameObject.FindWithTag("Player").SendMessage("TakeDamage");
                    _animator.SetBool("isTrace", false);
                    _animator.SetBool("isAttack" + att, true);
                    //if (playerScript.PlayerHP <= 0) curState = CurrentState.PlayerDie;
                    break;
                case CurrentState.dead:
                    _animator.SetBool("isDeath", true);
                    capsule.enabled = false;
                    //nvAgent.ResetPath();
                    yield return new WaitForSeconds(2f);
                    scene1Manager.End();
                    isDead = true;
                    break;

            }
            if (curState == CurrentState.dead) Destroy(gameObject);



            yield return null;
        }
    }

    public void Hit(int damages)
    {
        enemyHp -= damages;

        if (enemyHp <= 0)
        {
            StopAllCoroutines();    // 루틴 멈춤
            curState = CurrentState.dead;    // 상태로 전환
            StartCoroutine(CheckStateForAction());  // 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().Hit(enemyDamages);
        }
    }

}
