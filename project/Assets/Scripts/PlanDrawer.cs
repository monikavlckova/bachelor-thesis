using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanDrawer : MonoBehaviour
{
    private int[,] buildingPlan;
    public Sprite[] sprites = new Sprite[4];
    public Image square;
    private int SQUARESIZE = 70;
    private int SPACE = 5;
    private int PLANSIZE = 350;
    

    public void Draw(int[,] buildingPlan)
    {
        int w = buildingPlan.GetLength(0);
        int h = buildingPlan.GetLength(1);
        InitializeEmpty(w, h);

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                Image tile = this.transform.GetChild(i + j + (i * (h - 1))).transform.GetComponent<Image>();
                if (buildingPlan[j, i] != 0)
                    tile.sprite = sprites[(buildingPlan[j, i] - 1)];
                else
                    tile.color = new Color32(0,0,0,0);
            }
        }
    }

    public void InitializeEmpty(int w, int h)
    {
        RectTransform r = (RectTransform)this.transform;;
        PLANSIZE = (int)r.rect.width;
        SPACE = PLANSIZE / 70;
        SQUARESIZE = w > h ? (PLANSIZE-(w-1)-(w*(SPACE-1)))/w : (PLANSIZE-(h-1)-(h*(SPACE-1)))/h;
        square.rectTransform.sizeDelta = new Vector2(SQUARESIZE, SQUARESIZE);
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                Vector3 position = new Vector3(((i*SQUARESIZE)+(SQUARESIZE/2)+1+(i*SPACE)), -((j*SQUARESIZE)+(SQUARESIZE/2))-1-(j*SPACE));
                Image tile = Instantiate(square, this.transform);
                tile.rectTransform.anchoredPosition = position;
                tile.rectTransform.localScale = new Vector3(1,1,1);
            }
        }
    }
    
    public int[,] TranslateImagePlanToPlan()
    {
        Debug.Log("translate");
        int size = (int)Mathf.Sqrt(this.transform.childCount);
        int[,] plan = new int[size,size];
        int i = 0;
        int j = 0;
        foreach (Transform image in this.transform)
        {
            if (image.transform.GetComponent<Image>().sprite != null)
            {
                plan[j,i] = Int32.Parse(image.transform.GetComponent<Image>().sprite.name);
            }
            j = j > size-2 ? 0 : j+1;
            i = j == 0 ? i+1 : i;
        }
        return plan;
    }
}
