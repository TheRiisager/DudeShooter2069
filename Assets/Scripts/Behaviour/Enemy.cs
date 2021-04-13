using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy parameters")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthRegen;
    [SerializeField] private float ShotDamage;
    [SerializeField] private float fleeingSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fleeingThreshold;
    [SerializeField] private float detectionRange;

    [Header("Settable component references")]
    [SerializeField] private Transform target;
    [SerializeField] private Cover[] covers;

    //internal variables
    private float currentHealth;
    private Transform bestCoverSpot;
    private Node topNode;
    public HealthBar healthBar;


    //components
    private NavMeshAgent agent;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Called before start
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        ConstructBehaviourTree();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            topNode.Evaluate();
            currentHealth = Mathf.Clamp(currentHealth + healthRegen * Time.deltaTime, 0, maxHealth);
        }
    }

    private void ConstructBehaviourTree()
    {
        CoverAvailable coverAvailable = new CoverAvailable(covers, target, this);
        EnterCover enterCover = new EnterCover(this, agent);
        Health health = new Health(this, fleeingThreshold);
        Range range = new Range(this.transform, target, detectionRange);
        Shoot shoot = new Shoot(target, this);
        Chase chase = new Chase(this, agent, target);

        Inverter healthInverter = new Inverter(health);

        Sequence takeCoverSequence = new Sequence(new List<Node> { health, coverAvailable, enterCover });
        Sequence attackSequence = new Sequence(new List<Node> { range, healthInverter, chase, shoot });

        Selector topSelector = new Selector(new List<Node> { takeCoverSequence, attackSequence });
        topNode = topSelector;


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

    public float GetFleeingSpeed()
    {
        return fleeingSpeed;
    }

    public float GetNormalSpeed()
    {
        return normalSpeed;
    }

    public bool Shoot()
    {

        RaycastHit hit;
        Vector3 direction = transform.forward;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.transform == target)
            {
                IDamageable objectToDamage = hit.collider.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
                if (objectToDamage != null)
                {
                    objectToDamage.TakeDamage(ShotDamage * Time.deltaTime);
                    return true;
                }
            }
        }
        return false;
    }

    public void Kill()
    {
        Destroy(transform.gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0.0f)
        {
            Kill();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(agent.destination, 1);
    }
}
