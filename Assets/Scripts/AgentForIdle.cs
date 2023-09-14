using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentForIdle : MonoBehaviour
{
    private NavMeshAgent _agent;
    public string targetObjectName; // Inspector'da hedef nesnenin adını girin.
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
            if (Game.Instance.GhostIdleSeen)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Idle");
                _agent.SetDestination(target.transform.position);
            }
        }
    }

}