using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirMovement))]
public class Arrow : Projectile
{
    protected AirMovement movement_script;
    private Rigidbody2D rb;

    [SerializeField] [Range(0.01f, 0.2f)]
    private float slowDownDuration = 0;

    private Vector2? objectStruckInitialPos = null;

    private static List<Transform> allArrows = new List<Transform>();
    public static List<Transform> AllArrows { get => allArrows; }

    private static string arrowContainerTag = "Arrow Container";

    [SerializeField] private float timerAfterCollision = 0;

    private void Awake()
    {
        movement_script = GetComponent<AirMovement>();
        rb = GetComponent<Rigidbody2D>();
        transform.SetParent(GameObject.FindGameObjectWithTag(arrowContainerTag).transform);
        rotationOffset = -90;

        allArrows.Add(transform);
    }

    public override void SetInMotion(Vector2 direction)
    {
        float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotationOffset;
        transform.Rotate(0, 0, -rotation);
        Vector2 vel = direction * movement_script.speed;
        movement_script.Move(vel);
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
            StartCoroutine(SlowDown());

            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            {
                collision.GetComponent<Health>().TakeDamage(damage);
                transform.SetParent(collision.transform);
            }
        }
    }

    private void OnDestroy()
    {
        allArrows.Remove(transform);
        allArrows.TrimExcess();
    }

    private IEnumerator SlowDown()
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