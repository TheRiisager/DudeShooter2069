using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    private List<Node> nodes;

    public Selector(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach(Node node in nodes)
        {
            switch(node.Evaluate())
            {
                case NodeState.RUNNING:
                    nodeState = NodeState.RUNNING;
                    return nodeState;
                case NodeState.SUCCESS:
                    nodeState = NodeState.SUCCESS;
                    return nodeState;
                case NodeState.FAILURE:
                    break;
                default:
                    break;
            }
        }
        nodeState = NodeState.FAILURE;
        return nodeState;
    }
}
