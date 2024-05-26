using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

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
}
