using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeEngine : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI valueMoney;

    [SerializeField]
    private TextMeshProUGUI valueUpSpeed;
    [SerializeField]
    private TextMeshProUGUI valueUpHearts;
    [SerializeField]
    private TextMeshProUGUI valueUpRegeneration;
    [SerializeField]
    private TextMeshProUGUI valueUpCoin;

    [SerializeField]
    private TextMeshProUGUI lvlSpeed;
    [SerializeField]
    private TextMeshProUGUI lvlHearts;
    [SerializeField]
    private TextMeshProUGUI lvlRegeneration;
    [SerializeField]
    private TextMeshProUGUI lvlCoin;

    [SerializeField]
    private Slider sliderSpeed;
    [SerializeField]
    private Slider sliderHearts;
    [SerializeField]
    private Slider sliderRegeneration;
    [SerializeField]
    private Slider sliderCoin;

    [SerializeField]
    private Button buttonSpeed;
    [SerializeField]
    private Button buttonHearts;
    [SerializeField]
    private Button buttonRegeneration;
    [SerializeField]
    private Button buttonCoin;

    [SerializeField]
    private TextMeshProUGUI textCoinWin;

    private Sprite buttonFalseImage;

    private void Start()
    {
        buttonFalseImage = Resources.Load<Sprite>("UI\\ButtonFalse");

        Save.SetCoin(200);
        Save.SetLvlSpeed(0);
        Save.SetLvlHearts(0);

        Initialize();
    }

    public void Initialize(int coinWin = 0)
    {
        if(coinWin != 0)
        {
            textCoinWin.enabled = true;
            textCoinWin.text = "+" + coinWin.ToString();
        }
        else
        {
            textCoinWin.enabled = false;
        }

        int speed = Save.GetLvlSpeed();
        int hearts = Save.GetLvlHearts();
        int regeneration = Save.GetLvlRegeneration();
        int coin = Save.GetLvlCoin();

        valueMoney.text = Save.GetCoin().ToString();

        lvlSpeed.text = speed.ToString() + " <#32c8f8>/ 3";
        lvlHearts.text = hearts.ToString() + " <#313e54>/ 3";
        lvlRegeneration.text = regeneration.ToString() + " <#313e54>/ 3";
        lvlCoin.text = coin.ToString() + " <#fff300>/ 3";

        valueUpSpeed.text = ((speed + 1) * 15).ToString();
        valueUpHearts.text = ((hearts + 1) * 15).ToString();
        valueUpRegeneration.text = ((regeneration + 1) * 15).ToString();
        valueUpCoin.text = ((coin + 1) * 15).ToString();

        sliderSpeed.value = speed;
        sliderHearts.value = hearts;
        sliderRegeneration.value = regeneration;
        sliderCoin.value = coin;

        if(speed == 3)
        {
            Image im = buttonSpeed.GetComponent<Image>();
            im.sprite = buttonFalseImage;
            im.color = new Color(90, 90, 90);

            buttonSpeed.interactable = false;

            valueUpSpeed.enabled = false;
        }

        if(hearts == 3)
        {
            Image im = buttonHearts.GetComponent<Image>();
            im.sprite = buttonFalseImage;
            im.color = new Color(90, 90, 90);

            buttonHearts.interactable = false;

            valueUpHearts.enabled = false;
        }

        if (regeneration == 3)
        {
            Image im = buttonRegeneration.GetComponent<Image>();
            im.sprite = buttonFalseImage;
            im.color = new Color(90, 90, 90);

            buttonRegeneration.interactable = false;

            valueUpRegeneration.enabled = false;
        }

        if (coin == 3)
        {
            Image im = buttonCoin.GetComponent<Image>();
            im.sprite = buttonFalseImage;
            im.color = new Color(90, 90, 90);

            buttonCoin.interactable = false;

            valueUpCoin.enabled = false;
        }
    }

    public void PressUpSpeed()
    {
        int lvl = Save.GetLvlSpeed();

        if (lvl == 3) return;

        int coin = Save.GetCoin();

        if(coin >= (lvl + 1) * 15)
        {
            Save.SetLvlSpeed(++lvl);
            Save.SetCoin(coin - lvl * 15);
        }

        Initialize();
    }

    public void PressUpHearts()
    {
        int lvl = Save.GetLvlHearts();

        if (lvl == 3) return;

        int coin = Save.GetCoin();

        if (coin >= (lvl + 1) * 15)
        {
            Save.SetLvlHearts(++lvl);
            Save.SetCoin(coin - lvl * 15);
        }

        Initialize();
    }

    public void PressUpRegeneration()
    {
        int lvl = Save.GetLvlRegeneration();

        if (lvl == 3) return;

        int coin = Save.GetCoin();

        if (coin >= (lvl + 1) * 15)
        {
            Save.SetLvlRegeneration(++lvl);
            Save.SetCoin(coin - lvl * 15);
        }

        Initialize();
    }

    public void PressUpCoin()
    {
        int lvl = Save.GetLvlCoin();

        if (lvl == 3) return;

        int coin = Save.GetCoin();

        if (coin >= (lvl + 1) * 15)
        {
            Save.SetLvlCoin(++lvl);
            Save.SetCoin(coin - lvl * 15);
        }

        Initialize();
    }

    public void PressPlay()
    {

    }
}
