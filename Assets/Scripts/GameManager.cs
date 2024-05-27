using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject[] All;
    [SerializeField] private Choice[] PollutionChoices;
    [SerializeField] private Choice[] HappinessChoices;
    [Header("Propiedades")]
    [SerializeField] private List<Choice> ActiveChoices;
    [SerializeField] private int PollutionRate;
    [SerializeField] private int HappinessRate;
    [SerializeField] private int WeeksPassed;
    [SerializeField] private int LimitValue;

    public SliderHappy HappinessSlider;  // Referencia al script del slider de felicidad
    public PollSlider PollutionSlider;  // Referencia al script del slider de contaminación

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
        if (instance == null)
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
        // Inicializar los sliders
        HappinessSlider.setMaxH(LimitValue);
        PollutionSlider.setMaxP(LimitValue);

        StartTutorial();
    }

    private void StartTutorial()
    {
        HideAll();
    }

    private void HideAll()
    {
        if (PollutionChoices != null)
            foreach (Choice choice in PollutionChoices)
            {
                choice.gameObject.SetActive(false);
            }
        if (HappinessChoices != null)
            foreach (Choice choice in HappinessChoices)
            {
                choice.gameObject.SetActive(false);
            }
        StartCoroutine(WeekPassingBy());
    }

    private IEnumerator WeekPassingBy()
    {
        foreach (Choice choice in ActiveChoices)
        {
            Happiness = choice.CountHappiness(Happiness);
            Pollution = choice.CountPollution(Pollution);
        }
        Debug.Log("Hap " + Happiness);
        Debug.Log("Poll " + Pollution);
        Debug.Log("RateP " + PollutionRate);
        Debug.Log("RateH " + HappinessRate);

        // Actualizar los sliders
        HappinessSlider.sethappinness(Happiness);
        PollutionSlider.setPoll(Pollution);

        /*Espacio para el código de actualización gráfica*/
        if (Happiness < 40)
        {
            ModuleHapp.rateOverTime = 0;
            ModuleAng.rateOverTime = (LimitValue / 2) - Happiness;
        }
        else
        {
            ModuleHapp.rateOverTime = Happiness / 15;
            ModuleAng.rateOverTime = 0;
        }
        ModulePo.rateOverTime = Pollution;

        yield return new WaitForSeconds(WeekTime);

        if (Pollution >= 95 || Happiness <= 5)
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
        yield return new WaitForSeconds(20f);
        HideAll();
    }

    public void ChoiceTaken(Choice choice)
    {
        ActiveChoices.Add(choice);
        if (ActiveChoices.Count > 8)
        {
            Debug.Log("No se pueden tomar más medidas, elimina una");
        }
        StopAllCoroutines();
        HideAll();
    }
}
