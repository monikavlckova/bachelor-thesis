using UnityEngine;
using System.Collections.Generic;
using Random = System.Random;

public class PlanGenerator : MonoBehaviour
{
    public GameObject plan1;
    public GameObject plan2;
    public GameObject plan3;
    public GameObject plan4;

    private List<int[,]> buildingPlans;
    private Random rnd;
    private int index;

    public void GeneratePlans()
    {
        rnd = new Random();
        buildingPlans = new List<int[,]>();

        Plan buildingPlan = this.GetComponent<Building>().getPlan();
        
        if (Data.DATA.createdTask)
        {
            buildingPlans.Add(Data.DATA.createdPlan1);
            buildingPlans.Add(Data.DATA.createdPlan2);
            buildingPlans.Add(Data.DATA.createdPlan3);
            buildingPlans.Add(Data.DATA.createdPlan4);
            setIndex(buildingPlan);
        }
        else
        {
            index = rnd.Next(4);
            for (int i = 0; i < 4; i++)
            {
                if (i != index)
                {
                    int[,] modifiedPlan = buildingPlan.modify();
                    
                    for (int j = 0; j < i; j++)
                    {
                        while (buildingPlan.equals(modifiedPlan, buildingPlans[j]) ||
                               buildingPlan.equals(modifiedPlan, buildingPlan.getPlan()))
                        {
                            modifiedPlan = buildingPlan.modify();
                        }
                    }

                    buildingPlans.Add(modifiedPlan);
                }
                else
                {
                    buildingPlans.Add(buildingPlan.getPlan());
                }
            }
        }

        plan1.GetComponent<PlanDrawer>().Draw(buildingPlans[0]);
        plan2.GetComponent<PlanDrawer>().Draw(buildingPlans[1]);
        plan3.GetComponent<PlanDrawer>().Draw(buildingPlans[2]);
        plan4.GetComponent<PlanDrawer>().Draw(buildingPlans[3]);
    }

    private void setIndex(Plan buildingPlan)
    {
        if (buildingPlan.equals(buildingPlans[0], buildingPlan.getPlan())) index = 0;
        else if (buildingPlan.equals(buildingPlans[1], buildingPlan.getPlan())) index = 1;
        else if (buildingPlan.equals(buildingPlans[2], buildingPlan.getPlan())) index = 2;
        else if (buildingPlan.equals(buildingPlans[3], buildingPlan.getPlan())) index = 3;
    }

    public int getIndex()
    {
        return index;
    }

}