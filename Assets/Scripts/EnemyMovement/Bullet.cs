using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float moveSpeed;

    private float currentDistance = 0;
    private float maxDistance = 2500;

    private Rigidbody2D rb;
    private SpriteRenderer sr;  

    public void SetValues(Vector2 direction, float moveSpeed)
    {
        this.direction = direction;
        this.moveSpeed = moveSpeed;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (direction.x > 0)
            sr.flipX = true;
    }

    void Update()
    {
        rb.velocity = direction.normalized * moveSpeed * Time.deltaTime;
        currentDistance += Mathf.Abs(direction.magnitude * moveSpeed * Time.deltaTime);

        if (currentDistance > maxDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthEventSystem.instance.TriggerDamage(1);
            Destroy(gameObject);
        }
    }
}
