using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] All;
    private Choice[] PollutionChoices;
    private Choice[] HappinessChoices;
    [Header("Propiedades")]
    [SerializeField] private List<Choice> ActiveChoices;
    [SerializeField] private int PollutionRate;
    [SerializeField] private int HappinessRate;
    [SerializeField] private int WeeksPassed;
    [SerializeField] private int LimitValue;


    [Header("Variables")]
    [SerializeField] int Pollution;
    [SerializeField] int Happiness;
    [SerializeField] int WeekTime;
    [SerializeField] int ChoiceTime;

    [Header("Shaders")]
    [SerializeField] GameObject Lost;
    [SerializeField] Material ParticleH, ParticleA;
    [SerializeField] ParticleSystem PollutionParticles, HappinessParticles, AngryParticles;
    private ParticleSystem.EmissionModule ModulePo, ModuleHapp, ModuleAng;

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        ModulePo = PollutionParticles.emission;
        ModuleHapp = HappinessParticles.emission;
        ModuleAng = AngryParticles.emission;
    }
    private void Start()
    {
        ModuleAng.rateOverTime = 0;
        int j = 0;
        int i = 0;
        All = GameObject.FindGameObjectsWithTag("Choice");
        foreach(GameObject choice in All)
        {
            Choice Temp = gameObject.GetComponent<Choice>();
            if( Temp != null )
                if(Temp._RateH > 0)
                {
                    HappinessChoices[i] = Temp;
                    i++;
                }
                else if(Temp._RateP < 0) 
                {
                    PollutionChoices[j] = Temp;
                    j++;
                }                  
        }
        StartTutorial();
    }

    private void StartTutorial()
    {
        HideAll();
    }

    private void HideAll()
    {
        foreach(Choice choice in PollutionChoices)
        {
            choice.gameObject.SetActive(false);
        }
        foreach (Choice choice in HappinessChoices)
        {
            choice.gameObject.SetActive(false);
        }
        StartCoroutine(WeekPassingBy());
    }

    private IEnumerator WeekPassingBy()
    {
        foreach(Choice choice in ActiveChoices)
        {
            Happiness = choice.CountHappiness(Happiness);
            Pollution = choice.CountPollution(Pollution);
        }
        Debug.Log("Hap " + Happiness);
        Debug.Log("Poll " + Pollution);
        Debug.Log("RateP " + PollutionRate);
        Debug.Log("RateH " + HappinessRate);
        /*Espacio para el c�digo de actualizaci�n gr�fica*/
        if( Happiness < 40)
        {
            ModuleHapp.rateOverTime = 0;
            ModuleAng.rateOverTime = (LimitValue/2)-Happiness;
        }
        else
        {
            ModuleHapp.rateOverTime = Happiness / 15;
            ModuleAng.rateOverTime = 0;
        }
        ModulePo.rateOverTime = Pollution;

        yield return new WaitForSeconds(WeekTime);
        
        if(Pollution >=95 || Happiness <= 5)
        {
            Lost.SetActive(true);
            this.gameObject.SetActive(false);
        }

        StartCoroutine(ChoiceCountdown());
    }

    private IEnumerator ChoiceCountdown()
    {
        int pcv = Random.Range(0, PollutionChoices.Length);
        int hcv = Random.Range(0, HappinessChoices.Length);
        PollutionChoices[pcv].gameObject.SetActive(true);
        HappinessChoices[hcv].gameObject.SetActive(true);
        yield return null;
        //HideAll();
    }

    public void ChoiceTaken(Choice choice)
    {
        ActiveChoices.Add(choice);
        if(ActiveChoices.Count > 8)
        {
            Debug.Log("No se pueden tomar m�s medidas, elimina una");
        }
        StopAllCoroutines();
        HideAll();
    }
}
