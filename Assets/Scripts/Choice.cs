using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Choice : MonoBehaviour
{
    GameManager Manager;

    [Header("Texto")]
    [SerializeField] TextMeshProUGUI NameContainer;
    [SerializeField] TextMeshProUGUI DescriptionContainer;
    [SerializeField] string _Name;
    [SerializeField] string _Description;

    [Header("Propiedades")]
    [SerializeField] TextMeshProUGUI PollutionContainer;
    [SerializeField] TextMeshProUGUI HappinessContainer;
    [SerializeField] int MyPollutionRate;
    [SerializeField] int MyHappinessRate;

    [HideInInspector] public int _RateH, _RateP;

    private void Start()
    {
        Manager = GameManager.instance;
        SetText();
        _RateH = MyHappinessRate;
        _RateP = MyPollutionRate;
    }

    public int CountHappiness(int GlobalHappiness)
    {
        GlobalHappiness += MyHappinessRate;
        return GlobalHappiness;
    }

    public int CountPollution(int GlobalPollution)
    {
        GlobalPollution += MyPollutionRate;
        return GlobalPollution;
    }

    public int ReturnRate(int globalrate, bool Happiness)
    {
        if (Happiness)
        {
            globalrate += MyHappinessRate;
        }
        else
        {
            globalrate += MyPollutionRate;
        }
        return globalrate;
    }

    private void SetText()
    {
        this.NameContainer.text = _Name;
        this.DescriptionContainer.text = _Description;

        if(MyHappinessRate > 0)
            this.HappinessContainer.color = Color.green;
        else
            this.HappinessContainer.color = Color.red;

        if(MyPollutionRate > 0)
            this.PollutionContainer.color = Color.green;
        else
            this.PollutionContainer.color = Color.red;

        this.PollutionContainer.text = Mathf.Abs(MyPollutionRate).ToString();
        this.HappinessContainer.text = Mathf.Abs(MyHappinessRate).ToString();
    }
}
