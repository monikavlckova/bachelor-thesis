
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public GameObject CubePrefab;
    public int level;
    public GameObject Parent;
    private Plan plan;
    private static int SIZE = 2;
    private float HALF = SIZE / 2;
    private int FLOORS = 4;

    private void Start()
    {
        int progress = Data.DATA.progressInLvls[level - 1];

        if (Data.DATA.createdTask)
        {
            plan = new Plan(Data.DATA.createdBuildingPlan);
            SIZE = plan.getPlan().GetLength(0);
        }
        else
        {
            SIZE = progress / 4 + 2;
            if (SIZE > 4) SIZE = 4;
            plan = new Plan(SIZE, FLOORS, progress);
            GameObject.Find("Ground").transform.localScale = new Vector3(SIZE, 1.5f, SIZE);
        }
        HALF = (float) SIZE / 2;
        
        if (level == 1 || 
            (Data.DATA.progressInLvls[level - 1] % 2 == 0 && !Data.DATA.createdTask) || 
            (Data.DATA.createdTask && Data.DATA.buildBuilding))
        {
            Build(plan.getPlan());
            if (level != 1)
            {
                GameObject.Find("Building").GetComponent<BuildCube>().Lock();
                GameObject.Find("resetButton").GetComponent<Button>().enabled = false;
                GameObject.Find("resetButton").GetComponent<Image>().enabled = false;
            }

            if (level == 2)
            {
                GameObject.Find("buildingPlan").GetComponent<PlanDrawer>().InitializeEmpty(SIZE,SIZE, true);
            }
        }
        else
        {
            GameObject.Find("Building").GetComponent<BuildCube>().Unlock();
            GameObject.Find("Building").GetComponent<BuildCube>().SIZE = SIZE;
            if (level == 2)
            {
                GameObject.Find("buildingPlan").GetComponent<PlanDrawer>().Draw(plan.getPlan());
            }
        }
        if (level == 1)
        {
            this.GetComponent<PlanGenerator>().GeneratePlans();
        }
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
                        Quaternion.identity, Parent.transform);
                }
            }
        }
    }
    
    public int[,] TranslateBuildingToPlan()
    {
        int[,] plan = new int[SIZE,SIZE];
        foreach (Transform cube in Parent.transform)
        {
            int x = (int)(SIZE/2f+(cube.position.x));
            int y = (int)(1+(cube.position.y));
            int z = (int)(SIZE/2f-(cube.position.z));
            if (y > plan[z,x])
            {
                plan[z, x] = y;
            }
        }
        return plan;
    }
    
    public void Destroy()
    {
        foreach (Transform cube in Parent.transform)
        {
            GameObject.Destroy(cube.gameObject);
        }
    }
    
    public Plan getPlan()
    {
        return plan;
    }
}
