using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isMoveRight = false;
    private float moveSpeed = 0.4f;
    private float bulletSpeed = 500f;

    private bool isAttack = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private GameObject bulletPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        bulletPrefab = Resources.Load<GameObject>("Enemys\\BulletTrunk");
    }

    void Update()
    {
        if (!isAttack)
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
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(0.09f);
        animator.SetBool("isHit", false);
    }

    public void Attack(Vector3 playerPosition)
    {
        if (!isAttack)
        {
            if (playerPosition.x > transform.position.x)
            {
                if (isMoveRight)
                    StartCoroutine(AttackAnimation());
            }
            else
            {
                if (!isMoveRight)
                    StartCoroutine(AttackAnimation());

            }
        }
    }

    private IEnumerator AttackAnimation()
    {
        isAttack = true;
        animator.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.4f);

        GameObject go = Instantiate<GameObject>(bulletPrefab);

        go.transform.position = transform.position + new Vector3(0, -0.09f, 0.01f);

        Vector2 direction;
        if (isMoveRight)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        go.GetComponent<Bullet>().SetValues(direction, bulletSpeed);

        yield return new WaitForSeconds(0.22f);

        isAttack = false;
        animator.SetBool("isAttack", false);
    }
}
