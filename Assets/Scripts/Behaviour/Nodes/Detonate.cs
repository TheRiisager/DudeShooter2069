using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonate : Node
{
    private Transform target;
    private BombEnemy ai;

    public Detonate(Transform target, BombEnemy ai)
    {
        this.ai = ai;
        this.target = target;
    }
    public override NodeState Evaluate()
    {
        ai.transform.LookAt(target);
        nodeState = ai.Detonate() ? NodeState.SUCCESS : NodeState.FAILURE;
        
        return nodeState;
    }
}
