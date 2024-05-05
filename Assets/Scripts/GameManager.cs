using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Propiedades")]
    [SerializeField] Choice[] PollutionChoices;
    [SerializeField] Choice[] HappinessChoices;
    [SerializeField] private List<Choice> ActiveChoices;
    [SerializeField] private int PollutionRate;
    [SerializeField] private int HappinessRate;
    [SerializeField] private int WeeksPassed;


    [Header("Variables")]
    [SerializeField] int Pollution;
    [SerializeField] int Happiness;
    [SerializeField] int WeekTime;
    [SerializeField] int ChoiceTime;

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
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
        /*Espacio para el código de actualización gráfica*/
        yield return new WaitForSeconds(WeekTime);

        StartCoroutine(ChoiceCountdown());
    }

    private IEnumerator ChoiceCountdown()
    {
        int pcv = Random.Range(0, PollutionChoices.Length);
        int hcv = Random.Range(0, HappinessChoices.Length);
        PollutionChoices[pcv].gameObject.SetActive(true);
        HappinessChoices[hcv].gameObject.SetActive(true);
        yield return new WaitForSeconds(ChoiceTime);
        HideAll();
    }

    public void ChoiceTaken(Choice choice)
    {
        ActiveChoices.Add(choice);
        if(ActiveChoices.Count > 8)
        {
            Debug.Log("No se pueden tomar más medidas, elimina una");
        }
        StopAllCoroutines();
        HideAll();
    }
}
