using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkAttack : MonoBehaviour
{
    [SerializeField]
    private TrunkMovement trunkMovement; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trunkMovement.Attack(collision.transform.position);
        }
    }
}
