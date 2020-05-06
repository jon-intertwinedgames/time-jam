using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float groundDetectionLength = 0;
    private bool isOnGround;
    public bool IsOnGround { get => isOnGround; }

    private void Update()
    {
        isOnGround = CheckIfOnGround();
    }

    public bool CheckIfOnGround()
    {
        Vector2 startPosition = GetStartPosition();
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.right, groundDetectionLength, LayerMask.GetMask("Terrain"));

        if (hit)
            return true;

        return false;
    }

    private Vector2 GetStartPosition()
    {
        Vector2 startPosition = transform.position;
        startPosition.x -= groundDetectionLength / 2;
        return startPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 startPosition = GetStartPosition();
        startPosition.z = Camera.main.transform.position.z + 1;

        Vector3 endPosition = startPosition;
        endPosition.x += groundDetectionLength;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startPosition, endPosition);
    }
}
