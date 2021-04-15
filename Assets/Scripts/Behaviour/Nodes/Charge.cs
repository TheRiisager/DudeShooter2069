using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charge : Node
{
    private BombEnemy ai;
    private NavMeshAgent agent;
    private Transform target;

    public Charge(BombEnemy ai, NavMeshAgent agent, Transform target)
    {
        this.ai = ai;
        this.agent = agent;
        this.target = target;
    }
    public override NodeState Evaluate()
    {
        if(target == null)
        {
            nodeState = NodeState.FAILURE;
            return nodeState;
        }

        if(agent.remainingDistance > 0.5f || agent.destination != target.position)
        {
            agent.isStopped = false;
            nodeState = NodeState.RUNNING;
            agent.SetDestination(target.position);
        }
        else
        {
            agent.isStopped = true;
            nodeState = NodeState.SUCCESS;
        }
        return nodeState;
    }
}
