using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnterCover : Node
{
    private Enemy ai;
    private NavMeshAgent agent;

    public EnterCover(Enemy ai, NavMeshAgent agent)
    {
        this.ai = ai;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        Transform coverSpot = ai.GetBestCoverSpot();
        if(coverSpot == null)
        {
            nodeState = NodeState.FAILURE;
            return nodeState;
        }

        if(agent.remainingDistance > 0.5f || agent.destination != coverSpot.position)
        {
            agent.isStopped = false;
            agent.speed = ai.GetFleeingSpeed();
            nodeState = NodeState.RUNNING;
            agent.SetDestination(coverSpot.position);
        }
        else
        {
            agent.isStopped = true;
            agent.speed = ai.GetNormalSpeed();
            nodeState = NodeState.SUCCESS;
        }
        return nodeState;
    }
}
