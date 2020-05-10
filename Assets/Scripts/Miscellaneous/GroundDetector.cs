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
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.down, groundDetectionLength, LayerMask.GetMask("Terrain"));

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(this.transform.position
            + (Vector3.down * this.groundDetectionLength)
            , new Vector3(1,1,1) * 1f);
    }
}
