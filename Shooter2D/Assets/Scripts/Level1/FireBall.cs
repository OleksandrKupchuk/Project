using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireBall : MonoBehaviour
{
    [SerializeField] float fireBallSpeed;

    [SerializeField] Rigidbody2D fireBallRigidbody2D;

    Vector2 fireBallDirection;

    private void FixedUpdate()
    {
        fireBallRigidbody2D.velocity = fireBallDirection * fireBallSpeed;
    }

    public void FireBallInitialization(Vector2 direction)
    {
        fireBallDirection = direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
