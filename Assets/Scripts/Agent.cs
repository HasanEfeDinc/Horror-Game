using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    private NavMeshAgent _agent;
    public string targetObjectName; 
    private GameObject target;

    void Start()
    {
        target = GameObject.Find(targetObjectName);
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            _agent.SetDestination(target.transform.position);
        }
    }

}
