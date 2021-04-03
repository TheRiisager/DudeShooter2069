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
        }
        if(agent.remainingDistance > 0.5f)
        {
            agent.isStopped = false;
            nodeState = NodeState.RUNNING;
            agent.SetDestination(coverSpot.position);
        }
        else
        {
            agent.isStopped = true;
            nodeState = NodeState.SUCCESS;
        }
        return nodeState;
    }
}
