using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isMoveRight = false;
    private float bulletSpeed = 200f;

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
        bulletPrefab = Resources.Load<GameObject>("Enemys\\BulletPlant");
        playerPosition = GameObject.Find("Player").GetComponent<PlayerPosition>();

        defaultMaterial = sr.material;
        hitBlind = Resources.Load<Material>("Materials\\HitBlind");
    }

    void Update()
    {
        Vector3 pos = playerPosition.Get();

        if (pos.y - transform.position.y > -0.2f && pos.y - transform.position.y < 0.8f)
        {
            Debug.Log("Attack");
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

        yield return new WaitForSeconds(0.62f);

        GameObject go = Instantiate<GameObject>(bulletPrefab);

        go.transform.position = transform.position + new Vector3(0, 0.03f, 0.01f);

        Vector2 direction;
        if (isMoveRight)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        go.GetComponent<Bullet>().SetValues(direction, bulletSpeed);

        yield return new WaitForSeconds(0.38f);

        isAttack = false;
        animator.SetBool("isAttack", false);
    }
}
