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
    public Button ownTask;
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
            if (Data.DATA.highestLevelCompleted >= level)
            {
                if (level < 4)
                {
                    nextLevel.enabled = true;
                    nextLevel.GetComponent<Image>().enabled = true;   
                }
                ownTask.enabled = true;
                ownTask.GetComponent<Image>().enabled = true;
            }
            else
            {
                if (level < 4)
                {
                    nextLevel.enabled = false;
                    nextLevel.GetComponent<Image>().enabled = false;
                }
                ownTask.enabled = false;
                ownTask.GetComponent<Image>().enabled = false;
            }
        }
    }
    
    
}
