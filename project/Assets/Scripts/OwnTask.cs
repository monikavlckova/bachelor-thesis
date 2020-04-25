using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OwnTask : MonoBehaviour
{
    public GameObject building;
    public int level;
    public GameObject plan1;
    public GameObject plan2;
    public GameObject plan3;
    public GameObject plan4;
    public int[,] plan = new int[5,5];
    private IEnumerator _enumerator;
    
    public void Start()
    {
        if (level == 1)
        {
            GameObject.Find("buildingPlan1").GetComponent<PlanDrawer>().InitializeEmpty(5,5, true);
            GameObject.Find("buildingPlan2").GetComponent<PlanDrawer>().InitializeEmpty(5,5, true);
            GameObject.Find("buildingPlan3").GetComponent<PlanDrawer>().InitializeEmpty(5,5, true);
            GameObject.Find("buildingPlan4").GetComponent<PlanDrawer>().InitializeEmpty(5,5, true);
        }
    }

    public void TranslateBuildingToPlan()
    {
        foreach (Transform cube in building.transform)
        {
            int x = 2+(int)(cube.position.x);
            int y = 1+(int)(cube.position.y);
            int z = 2-(int)(cube.position.z);
            if (y > plan[z,x])
            {
                plan[z, x] = y;
            }
        }
        Data.DATA.createdTask = true;
        Data.DATA.createdBuildingPlan = plan;
    }

    public int[,] TranslateImagePlanToPlan(GameObject imagePlan)
    {
        Debug.Log("translate");
        int[,] plan = new int[5,5];
        int i = 0;
        int j = 0;
        foreach (Transform image in imagePlan.transform)
        {
            if (image.transform.GetComponent<Image>().sprite != null)
            {
                plan[j,i] = Int32.Parse(image.transform.GetComponent<Image>().sprite.name);
            }
            j = j > 3 ? 0 : j+1;
            i = j == 0 ? i+1 : i;
        }
        return plan;
    }

    public void TranslateAllPlans()
    {
        Data.DATA.createdPlan1 = TranslateImagePlanToPlan(plan1);
        Data.DATA.createdPlan2 = TranslateImagePlanToPlan(plan2);
        Data.DATA.createdPlan3 = TranslateImagePlanToPlan(plan3);
        Data.DATA.createdPlan4 = TranslateImagePlanToPlan(plan4);
    }

    public bool IsValid()
    {
        Plan p = new Plan(1,3);
        return 
        p.equals(Data.DATA.createdPlan1, Data.DATA.createdBuildingPlan) ^
        p.equals(Data.DATA.createdPlan2, Data.DATA.createdBuildingPlan) ^
        p.equals(Data.DATA.createdPlan3, Data.DATA.createdBuildingPlan) ^
        p.equals(Data.DATA.createdPlan4, Data.DATA.createdBuildingPlan);
    }

    public void LoadCratedTask()
    {
        if (IsValid())
        {
            Application.LoadLevel(1);
        }
        else
        {
            _enumerator = GameObject.Find("EventSystem").GetComponent<Notification>().show(false);
            this.StartCoroutine(_enumerator);
        }
    }
}