using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDEngine : MonoBehaviour
{
    [SerializeField]
    private GameObject game;
    [SerializeField]
    private GameObject upgrade;

    public void EndGame(int coin)
    {
        Save.Death();

        GetComponent<AudioSource>().Play();

        upgrade.SetActive(true);
        Save.SetCoin(Save.GetCoin() + coin);
        upgrade.GetComponent<UpgradeEngine>().Initialize(coin);

        game.SetActive(false);

        GameObject player = GameObject.Find("Player");
        player.GetComponent<CharacterMovement>().StopPlayer();
        player.transform.position = new Vector3(0f, 0.5f, -1);

        GameObject.Find("Engine").GetComponent<LevelGenerator>().GenerateLevel();
    }
}
