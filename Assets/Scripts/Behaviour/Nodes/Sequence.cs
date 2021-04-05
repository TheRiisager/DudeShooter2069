using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    private List<Node> nodes;

    public Sequence(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        bool isNodeRunning = false;
        foreach(Node node in nodes)
        {
            switch(node.Evaluate())
            {
                case NodeState.RUNNING:
                    isNodeRunning = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    nodeState = NodeState.FAILURE;
                    return nodeState;
                default:
                    break;
            }
        }

        nodeState = isNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return nodeState;
    }
}
