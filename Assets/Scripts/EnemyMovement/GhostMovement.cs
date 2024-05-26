using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isMoveRight = false;
    private float moveSpeed = 0.7f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Material hitBlind;
    private Material defaultMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        defaultMaterial = sr.material;
        hitBlind = Resources.Load<Material>("Materials\\HitBlind");

        StartCoroutine(Invicible());
    }

    void Update()
    {
        if (isMoveRight)
        {
            rb.velocity = new Vector2(1f, 0) * moveSpeed;
        }
        else
        {
            rb.velocity = new Vector2(-1f, 0) * moveSpeed;
        }
    }

    private IEnumerator Invicible()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));

        animator.SetBool("Invicible", true);

        yield return new WaitForSeconds(Random.Range(2, 4));

        animator.SetBool("Invicible", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            sr.flipX = !sr.flipX;
            isMoveRight = !isMoveRight;
        }

        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            animator.SetBool("Invicible", false);
            StartCoroutine(Invicible());

            HealthEventSystem.instance.TriggerDamage(1);
            StartCoroutine(HitAnimation());
        }
    }

    private IEnumerator HitAnimation()
    {
        sr.material = hitBlind;
        yield return new WaitForSeconds(0.2f);
        sr.material = defaultMaterial;
    }
}
