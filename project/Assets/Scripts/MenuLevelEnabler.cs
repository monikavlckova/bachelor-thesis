using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevelEnabler : MonoBehaviour
{
    public int level;
    public Color enabledButton = new Color32(183, 195, 116, 255);
    public Color enabledButtonImage = Color.white;
    public Color disabledButton = Color.black;
    public Color disabledButtonImage = Color.gray;
    
    // Update is called once per frame
    void Start()
    {
        GetComponent<Image>().color = Color.white;
        transform.Find("Play").transform.GetComponent<Button>().enabled = true;
        transform.Find("Play").transform.GetComponent<Image>().color = enabledButton;
        transform.Find("Play").transform.Find("Image").GetComponent<Image>().color = enabledButtonImage;
                
        transform.Find("Create").transform.GetComponent<Button>().enabled = true;
        transform.Find("Create").transform.GetComponent<Image>().color = enabledButton;
        transform.Find("Create").transform.Find("Image").GetComponent<Image>().color = enabledButtonImage;
                
        transform.Find("Reset").transform.GetComponent<Image>().color = enabledButton;
        transform.Find("Reset").transform.Find("Image").GetComponent<Image>().color = enabledButtonImage;

        if (level >= Data.DATA.heighestLevelCompleted + 1)
        {
            transform.Find("Create").transform.GetComponent<Button>().enabled = false;
            transform.Find("Create").transform.GetComponent<Image>().color = disabledButton;
            transform.Find("Create").transform.Find("Image").GetComponent<Image>().color = disabledButtonImage;
        }
        if (level > Data.DATA.heighestLevelCompleted + 1)
        {
            GetComponent<Image>().color = Color.black;
            transform.Find("Play").transform.GetComponent<Button>().enabled = false;
            transform.Find("Play").transform.GetComponent<Image>().color = disabledButton;
            transform.Find("Play").transform.Find("Image").GetComponent<Image>().color = disabledButtonImage;
            
            transform.Find("Reset").transform.GetComponent<Image>().color = disabledButton;
            transform.Find("Reset").transform.Find("Image").GetComponent<Image>().color = disabledButtonImage;
        }
        
        
    }
}
