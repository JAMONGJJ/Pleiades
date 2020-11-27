using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CapsuleCollider))]
public class BDDScript : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;

    private Animator animator;
    int layerMask;
    Vector3 destinationPos;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 10;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        destinationPos = new Vector3();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 목적지 도착하면 default 애니메이션
        if (transform.position == destinationPos)
            animator.SetBool("WALK", false);

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(-1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << layerMask))
            {
                animator.SetBool("WALK", true);
                agent.SetDestination(hit.point);
                destinationPos = agent.destination;
            }
        }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.GetTouch(0) && !EventSystem.current.IsPointerOverGameObject(-1)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << layerMask))
            {
                animator.SetBool("WALK", true);
                agent.SetDestination(hit.point);
                destinationPos = agent.destination;
            }
        }
#endif
    }
}




