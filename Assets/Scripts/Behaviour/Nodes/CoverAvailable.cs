using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoverAvailable : Node
{
    private Cover[] covers;
    private Transform target;
    private Enemy ai;

    public CoverAvailable(Cover[] covers, Transform target, Enemy ai)
    {
        this.covers = covers;
        this.target = target;
        this.ai = ai;
    }
    public override NodeState Evaluate()
    {
        Transform bestSpot = FindBestSpot();
        ai.setBestCoverSpot(bestSpot);
        nodeState = bestSpot != null ? NodeState.SUCCESS : NodeState.FAILURE;
        return nodeState;
    }

    private Transform FindBestSpot()
    {
        if(ai.GetBestCoverSpot() != null)
        {
            if(IsSpotValid(ai.GetBestCoverSpot()))
            {
                return ai.GetBestCoverSpot();
            }
        }
        Transform bestSpot = null;
        float bestAngle = 0f;
        for(int i = 0; i < covers.Length; i++)
        {
            Transform bestSpotInCover = FindBestSpotInCover(covers[i], ref bestAngle);
            if(bestSpot != null)
            {
                float currentDistance = Vector3.Distance(ai.transform.position, bestSpot.position);
                float newDistance = Vector3.Distance(ai.transform.position, bestSpotInCover.position);

                if(newDistance < currentDistance)
                {
                    bestSpot = bestSpotInCover;
                }
            } else{
                bestSpot = bestSpotInCover;
            }
        }
        return bestSpot;
    }

    private Transform FindBestSpotInCover(Cover cover, ref float bestAngle)
    {
        Transform[] spots = cover.getCoverSpots();
        Transform bestSpot = null;

        for(int i = 0; i < spots.Length; i++)
        {
            if(IsSpotValid(spots[i]))
            {
                float angle = Vector3.Angle(spots[i].forward, target.forward);
                if(angle > bestAngle)
                {
                    bestAngle = angle;
                    bestSpot = spots[i];
                }
            }
        }
        return bestSpot;
    }

    private bool IsSpotValid(Transform spot)
    {
        RaycastHit hit;
        Vector3 direction = target.position -spot.position;

        if(Physics.Raycast(spot.position, direction, out hit))
        {
            if(hit.collider.transform != target)
            {
                return true;
            }
        }
        return false;
    }
}
