using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Node
{
    private Enemy ai;
    private float threshold;

    public Health(Enemy ai, float threshold)
    {
        this.ai = ai;
        this.threshold = threshold;
    }
   public override NodeState Evaluate()
   {
       nodeState = ai.GetHealth() <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
       return nodeState;
   }
}
