using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Choice : MonoBehaviour
{
    GameManager Manager;

    [Header("Texto")]
    [SerializeField] string Name;
    [SerializeField] string Description;

    [Header("Propiedades")]
    [SerializeField] int MyPollutionRate;
    [SerializeField] int MyHappinessRate;

    private void Start()
    {
        Manager = GameManager.instance;
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
}
