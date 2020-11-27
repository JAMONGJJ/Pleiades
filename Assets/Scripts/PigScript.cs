using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PigScript : AnimalScript
{
    // Start is called before the first frame update
    void OnEnable()
    {
        animator = gameObject.GetComponent<Animator>();
        time = 10.0f;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0.8f;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MinigamePlayer")
        {
            base.OnTriggerEnter(other);
            GameObject.FindWithTag("GameManager").GetComponent<Minigame3Script>().GetAnimals();
        }
    }

    public override void DestroyItself()
    {
        base.DestroyItself();
    }
}
