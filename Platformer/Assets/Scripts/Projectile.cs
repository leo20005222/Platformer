using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Move the projectile to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy the projectile if it goes off-screen (adjust based on your game)
        if (transform.position.x < -10f) // Change -10f to the left boundary of your gameplay
        {
            Destroy(gameObject);
        }
    }
}
