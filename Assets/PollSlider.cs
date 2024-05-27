using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PollSlider : MonoBehaviour
{
    public Slider sliderP;
    public void setMaxP(int maxPoll)
    {
        sliderP.maxValue = maxPoll;

    }

    public void setPoll(int Poll)
    {
        sliderP.value = Poll;
    }
}
