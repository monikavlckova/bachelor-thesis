using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevelEnabler : MonoBehaviour
{
    public int level;
    
    void Start()
    {
        GetComponent<Image>().color = Color.white;
        Transform create = transform.Find("Create");
        Transform createImage = create.transform.Find("Image");

        GetComponent<Button>().enabled = true;
        create.transform.GetComponent<Button>().enabled = true;

        if (level >= Data.DATA.highestLevelCompleted + 1)
        {
            create.transform.GetComponent<Button>().enabled = false;
            create.transform.GetComponent<Image>().enabled = false;
            createImage.transform.GetComponent<Image>().enabled = false;
           
        }
        if (level > Data.DATA.highestLevelCompleted + 1)
        {
            GetComponent<Image>().color = Color.black;
            GetComponent<Button>().enabled = false;
            
        }
        
        
    }
}
