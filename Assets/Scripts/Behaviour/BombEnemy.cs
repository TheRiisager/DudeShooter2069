using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombEnemy : MonoBehaviour, IDamageable
{
    [Header("Enemy parameters")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthRegen;
    [SerializeField] private float chargeRange;
    [SerializeField] private float detonateRange;

    [Header("Settable component references")]
    [SerializeField] private Transform target;
    [SerializeField] private Cover[] covers;

    //internal variables
    private float currentHealth
    {
        get {return currentHealth; }
        set {currentHealth = Mathf.Clamp(value,0,maxHealth);}
    }
    private Transform bestCoverSpot;
    private Node topNode;


    //components
    private NavMeshAgent agent;

    private void Start() {
        currentHealth = maxHealth;
    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Update() {
        currentHealth += Time.deltaTime * healthRegen;
    }

    public bool Detonate()
    {

        if(Vector3.Distance(target.transform.position, agent.transform.position) < detonateRange)
        {
            Debug.Log("boom");
            // explode and damage
            return true;
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
