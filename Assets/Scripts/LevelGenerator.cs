using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject checkpointFlag;

    private GameObject[] enemys = new GameObject[8];
    private GameObject[] floors = new GameObject[7];

    private string[] enNames = { "Enemys\\EnemyBat", "Enemys\\EnemyBee", "Enemys\\EnemyChicken", "Enemys\\EnemyGhost", "Enemys\\EnemyPlant", "Enemys\\EnemySlime", "Enemys\\EnemyTrunk", "Enemys\\EnemyTurtle" };

    private int countFloors = 0;
    private int numOfFloors = 15;

    private List<GameObject> floorList = new List<GameObject>();
    private List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        for(int i = 0; i < enemys.Length; i++)
        {
            enemys[i] = Resources.Load<GameObject>(enNames[i]);
        }

        for(int i = 0; i < floors.Length; i++)
        {
            floors[i] = Resources.Load<GameObject>("Floors\\Floor" + (i + 1).ToString());
        }
        floorList.Add(Instantiate(floors[0]));
        floorList[0].transform.position = new Vector3(0, -4.5f, 0);
        floorList.Add(Instantiate(floors[0]));
        floorList[1].transform.position = new Vector3(0, -3f, 0);
        floorList.Add(Instantiate(floors[0]));
        floorList[2].transform.position = new Vector3(0, -1.5f, 0);

        floorList.Add(Instantiate(floors[0]));
        floorList[3].transform.position = new Vector3(0, 0, 0);
        floorList[3].transform.Find("Box").gameObject.SetActive(true);

        for (int i = 0; i < 10; i++)
        {
            GenerateFloor();
        }
    }

    private void Start()
    {
        int hightScore = Save.GetHightScore();

        if(hightScore != 0)
        {
            checkpointFlag.transform.position = new Vector3(Save.GetXPos(), 1.5f * hightScore + 0.64f, -0.5f);
        }
    }

    public int GetScore()
    {
        return countFloors - numOfFloors + 4;
    }

    public void GenerateFloor()
    {
        countFloors++;
        int floorType = (countFloors % 34) / 5;

        floorList.Add(Instantiate(floors[floorType]));
        floorList[floorList.Count - 1].transform.position = new Vector3(0, 1.5f * countFloors, 0);

        if (floorList.Count > numOfFloors)
        {
            Destroy(floorList[0]);
            floorList.RemoveAt(0);
        }

        int ran = Random.Range(0, 8);
        enemyList.Add(Instantiate(enemys[Random.Range(0, 8)]));
        enemyList[enemyList.Count - 1].transform.position = new Vector3(Random.Range(-1.75f, 1.75f), 1.5f * countFloors + 0.4f, 0);

        if (floorList.Count > numOfFloors + 3)
        {
            Destroy(enemyList[0]);
            enemyList.RemoveAt(0);
        }
    }
}
