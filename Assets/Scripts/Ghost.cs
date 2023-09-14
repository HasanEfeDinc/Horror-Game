using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    [SerializeField] private Collider Room1Collider;
    [SerializeField] private GameObject TheGhost;
    [SerializeField] private GameObject TheGhostIdle;
    
    
    private Vector3 randomPoint;
    private int initialAttackChance = 40;
    private int BasedAttackChance = 40;
    private int AttackMultipler = 0;
    
    private bool GhostCanAttackNow = false;
    private bool canInteract = false;
    

    private void Start()
    {
        InvokeRepeating("IncreaseMultipler", 0f, 25f);
        InvokeRepeating("Idle", 0f, 30f);
        StartCoroutine(waittwomin());
        
        
    }

    private void Update()
    {
        
        Debug.Log(Game.Instance.GhostIdleSeen);
    }

    

    void Idle()
    {
        if (GhostCanAttackNow)
        {
            float chance = UnityEngine.Random.Range(30f, 100f);
            if (BasedAttackChance > chance)
            {
                Attack();
            }
            else
            {
                Interact();
            }
        }
    }

    void Attack()
    {
        AttackMultipler = 0;
        randomPoint = new Vector3(
            UnityEngine.Random.Range(Room1Collider.bounds.min.x, Room1Collider.bounds.max.x),
            UnityEngine.Random.Range(Room1Collider.bounds.min.y, Room1Collider.bounds.max.y),
            UnityEngine.Random.Range(Room1Collider.bounds.min.z, Room1Collider.bounds.max.z)
        );
        Instantiate(TheGhost, randomPoint, Quaternion.identity);
        BasedAttackChance = initialAttackChance;
        initialAttackChance -= 5;
        BasedAttackChance -= 5;

    }

    void Interact()
    {
        int InteractChance = 15; 
        float chance = UnityEngine.Random.Range(0f, 100f);
        if (InteractChance > chance)
        {
            canInteract = true;
        }

        if (canInteract)
        {
            StartCoroutine(WaitForIdle());
        }
    }
    
    
    
    void IncreaseMultipler()
    {
        if (GhostCanAttackNow)
        {
            AttackMultipler += 3;
            BasedAttackChance = BasedAttackChance + AttackMultipler;
        }
        
    }
    IEnumerator waittwomin()
    {
        yield return new WaitForSeconds(1f);
        GhostCanAttackNow = true;
    }
    IEnumerator WaitForIdle()
    {
        canInteract = false;
        randomPoint = new Vector3(
            UnityEngine.Random.Range(Room1Collider.bounds.min.x, Room1Collider.bounds.max.x),
            UnityEngine.Random.Range(Room1Collider.bounds.min.y, Room1Collider.bounds.max.y),
            UnityEngine.Random.Range(Room1Collider.bounds.min.z, Room1Collider.bounds.max.z)
        );
        GameObject GhostIdle = Instantiate(TheGhostIdle, randomPoint, Quaternion.identity);
    
        float timer = 0f;
        float maxWaitTime = 10f; 
        while (timer < maxWaitTime)
        {
            if (Game.Instance.GhostIdleSeen)
            {
                yield return new WaitForSeconds(3f);
                Destroy(GhostIdle);
                Game.Instance.GhostIdleSeen = false;
                yield break; 
            }
        
            timer += Time.deltaTime; 
            yield return null; 
        }
    
        
        Destroy(GhostIdle);
    }

    
}
