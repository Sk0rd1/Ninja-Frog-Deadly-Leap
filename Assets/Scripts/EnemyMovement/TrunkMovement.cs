using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrunkMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isMoveRight = false;
    private float moveSpeed = 0.4f;
    private float bulletSpeed = 400f;

    private bool isAttack = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private GameObject bulletPrefab;
    private PlayerPosition playerPosition;

    private Material hitBlind;
    private Material defaultMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        bulletPrefab = Resources.Load<GameObject>("Enemys\\BulletTrunk");
        playerPosition = GameObject.Find("Player").GetComponent<PlayerPosition>();

        defaultMaterial = sr.material;
        hitBlind = Resources.Load<Material>("Materials\\HitBlind");
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

        Vector3 pos = playerPosition.Get();

        if(pos.y - transform.position.y > 0.2f && pos.y - transform.position.y < 0.8f)
        {
            Attack(pos);
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
