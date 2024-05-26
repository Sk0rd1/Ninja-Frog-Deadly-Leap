using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isMoveRight = false;
    //private float amplitude = 0.9f;
    //private float frequency = 2f;
    private float moveSpeed = 1f;
    private float timeStay = 2f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float vertiacalDirection = -1;

    private Material hitBlind;
    private Material defaultMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        defaultMaterial = sr.material;
        hitBlind = Resources.Load<Material>("Materials\\HitBlind");

        StartCoroutine(Move());
    }

    /*private IEnumerator Move()
    {
        while (true)
        {
            while (true)
            {
                float verticalMovement = Mathf.Sin(Time.time * frequency + Mathf.PI / 2) * amplitude;

                rb.velocity = new Vector2(0, verticalMovement);

                Debug.Log(verticalMovement);

                if (Mathf.Abs(verticalMovement) < 0.1f || Mathf.Abs(verticalMovement) > 0.99f)
                    break;

                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(timeStay);
        }
    }*/

    private IEnumerator Move()
    {
        float currentTime = 0;

        while (true)
        {
            yield return new WaitForSeconds(timeStay);

            vertiacalDirection = -vertiacalDirection;

            while (currentTime <= 0.7f)
            {
                rb.velocity = new Vector2(0, moveSpeed) * vertiacalDirection;
                currentTime += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            rb.velocity = Vector2.zero;

            currentTime = 0;    
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

        /*if(collision.CompareTag("EnemyFloor"))
        {
            Debug.Log(currentTime);
            StopCoroutine(Move());
            StartCoroutine(Move());
        }*/
    }

    private IEnumerator HitAnimation()
    {
        sr.material = hitBlind;
        yield return new WaitForSeconds(0.2f);
        sr.material = defaultMaterial;
    }
}
