using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Image image;
    public Image background;

    public Sprite good;
    public Sprite bad;

    private void Start()
    {
        image.enabled = false;
        background.enabled = false;
    }

    public IEnumerator show(bool correct)
    {
        image.enabled = true;
        background.enabled = true;
        image.transform.GetComponent<Image>().sprite = correct ? good : bad;
        yield return new WaitForSecondsRealtime(1);
        image.enabled = false;
        background.enabled = false;
    }
}
