using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isMoveRight = false;
    private float amplitude = 0.8f;
    private float frequency = 2f;
    private float moveSpeed = 0.5f;

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
    }

    void Update()
    {
        float verticalMovement = Mathf.Sin(Time.time * frequency + Mathf.PI / 2) * amplitude;   

        if (isMoveRight)
        {
            rb.velocity = new Vector2(moveSpeed, verticalMovement);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, verticalMovement);
        }
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
