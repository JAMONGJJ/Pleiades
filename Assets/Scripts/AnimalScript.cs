using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalScript : MonoBehaviour
{
    [SerializeField]
    protected GameObject touchEffect;              // 동물들 터치했을때 이펙트

    protected Animator animator;
    protected NavMeshAgent agent;
    protected float time;
    protected Vector3 destinationPos;
    
    protected virtual void FixedUpdate()
    {
        time += Time.deltaTime;

        // 10초마다 랜덤한 위치로 이동
        if (time >= 10.0f)
        {
            time = 0.0f;
            animator.SetBool("IsWalk", true);
            animator.SetBool("IsIdle", false);
            destinationPos = new Vector3(UnityEngine.Random.Range(-9.0f, 9.0f), 0.0f, UnityEngine.Random.Range(-12.0f, 7.0f));
            agent.destination = destinationPos;
        }

        // 목적지에 도착했을 경우 애니메이션 전환
        if (gameObject.transform.position == destinationPos)
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsWalk", false);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(touchEffect, transform.position, Quaternion.identity);
    }

    public virtual void DestroyItself()
    {
        Destroy(gameObject);
        Resources.UnloadUnusedAssets();
    }
}
