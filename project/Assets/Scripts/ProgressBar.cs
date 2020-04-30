using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int level;
    public bool isLevel = false;
    public Button nextLevel;
    private const float MAX_PROGRESS = 10f;
    private float progress = 0f;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        if (Data.DATA.createdTask)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
        progress = Data.DATA.progressInLvls[level-1];
        image.fillAmount = (progress) / MAX_PROGRESS;
    }

    private void Update()
    {
        progress =  Data.DATA.progressInLvls[level-1];
        image.fillAmount = progress / MAX_PROGRESS;
        if (isLevel)
        {
            if (progress > 9)
            {
                nextLevel.enabled = true;
                nextLevel.GetComponent<Image>().enabled = true;
            }
            else
            {
                nextLevel.enabled = false;
                nextLevel.GetComponent<Image>().enabled = false;
            }
        }
    }
    
    
}
