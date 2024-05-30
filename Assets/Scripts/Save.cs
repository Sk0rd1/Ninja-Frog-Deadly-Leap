using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Save
{
    public static void Death()
    {
        int maxScore = GetHightScore();
        int curScore = GameObject.Find("Engine").GetComponent<LevelGenerator>().GetScore();

        if(curScore >= maxScore)
        {
            PlayerPrefs.SetInt("HightScore", curScore);
            PlayerPrefs.SetFloat("xPos", GameObject.Find("Player").transform.position.x);
        }
    }

    public static int GetHightScore()
    {
        try
        {
            return PlayerPrefs.GetInt("HightScore");
        }
        catch { return 0; }
    }

    public static float GetXPos()
    {
        try
        {
            return PlayerPrefs.GetFloat("xPos");
        }
        catch { return 0; }
    }

    public static void SetCoin(int value)
    {
        PlayerPrefs.SetInt("Coin", value);
    }

    public static int GetCoin()
    {
        try
        {
            return PlayerPrefs.GetInt("Coin");
        }
        catch { return 0; }
    }

    public static void SetLvlSpeed(int value)
    {
        PlayerPrefs.SetInt("LvlSpeed", value);
    }

    public static int GetLvlSpeed()
    {
        try
        {
            return PlayerPrefs.GetInt("LvlSpeed");
        }
        catch { return 0; }
    }

    public static void SetLvlHearts(int value)
    {
        PlayerPrefs.SetInt("LvlHearts", value);
    }

    public static int GetLvlHearts()
    {
        try
        {
            return PlayerPrefs.GetInt("LvlHearts");
        }
        catch { return 0; }
    }
}
