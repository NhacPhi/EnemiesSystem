using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 dir = MM.Instance.player.transform.position - agent.transform.position;
        //if no neighbors, maintain current alignment
        if (context.Count == 0)
            return dir.normalized;

        //add all points together and average
        Vector2 alignmentMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            Vector2 direection = MM.Instance.player.transform.position - item.transform.position;
            alignmentMove += (Vector2)direection.normalized;
        }
        alignmentMove /= context.Count;

        if(alignmentMove.sqrMagnitude > 0)
        {
            return alignmentMove;
        }
        else
        {
            return Vector2.zero;
        }


    }
}
