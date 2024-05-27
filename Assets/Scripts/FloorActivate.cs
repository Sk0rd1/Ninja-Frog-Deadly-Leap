using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorActivate : MonoBehaviour
{
    [SerializeField]
    private GameObject box;

    private LevelGenerator levelGenerator;

    private bool isCreateNextFloor = false;

    private void Start()
    {
        levelGenerator = GameObject.Find("Engine").GetComponent<LevelGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!isCreateNextFloor)
            {
                levelGenerator.GenerateFloor();
                isCreateNextFloor = true;
            }
            box.SetActive(true);
        }
    }
}
