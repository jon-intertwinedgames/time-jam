using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirMovement), typeof(Rigidbody2D))]
public class Arrow : Projectile
{
    private void Awake()
    {
        movement_script = GetComponent<AirMovement>();
        transform.SetParent(GetArrowContainer());
        rotationOffset = -90;
    }

    public static Transform GetArrowContainer()
    {
        return GameObject.FindGameObjectWithTag("Arrow Container").transform;
    }

    public static Transform[] GetAllActiveArrows()
    {
        Transform arrowContainer_trans = Arrow.GetArrowContainer();
        int numOfArrows = arrowContainer_trans.childCount;
        Transform[] allArrows = new Transform[numOfArrows];

        for (int i = 0; i < numOfArrows; i++)
            allArrows[i] = arrowContainer_trans.GetChild(i);

        return allArrows;
    }

    public static GameObject FindClosestArrowToCursor(Transform[] allArrows)
    {
        Vector2 mouseInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject closestArrow = allArrows[0].gameObject;
        float shortestDistance = ((Vector2)closestArrow.transform.position - mouseInWorldPoint).magnitude;

        for (int i = 1; i < allArrows.Length; i++)
        {
            Transform currentArrow = allArrows[i];
            Vector2 distance = (Vector2)currentArrow.position - mouseInWorldPoint;
            float distanceMagnitude = distance.magnitude;

            if (distanceMagnitude < shortestDistance)
            {
                shortestDistance = distanceMagnitude;
                closestArrow = currentArrow.gameObject;
            }
        }

        return closestArrow;
    }
}