using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    private bool isActivate = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isActivate)
        {
            isActivate = true;
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        HealthEventSystem.instance.TriggerCoin(1);

        while(true)
        {
            if(transform.localScale.x >= 0.1f)
            {
                transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime, transform.localScale.y - Time.deltaTime, transform.localScale.z);
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
