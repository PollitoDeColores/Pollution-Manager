using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHappy : MonoBehaviour
{
    public Slider sliderH;
   

    public void setMaxH(int maxHappiness)
    {
        sliderH.maxValue = maxHappiness;

    }

    public void sethappinness(int happiness)
    {
        sliderH.value = happiness;
    }


}
