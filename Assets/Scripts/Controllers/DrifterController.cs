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

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavAgent>();
        movement = GetComponent<AirMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int s = new Vector2Int(
            Mathf.FloorToInt(this.transform.position.x),
            Mathf.FloorToInt(this.transform.position.y)
            );

        Vector2Int e = new Vector2Int(
            Mathf.FloorToInt(goal.position.x),
            Mathf.FloorToInt(goal.position.y)
            );
        path =  agent.FindPath(s, e);

        float xa = 0;
        float ya = 0;

        if (path != null)
        {
            if (path.Count > 0)
            {
                var point = path[0];

                float x = this.transform.position.x;
                float y = this.transform.position.y;


                //if (Vector2.Distance(point, this.transform.position) > 0.1f)
                {
                    if (point.x < x)
                        xa = -1;
                    else if ((point.x > x))
                        xa = 1;
                    else
                        xa = 0;

                    if (point.y < y)
                        ya = -1;
                    else if ((point.y > y))
                        ya = 1;
                    else
                        ya = 0;
                }
            }
        }

        movement.Move(new Vector2(xa, ya));

    }
    //Called by Unity
    private void OnDrawGizmosSelected()
    {
        if (path == null) return;
        Gizmos.color = Color.green;
        foreach (var p in path)
        {
           Gizmos.DrawWireSphere(new Vector2(p.x, p.y), 0.1f);
        }
    }
}
