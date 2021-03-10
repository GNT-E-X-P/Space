using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterController : MonoBehaviour
{

    public enum CurrentState { idle, trace, attack, dead, PlayerDie };
    public CurrentState curState = CurrentState.idle;   //초기상태

    private Transform _transform;   //몹 위치
    private Transform playerTransform;  //player 위치
    private NavMeshAgent nvAgent;   //내비
    private Animator _animator; //
    private GameObject player;      // 플레이어 오브젝트
    private Player playerScript;    // 플레이어 스크립트
    private CapsuleCollider capsule;

    public float traceDist = 15.0f; // 추적 사정거리
    public float attackDist = 3.2f; // 공격 사정거리
    public int enemyHp;
    public int enemyDamages;

    // 사망 여부
    private bool isDead = false;

    // Use this for initialization
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>(); //내 위치
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();   //지정한 태그를 달고 있는 게임오브젝트를 찾음
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
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
                    //nvAgent.Stop();
                    nvAgent.ResetPath();
                    //_animator.SetBool("Idle", true);
                    _animator.SetBool("isTrace", false);
                    _animator.SetBool("isAttack", false);
                    break;
                case CurrentState.trace:
                    //nvAgent.destination = playerTransform.position;
                    //nvAgent.Resume();
                    nvAgent.SetDestination(playerTransform.position);
                    //_animator.SetBool("Walk", true);
                    _animator.SetBool("isTrace", true);
                    _animator.SetBool("isAttack", false);
                    break;
                case CurrentState.attack:
                    //_animator.SetBool("Attack", true);
                    _animator.SetBool("isTrace", false);
                    _animator.SetBool("isAttack", true);
                    //if (playerScript.PlayerHP <= 0) curState = CurrentState.PlayerDie;
                    break;
                case CurrentState.dead:
                    _animator.SetBool("isDeath", true);
                    nvAgent.ResetPath();
                    capsule.enabled = false;
                    yield return new WaitForSeconds(2f);
                    isDead = true;
                    break;

            }
            if (curState == CurrentState.dead) Destroy(gameObject);

            yield return null;
        }
    }

    public void Hit(int damages)
    {

        if (enemyHp > 0)
        {
            enemyHp -= damages;
        }

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