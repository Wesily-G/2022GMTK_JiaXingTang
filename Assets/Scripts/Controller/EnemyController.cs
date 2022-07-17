using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent navMeshAgent;

    private Vector3 targetPos;
    public enum EnemyState
    {
        Idle,
        Move,
        Pursue
    }

    //行动范围
    public float maxX = 4.74f;
    public float minX = -5.62f;
    public float maxZ = 5.92f;
    public float minZ = -6.33f;

    private EnemyState enemyState;

    public EnemyState EnemyState1
    {
        get => enemyState;
        set
        {
            enemyState = value;
            switch (enemyState)
            {
                case EnemyState.Idle:
                    //播放动画
                    //关闭导航
                    //休息一段时间后去巡逻
                    //animator.CrossFadeInFixedTime("Idle", 0.25f);
                    navMeshAgent.enabled = false;
                    Invoke(nameof(GoMove), Random.Range(3f, 10f));
                    break;
                case EnemyState.Move:
                    //播放动画
                    //开启导航
                    //animator.CrossFadeInFixedTime("Move", 0.25f);
                    navMeshAgent.enabled = true;
                    targetPos = GetTargetPos();
                    navMeshAgent.SetDestination(targetPos);
                    break;
                case EnemyState.Pursue:
                    //animator.CrossFadeInFixedTime("Move", 0.25f);
                    navMeshAgent.enabled = true;
                    break;
                
            }
        }
    }


    private void Start()
    {
        //checkCollider.Init(this, 10);
        EnemyState1 = EnemyState.Idle;
    }
    private void Update()
    {
        StateOnUpdate();
    }
    private void StateOnUpdate()
    {
        switch (EnemyState1)
        {

            case EnemyState.Move:
                if (Vector3.Distance(transform.position, targetPos) < 1f)
                {
                    EnemyState1 = EnemyState.Idle;
                }
                break;
            case EnemyState.Pursue:
                if (Vector3.Distance(transform.position, PlayerController.Ins.transform.position) < 1)
                {
                    //EnemyState1 = EnemyState.Attack;
                }
                else
                {
                    navMeshAgent.SetDestination(PlayerController.Ins.transform.position);
                }
                break;

        }
    }

    private void GoMove()
    {
        EnemyState1 = EnemyState.Move;

    }
    /// <summary>
    /// 获取范围内的随机点
    /// </summary>
    /// <returns></returns>
    private Vector3 GetTargetPos()
    {
        return new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneController.Ins.TransitionToBattle();
        }
    }
}
