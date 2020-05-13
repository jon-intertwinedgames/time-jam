using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirMovement))]
public class Arrow : Projectile
{
    private Rigidbody2D rb;

    [SerializeField] [Range(0.01f, 0.2f)]
    private float slowDownDuration = 0;

    private Vector2? objectStruckInitialPos = null;

    private static List<Transform> allArrows = new List<Transform>();
    public static List<Transform> AllArrows { get => allArrows; }

    private static string arrowContainerTag = "Arrow Container";

    [SerializeField] private float timerAfterCollision = 0;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        transform.SetParent(GameObject.FindGameObjectWithTag(arrowContainerTag).transform);
        rotationOffset = -90;

        allArrows.Add(transform);
    }

    public static GameObject FindClosestArrowToCursor()
    {
        Vector2 mouseInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject closestArrow = allArrows[0].gameObject;
        float shortestDistance = ((Vector2)closestArrow.transform.position - mouseInWorldPoint).magnitude;

        for (int i = 1; i < allArrows.Count; i++)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectStruckInitialPos == null)
        {
            objectStruckInitialPos = collision.transform.position;
            rb.velocity = Vector2.zero;
            //StartCoroutine(SlowDownAfterCollision());
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            Destroy(rb);/// I believe this shouldn't be used since it could break scripts but it works..


            if (collision.tag == "Enemy")
            {
                transform.SetParent(collision.transform);
            }
        }
    }

    private void OnDestroy()
    {
        allArrows.Remove(transform);
        allArrows.TrimExcess();
    }

    private IEnumerator SlowDownAfterCollision()
    {
        float timer = 0;
        float speed = rb.velocity.magnitude;
        Destroy(rb);
        
        float currentSpeed = 1;

        GetComponent<SelfDestruct>().ResetWithNewTimer(timerAfterCollision);

        while (currentSpeed != 0)
        {
            currentSpeed = Mathf.Lerp(speed, 0, timer / slowDownDuration);
            transform.Translate(currentSpeed * Time.deltaTime, 0, 0, Space.Self);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}