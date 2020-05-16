using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterBeam: Projectile
{
    BoxCollider2D boxCollider2D;
    LineRenderer lineRenderer;

    [SerializeField]
    float rateofExpand = 10f;

    float x = 0;

    protected override void Awake()
    {
        base.Awake();
        rotationOffset = -90;
    }

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();



        boxCollider2D.size = Vector3.zero;
        boxCollider2D.offset = Vector3.zero;
        lineRenderer.SetPosition(1, Vector3.zero);
    }
    
    private void Update()
    {
        
        x += rateofExpand * Time.deltaTime;
        lineRenderer.SetPosition(1, new Vector3(x,0,0));
        boxCollider2D.size = new Vector3(x, 0.125f, 0);
        boxCollider2D.offset = new Vector3(x / 2, 0, 0);
    }
}
