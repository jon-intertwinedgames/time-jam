using com.leothelegion.Nav;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterController : MonoBehaviour
{
    NavAgent agent = null;
    AirMovement movement = null;

    [SerializeField]
    List<Vector2> path;

    [SerializeField]
    Transform goal = null;

    [SerializeField]
    float minFlyingAltitude = 10f;

    [SerializeField]
    [Range(0.00001f, 0.5f)]
    private float floatyness = 0.01f;

    Vector2 floatyMovement = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavAgent>();
        movement = GetComponent<AirMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float xa = 0;
        float ya = 0;

        Vector2Int s = new Vector2Int(
            Mathf.FloorToInt(this.transform.position.x),
            Mathf.FloorToInt(this.transform.position.y)
            );

        Vector2Int e = new Vector2Int(
            Mathf.FloorToInt(goal.position.x),
            Mathf.FloorToInt(goal.position.y)
            );
        path =  agent.FindPath(s, e);

        if (path != null)
        {
            if (path.Count > 0)
            {
                var point = path[0];

                float x = this.transform.position.x;
                float y = this.transform.position.y;


                if (Vector2.Distance(point, this.transform.position) > 0.1f)
                {
                    if (point.x < x)
                        xa = -1;
                    else if ((point.x > x))
                        xa = 1;
                    else
                        xa = 0;

                    if(y > minFlyingAltitude)
                    {
                        if (point.y < y)
                            ya = -1;
                        else if ((point.y > y))
                            ya = 1;
                        else
                            ya = 0;
                    }
                    else if(y < minFlyingAltitude)
                    {
                        ya = 1;
                    }
                    else if(y == minFlyingAltitude)
                    {
                        ya = 0;
                    }

                    
                }
            }
        }

        floatyMovement = Vector2.Lerp(floatyMovement, new Vector2(xa, ya), floatyness);

        movement.Move(new Vector2(floatyMovement.x, floatyMovement.y));

    }
    //Called by Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var drawRange = 10;
        Gizmos.DrawLine(
            new Vector3(this.transform.position.x - drawRange, minFlyingAltitude, 0),
            new Vector3(this.transform.position.x + drawRange, minFlyingAltitude, 0)
            );
        if (path == null) return;
        Gizmos.color = Color.green;
        foreach (var p in path)
        {
           Gizmos.DrawWireSphere(new Vector2(p.x, p.y), 0.1f);
        }
    }
}
