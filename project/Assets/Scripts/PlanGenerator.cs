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

/*public class PlanGenerator : MonoBehaviour
{
    public GameObject CubePrefab;
    public GameObject plan1;
    public GameObject plan2;
    public GameObject plan3;
    public GameObject plan4;

    private List<int[,]> buildingPlans;
    private Random rnd;
    private int index;

    private int progress;

    private static int SIZE = 2;
    private float HALF = SIZE / 2;
    private int FLOORS = 3;
    
    void Start()
    {
        rnd = new Random();
        buildingPlans = new List<int[,]>();

        progress = DataFile.Instance.GetProgressInLvls()[0]; //TODO zmen 0 za premennu
        if (progress < 4) SIZE = 2;
        else if (progress < 8) SIZE = 3;
        else SIZE = 4;
        HALF = (float) SIZE / 2;
        int[,] plan = new int[SIZE, SIZE];
        plan = Generate(plan);

        index = rnd.Next(4);

        for (int i = 0; i < 4; i++)
        {
            if (i != index) buildingPlans.Add(modify(plan));
            else buildingPlans.Add(plan);
        }

        Build(buildingPlans[index]);
        plan1.GetComponent<PlanDrawer>().Draw(buildingPlans[0]);
        plan2.GetComponent<PlanDrawer>().Draw(buildingPlans[1]);
        plan3.GetComponent<PlanDrawer>().Draw(buildingPlans[2]);
        plan4.GetComponent<PlanDrawer>().Draw(buildingPlans[3]);
    }
    
    private void Build(int[,] buildingPlan)
    {
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                for (int k = 0; k < buildingPlan[i, j]; k++)
                {
                    Instantiate(CubePrefab, new Vector3(j - HALF + 0.5f, k, SIZE - HALF - 0.5f - i),
                        Quaternion.identity);
                }
            }
        }
    }

    public int[,] Generate(int[,] buildingPlan, int c = 0)
    {
        for (int i = c; i < buildingPlan.Length - 1 - c; i++)
        {
            for (int j = c; j < buildingPlan.Length - 1 - c; j++)
            {
                buildingPlan[i, j] = rnd.Next(FLOORS + 1);
            }
        }

        while (!isBuilding(buildingPlan))
        {
            buildingPlan = Generate(buildingPlan);
        }

        return buildingPlan;
    }

    private bool isBuilding(int[,] buildingPlan)
    {
        int size = this.buildingSize(buildingPlan);
        bool[,] visited = new bool[SIZE, SIZE];
        if (size == 0) return false;
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (buildingPlan[i, j] != 0)
                {
                    size -= check(i, j, buildingPlan, visited);
                    return size == 0;
                }
            }
        }

        return false;
    }

    private int check(int i, int j, int[,] buildingPlan, bool[,] visited)
    {
        if (i >= SIZE || j >= SIZE) return 0;

        if (buildingPlan[i, j] != 0 && !visited[i, j])
        {
            visited[i, j] = true;
            return check(i, j + 1, buildingPlan, visited) + check(i + 1, j, buildingPlan, visited) + 1;
        }

        return 0;
    }

    private int buildingSize(int[,] buildingPlan)
    {
        int size = 0;
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (buildingPlan[i, j] > 0)
                {
                    size += 1;
                }
            }
        }

        return size;
    }

    private int[,] Generate1(int[,] buildingPlan)
    {
        for (int i = 0; i < SIZE - HALF; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                int n = rnd.Next(FLOORS + 1);
                buildingPlan[i, j] = n;
                buildingPlan[SIZE - 1 - i, j] = n;
            }
        }

        while (!isBuilding(buildingPlan))
        {
            buildingPlan = Generate1(buildingPlan);
        }

        return buildingPlan;
    }

    private int[,] Generate2(int[,] buildingPlan)
    {
        for (int i = 0; i < SIZE - HALF; i++)
        {
            for (int j = 0; j < SIZE - HALF; j++)
            {
                int n = rnd.Next(FLOORS + 1);
                buildingPlan[i, j] = n;
                buildingPlan[SIZE - 1 - i, j] = n;
                buildingPlan[i, SIZE - 1 - j] = n;
                buildingPlan[SIZE - 1 - i, SIZE - 1 - j] = n;
            }
        }

        while (!isBuilding(buildingPlan))
        {
            buildingPlan = Generate2(buildingPlan);
        }

        return buildingPlan;
    }

    private int[,] modify(int[,] plan)
    {
        int[,] modifiedPlan = new int[SIZE, SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                int value = plan[i, j];
                if (value > 0) value += rnd.Next(2);
                modifiedPlan[i, j] = value;
            }
        }
        return rotateRandom(modifiedPlan);
    }

    private int[,] rotate90(int[,] plan)
    {
        int[,] rotatedPlan = new int[SIZE,SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                rotatedPlan[j, SIZE-1-i] = plan[i,j];
            }
        }    
        return rotatedPlan;
    }

    private int[,] rotateRandom(int[,] plan)
    {
        int random = rnd.Next(3)+1;
        Debug.Log(random);
        for (int i = 0; i < random; i++)
        {
            plan = rotate90(plan);
        }

        return plan;
    }

    public int getIndex()
    {
        return index;
    }
}*/
