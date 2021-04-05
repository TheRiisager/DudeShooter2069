using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Node
{
    private Transform target;
    private Enemy ai;

    public Shoot(Transform target, Enemy ai)
    {
        this.ai = ai;
        this.target = target;
    }
    public override NodeState Evaluate()
    {
        ai.transform.LookAt(target);
        nodeState = ai.Shoot() ? NodeState.SUCCESS : NodeState.FAILURE;
        
        return nodeState;
    }
}
