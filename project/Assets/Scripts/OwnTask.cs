using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

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

        if (level == 2)
        {
            GameObject.Find("buildingPlan").GetComponent<PlanDrawer>().InitializeEmpty(5,5, true);
        }
    }

    public void TranslateBuildingToPlan()
    {
        foreach (Transform cube in building.transform)
        {
            int x = 2+(int)(cube.position.x);
            int y = 1+(int)(cube.position.y);
            int z = 2-(int)(cube.position.z);
            if (y > plan[z, x])
            {
                plan[z, x] = y;
            }
        }
        Data.DATA.createdTask = true;
        Data.DATA.createdBuildingPlan = plan;
    }

    public int[,] TranslateImagePlanToPlan(GameObject imagePlan)
    {
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

    public bool IsValidLevel(int level)
    {
        plan = new int[5,5];
        TranslateBuildingToPlan();
        Plan p = new Plan(1,3, 1);
        int [,] emptyPlan = new int[5,5];
        if (level == 1) 
        {
            TranslateAllPlans();
            return p.isBuilding(plan, 1, 100) && 
                   (p.equals(Data.DATA.createdPlan1, plan) ^
                    p.equals(Data.DATA.createdPlan2, plan) ^
                    p.equals(Data.DATA.createdPlan3, plan) ^
                    p.equals(Data.DATA.createdPlan4, plan));
        }

        if (level == 2)
        {
            int [,] imagePlan = TranslateImagePlanToPlan(GameObject.Find("buildingPlan"));

            if (p.equals(plan, emptyPlan) ^ p.equals(imagePlan, emptyPlan))
            {
                if (p.equals(plan, emptyPlan))
                {
                    Data.DATA.buildBuilding = false;
                    Data.DATA.createdBuildingPlan = imagePlan;
                }
                else Data.DATA.buildBuilding = true;
                
                if (!p.isBuilding(Data.DATA.createdBuildingPlan, 1, 100)) return false;
                
                return true;
            }
            return false;
        }

        if (level == 3 || level == 4)
        {
            int[] values = GetComponent<Floors>().values;
            int[] emptyValues = new int[values.Length];
            if (p.equals(plan, emptyPlan) ^ values.SequenceEqual(emptyValues))
            {
                if (p.equals(plan, emptyPlan))
                {
                    Data.DATA.buildBuilding = false;
                    Data.DATA.floorsValues = values;
                    int floors = values[0];
                    int cubess = values[1];
                    if (level == 3 && (floors > cubess || floors < 1 || cubess > floors*25)) return false;
                    if (level == 4) for (int i = 1; i < values.Length-1; i++) if (values[i - 1] < values[i]) return false;
                }
                else
                {
                    if (!p.isBuilding(Data.DATA.createdBuildingPlan, 1, 100)) return false;
                    Data.DATA.buildBuilding = true;
                    
                }
                
                return true;
            }
            return false;
        }
        return false;
    }

    public void LoadCratedTask()
    {
        if (IsValidLevel(level))
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            _enumerator = GameObject.Find("EventSystem").GetComponent<Notification>().show(false);
            this.StartCoroutine(_enumerator);
        }
    }
}