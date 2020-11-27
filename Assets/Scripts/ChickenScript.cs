using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenScript : AnimalScript
{
    private void OnEnable()
    {
        animator = gameObject.GetComponent<Animator>();
        time = 10.0f;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0.8f;
    }

    private new void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time >= 10.0f)
        {
            time = 0.0f;
            animator.SetBool("Walk", true);
            destinationPos = new Vector3(UnityEngine.Random.Range(-9.0f, 9.0f), 0.0f, UnityEngine.Random.Range(-12.0f, 7.0f));
            agent.destination = destinationPos;
        }

        // 목적지에 도착했을 경우 애니메이션 전환
        if (transform.position == destinationPos)
            animator.SetBool("Walk", false);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MinigamePlayer")
        {
            base.OnTriggerEnter(other);
            GameObject.FindWithTag("GameManager").GetComponent<Minigame1Script>().GetAnimals();
        }
    }

    public override void DestroyItself()
    {
        base.DestroyItself();
    }
}
