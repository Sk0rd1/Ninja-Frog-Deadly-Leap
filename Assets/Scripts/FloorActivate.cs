using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorActivate : MonoBehaviour
{
    [SerializeField]
    private GameObject box;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            box.SetActive(true);
        }
    }
}
