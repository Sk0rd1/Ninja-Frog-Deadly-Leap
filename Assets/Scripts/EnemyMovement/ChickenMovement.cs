using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isMoveRight = false;
    private bool isIdle = false;
    private float moveSpeed = 1f;

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

        animator.SetBool("isRun", true);

        StartCoroutine(ChangeMove());
    }

    void Update()
    {
        if (!isIdle)
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
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator ChangeMove()
    {
        yield return new WaitForSeconds(Random.Range(1, 4));

        Debug.Log("isIdle " + isIdle);

        if(isIdle)
        {
            isIdle = false;
            animator.SetBool("isRun", true);
            Debug.Log(1);
        }
        else
        {
            if (Random.Range(0f, 2f) > 1)
            {
                isIdle = true;
                animator.SetBool("isRun", false);
                Debug.Log(2);
            }
            else
            {
                isMoveRight = !isMoveRight;
                sr.flipX = !sr.flipX;
                Debug.Log(3);
            }
        }

        StartCoroutine(ChangeMove());
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
