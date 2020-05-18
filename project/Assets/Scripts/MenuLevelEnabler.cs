using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevelEnabler : MonoBehaviour
{
    public int level;
    private Image image;
    private Button button;
    private Transform create;
    private Image createTransformImage;
    private Button createButton;
    private Transform createImage;
    private Image createImageTransformImage;
    private Image lockImage;

    private void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        create = transform.Find("Create");
        createTransformImage = create.transform.GetComponent<Image>();
        createButton = create.transform.GetComponent<Button>();
        createImage = create.transform.Find("Image");
        createImageTransformImage = createImage.transform.GetComponent<Image>();
        lockImage = transform.Find("Lock").transform.GetComponent<Image>();
    }

    void Update()
    {
        image.color = Color.white;
        lockImage.enabled = false;
        
        button.enabled = true;
        createButton.enabled = true;

        if (level >= Data.DATA.highestLevelCompleted + 1)
        {
            createButton.enabled = false;
            createTransformImage.enabled = false;
            createImageTransformImage.enabled = false;
        }
        if (level > Data.DATA.highestLevelCompleted + 1)
        {
            image.color = Color.black;
            button.enabled = false;
            lockImage.enabled = true;
        }
    }
}
