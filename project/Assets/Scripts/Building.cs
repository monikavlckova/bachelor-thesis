
using UnityEngine;

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
            plan = new Plan(SIZE, FLOORS);
        }
        HALF = (float) SIZE / 2;

        Build(plan.getPlan());
        if (level == 1)
        {
            this.GetComponent<PlanGenerator>().GeneratePlans();
        }

        if (level == 2)
        {
            GameObject.Find("buildingPlan").GetComponent<PlanDrawer>().InitializeEmpty(SIZE,SIZE);
        }
    }

    private void Build(int[,] buildingPlan)
    {
        Debug.Log("build");
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
    public Plan getPlan()
    {
        return plan;
    }
}
