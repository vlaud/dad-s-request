using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : CharacterMovement
{
    protected void MoveToPositionByNav(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        if(NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
        {
            switch(path.status)
            {
                case NavMeshPathStatus.PathComplete:
                    // 갈수 있음
                    break;
                case NavMeshPathStatus.PathPartial:
                    // 가다 막힘
                    break;
                case NavMeshPathStatus.PathInvalid:
                    // 못감
                    break;
            }
            StopAllCoroutines();
            StartCoroutine(DrawingPath(path.corners));
            StartCoroutine(MovingByPath(path.corners));
        }

    }

    IEnumerator DrawingPath(Vector3[] path)
    {
        while (true)
        {
            for (int i = 0; i < path.Length -1; ++i)
            {
                Debug.DrawLine(path[i], path[i + 1], Color.red);
            }
            yield return null;
        }
    }
    IEnumerator MovingByPath(Vector3[] path)
    {
        int i = 1;
        while (i < path.Length)
        {
            bool bDone = false;
            MoveToPosition(path[i], () => bDone = true);
            while (!bDone)
            {
                yield return null;
            }
            ++i;
        }
    }
}
