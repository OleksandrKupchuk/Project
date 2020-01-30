using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    float fillAmoundForLevel;
    [SerializeField] Image contentLevel;


    float fillAmount;

    [SerializeField] float lerpSpeed;

    [SerializeField] Text valueText;

    [SerializeField] Image content;

    public float MaxValue { get; set; }

    [SerializeField] Text levelText;


    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
    }

    float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    void LevelBar()
    {
        if(fillAmoundForLevel != contentLevel.fillAmount)
        {
            contentLevel.fillAmount = Mathf.Lerp(contentLevel.fillAmount, fillAmoundForLevel, Time.deltaTime * lerpSpeed);
        }
    }
}
