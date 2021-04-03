using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy parameters")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float ShotDamage;

    [Header("Settable component references")]
    [SerializeField] private Transform target;

    //internal variables
    private float currentHealth;
    private Transform bestCoverSpot;


    //components
    private NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }

    public void setBestCoverSpot(Transform coverSpot)
    {
        bestCoverSpot = coverSpot;
    }

    public bool Shoot()
    {
        Debug.Log("Bang!");
        RaycastHit hit;
        Vector3 direction = transform.position - target.position;
        if(Physics.Raycast(transform.position, direction, out hit))
        {
            if(hit.collider.transform == target)
            {
                IDamageable objectToDamage = hit.collider.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
                if(objectToDamage != null)
                {
                    objectToDamage.TakeDamage(ShotDamage);
                    return true;
                }
            }
        }
        return false;
    }

    public void Kill()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
