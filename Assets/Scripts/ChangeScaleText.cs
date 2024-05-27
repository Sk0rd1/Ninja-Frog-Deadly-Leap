using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeScaleText : MonoBehaviour
{
    [SerializeField]
    private float minScale = 120;
    [SerializeField]
    private float maxScale = 160;
    [SerializeField]
    private float speedScale = 40f;

    private TextMeshProUGUI text;
    private float coef = 1;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.fontSize = minScale;
    }

    private void Update()
    {
        if (text.fontSize <= minScale)
            coef = 1;

        if (text.fontSize >= maxScale)
            coef = -1;

        text.fontSize += coef * speedScale * Time.deltaTime;
    }
}
