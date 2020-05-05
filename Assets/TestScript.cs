using com.leothelegion.Nav;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    NavAgent agent;

    [SerializeField]
    List<Vector2> path;

    [SerializeField]
    Transform goal;
    // Start is called before the first frame update
    void Start()
    {
        
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
        agent.FindPath(s, e);
    }
}
