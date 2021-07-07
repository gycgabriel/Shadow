using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;

    private void FixedUpdate()
    {
        transform.position += (Vector3) direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
        GetComponentInChildren<Animator>().SetFloat("DirectionX", direction.x);
        GetComponentInChildren<Animator>().SetFloat("DirectionY", direction.y);
    }
}
