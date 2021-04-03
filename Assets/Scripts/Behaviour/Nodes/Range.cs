using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Node
{
    private Transform origin;
    private Transform target;
    private float range;

    public Range(Transform origin, Transform target, float range)
    {
        this.origin = origin;
        this.target = target;
        this.range = range;
    }
    public override NodeState Evaluate()
    {
        nodeState = Vector3.Distance(origin.position, target.position) <= range ? NodeState.SUCCESS : NodeState.FAILURE;
        return nodeState;
    }
}
