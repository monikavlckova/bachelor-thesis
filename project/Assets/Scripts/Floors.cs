using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Floors : MonoBehaviour
{
    public Text[] textFloors = new Text[5];
    public int[] values = new int[5];

    public void increase(int floor)
    {
        values[floor - 1] += 1;
        textFloors[floor - 1].text = values[floor - 1].ToString();
    }
    
    public void decrease(int floor)
    {
        if (values[floor - 1] > 0)
        {
            values[floor - 1] -= 1;
            textFloors[floor - 1].text = values[floor - 1].ToString();
        }
    }
    
    

}
