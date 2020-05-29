using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float groundDetectionLength = 0;
    private bool isOnGround;
    public bool IsOnGround { get => isOnGround; }

    [SerializeField]
    private Transform player_trans;

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
        Vector2 startPosition = player_trans.position + transform.localPosition;
        startPosition.x -= groundDetectionLength / 2;
        return startPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 startPosition = GetStartPosition();
        Gizmos.DrawLine(startPosition,
            startPosition + (Vector3.right * this.groundDetectionLength));
    }
}
