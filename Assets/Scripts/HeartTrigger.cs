using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartTrigger : MonoBehaviour
{
    private bool isActivate = false;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivate)
        {
            isActivate = true;
            StartCoroutine(Scale());
        }
    }

    private IEnumerator Move()
    {
        float minPos = transform.position.y - 0.15f;
        float maxPos = transform.position.y + 0.15f;

        float coef = 1;
        float speed = 0.6f;

        while (true)
        {
            transform.position += new Vector3(0, coef * speed * Time.deltaTime, 0);

            if (transform.position.y < minPos)
                coef = 1;

            if (transform.position.y > maxPos)
                coef = -1;

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Scale()
    {
        HealthEventSystem.instance.TriggerHeal(1);

        while (true)
        {
            if (transform.localScale.x >= 0.1f)
            {
                transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * 4, transform.localScale.y - Time.deltaTime * 4, transform.localScale.z);
            }
            else
            {
                Destroy(gameObject);
                break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
