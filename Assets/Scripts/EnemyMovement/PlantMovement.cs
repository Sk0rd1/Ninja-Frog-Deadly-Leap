using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isMoveRight = false;
    private float bulletSpeed = 550f;

    private bool isAttack = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private GameObject bulletPrefab;
    private PlayerPosition playerPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        bulletPrefab = Resources.Load<GameObject>("Enemys\\BulletTrunk");
        playerPosition = GameObject.Find("Player").GetComponent<PlayerPosition>();
    }

    void Update()
    {
        Vector3 pos = playerPosition.Get();

        if (pos.y - transform.position.y > 0.4f && pos.y - transform.position.y < 0.8f)
        {
            Attack(pos);
        }

        if (pos.x > transform.position.x)
        {
            if (!isMoveRight)
            {
                isMoveRight = !isMoveRight;
                sr.flipX = !sr.flipX;
            }
        }
        else
        {
            if (isMoveRight)
            {
                isMoveRight = !isMoveRight;
                sr.flipX = !sr.flipX;
            }
        }

        Debug.Log(isMoveRight + " " + pos.x + " " + transform.position.x);
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
        animator.SetBool("isAttack", false);
        StopCoroutine(AttackAnimation());
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

        yield return new WaitForSeconds(1.24f);

        GameObject go = Instantiate<GameObject>(bulletPrefab);

        go.transform.position = transform.position + new Vector3(0, -0.085f, 0.01f);

        Vector2 direction;
        if (isMoveRight)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        go.GetComponent<Bullet>().SetValues(direction, bulletSpeed);

        yield return new WaitForSeconds(0.76f);

        isAttack = false;
        animator.SetBool("isAttack", false);
    }
}
