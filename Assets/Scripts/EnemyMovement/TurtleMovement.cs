using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthEventSystem.instance.TriggerDamage(1);
            StartCoroutine(HitAnimation());
        }
    }

    private IEnumerator HitAnimation()
    {
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(0.09f);
        animator.SetBool("isHit", false);
    }
}
