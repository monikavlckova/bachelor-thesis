using System;
using UnityEngine;
using UnityEngine.UI;

public class Gif : MonoBehaviour
{
    public Sprite[] images;
    private float fps = 1.0f;
    private int index = 0;
    private float time = 0f;
    private void Update()
    {
        time += Time.deltaTime * 20;
        if (time > index)
        {
            index += 1;
            Debug.Log(index);
            transform.GetComponent<Image>().sprite = images[index];
            if (index >= images.Length - 1)
            {
                time = -1;
                index = -1;
            }
        }
    }
    
}
