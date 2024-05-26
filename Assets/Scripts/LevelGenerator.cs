using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private GameObject[] enemys = new GameObject[8];
    private GameObject[] floors = new GameObject[7];

    private string[] enNames = { "Enemys\\EnemyBat", "Enemys\\EnemyBee", "Enemys\\EnemyChicken", "Enemys\\EnemyGhost", "Enemys\\EnemyPlant", "Enemys\\EnemySlime", "Enemys\\EnemyTrunk", "Enemys\\EnemyTurtle" };

    private void Awake()
    {
        for(int i = 0; i < enemys.Length; i++)
        {
            enemys[i] = Resources.Load<GameObject>(enNames[i]);
        }

        for(int i = 0; i < floors.Length; i++)
        {
            floors[i] = Resources.Load<GameObject>("Floors\\Floor" + i.ToString());
        }
    }

    public void GenerateFloor()
    {

    }
}
