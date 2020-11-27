using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClientScript : MonoBehaviour
{
    [HideInInspector]
    public GameObject Chair;            // client가 앉을 의자 위치
    [HideInInspector]
    public int code;                        // client간에 구분하기 위한 코드 (default : -1, 이외에는 매장 내 의자의 순서로)
    [HideInInspector]
    public bool served;                     // true : 요리 미니게임 성공해서 서빙 받음(flag값이 2->3) / false : 요리 미니게임 실패해서 서빙 못 받음(flag값이 2->4로 건너뜀)

    [HideInInspector]
    public float timer;
    [HideInInspector]
    public int flag;                         // 0 : 초기상태 / 1 : 걷고 있는 상태 / 2 : 의자에 앉은 상태(주문 넣음, 아직 서빙 안 됨) / 3 : 주문 넣음(서빙 기다리는중)
                                                // 4 : 서빙 받음(일정 시간 지나면 식당에서 나감) / 5 : 음식 다 먹고 일어남 / 6 : 출구로 이동중

    GameObject exitPosition;            // 고객이 음식을 다 먹고 퇴장할 위치
    Animator animator;
    NavMeshAgent agent;

    // Start is called before the first frame update
    private void Start()
    {
        flag = 0;
        exitPosition = GameObject.FindWithTag("ExitPosition");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0.5f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (flag == 0)      // 초기에 손님 오브젝트 생성 후 의자 위치로 이동
        {
            animator.SetBool("Walking", true);
            agent.SetDestination(Chair.transform.position);
            flag = 1;
        }
        else if (flag == 2)     // 의자에 앉은 후 주문
        {
            GameObject.FindWithTag("OrderListUI").GetComponent<OrderListScript>().order(code);
            flag = 3;
        }
        else if (flag == 4)     // 요리 성공, 
        {
            if (code == 0)              // 테이블이 없는 자리에 온 손님 -> 주문 서빙 완료하면 바로 나가는걸로
            {
                animator.SetTrigger("Stand");
                flag++;
            }

            timer += Time.deltaTime;
            if (timer >= 30.0f)             // 요리 성공 후 30초 동안 자리에 있다가 일어남
            {
                animator.SetTrigger("Stand");
                flag = 5;
            }
        }
        else if(flag == 5)          // 손님 출구로 이동
        {
            animator.SetBool("Walking", true);
            agent.SetDestination(exitPosition.transform.position);
            flag = 6;
        }


        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                {
                    if (flag == 1)          // 의자 도착
                    {
                        transform.forward = Chair.transform.forward;
                        animator.SetTrigger("Sit");
                        flag = 2;
                    }
                    else if(flag == 6)      // 출구 도착
                    {
                        Destroy(gameObject);
                        Resources.UnloadUnusedAssets();
                    }
                    else
                        animator.SetBool("Walking", false);
                }
            }

        }
    }
}
