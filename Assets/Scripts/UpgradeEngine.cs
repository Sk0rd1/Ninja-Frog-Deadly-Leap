using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private TextMeshProUGUI lvlSpeed;
    [SerializeField]
    private TextMeshProUGUI lvlHearts;
    [SerializeField]
    private Slider sliderSpeed;
    [SerializeField]
    private Slider sliderHearts;
    
    public void Initialize()
    {
        valueMoney.text = Save.GetCoin().ToString();

        lvlSpeed.text = Save.GetLvlSpeed().ToString() + " <#313e54>/ 3";
        lvlHearts.text = Save.GetLvlHearts().ToString() + " <#313e54>/ 3";
    }
}
