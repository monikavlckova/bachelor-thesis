using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Plan
{
    private int[,] plan;
    private Random rnd = new Random();

    public int[,] getPlan()
    {
        return plan;
    }

    public Plan(int size, int floors)
    {
        plan = Generate(size, floors);
    }

    public Plan(int[,] plan)
    {
        this.plan = plan;
    }

    public int[,] Generate(int size, int floors)
    {
        int[,] buildingPlan = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int cubes = rnd.Next(floors + 1);
                if (cubes == 4) cubes = rnd.Next(floors + 1);
                if (cubes != 0) cubes = rnd.Next(floors + 1);
                buildingPlan[i, j] = cubes;
            }
        }

        while (!isBuilding(buildingPlan))
        {
            buildingPlan = Generate(size, floors);
        }

        return buildingPlan;
    }

    private bool isBuilding(int[,] buildingPlan)
    {
        int size = buildingPlan.GetLength(0);

        int buildingSize = cubesInFloors(buildingPlan)[0];
        if (buildingSize == 0) return false;
        bool[,] visited = new bool[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (buildingPlan[i, j] != 0)
                {
                    buildingSize -= check(i, j, buildingPlan, visited);
                    return buildingSize == 0;
                }
            }
        }
        return false;
    }

    private int check(int i, int j, int[,] buildingPlan, bool[,] visited)
    {
        int size = buildingPlan.GetLength(0);
        if (i >= size || j >= size) return 0;

        if (buildingPlan[i, j] == 0 || visited[i, j]) return 0;
        visited[i, j] = true;
        return check(i, j + 1, buildingPlan, visited) + check(i + 1, j, buildingPlan, visited) + 1;
    }

    public int[,] modify()
    {
        int size = plan.GetLength(0);
        int[,] modifiedPlan = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int value = plan[i, j];
                if (value > 0) value += rnd.Next(2);
                value = value > 4 ? 4 : value;
                modifiedPlan[i, j] = value;
            }
        }

        while (equals(modifiedPlan, plan)) modifiedPlan = modify();
        return rotateRandom(modifiedPlan);
    }

    private int[,] rotate90(int[,] plan)
    {
        int size = plan.GetLength(0);
        int[,] rotatedPlan = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                rotatedPlan[j, size - 1 - i] = plan[i, j];
            }
        }

        return rotatedPlan;
    }

    private int[,] rotateRandom(int[,] plan)
    {
        int random = rnd.Next(4);
        for (int i = 0; i < random; i++)
        {
            plan = rotate90(plan);
        }
        return plan;
    }

    public int[] cubesInFloors(int[,] plan)
    {
        int[] values = new int[5];
        for (int i = 0; i < plan.GetLength(0); i++)
        {
            for (int j = 0; j < plan.GetLength(1); j++)
            {
                for (int v = 0; v < plan[i,j]; v++)
                {
                    values[v] += 1;
                }
            }
        }
        return values;
    }

    public int numberOfCubes(int[,] plan)
    {
        int num = 0;
        for (int i = 0; i < plan.GetLength(0); i++)
        {
            for (int j = 0; j < plan.GetLength(1); j++)
            {
                num += plan[i, j];
            }
        }
        return num;
    }
    
    public int numberOfFloors(int[,] plan)
    {
        int num = 0;
        for (int i = 0; i < plan.GetLength(0); i++)
        {
            for (int j = 0; j < plan.GetLength(1); j++)
            {
                if (num < plan[i, j]) num = plan[i, j];
            }
        }

        return num;
    }

    public bool equals(int[,] plan1, int[,] plan2)
    {
        Debug.Log("equals");
        int[,] rotated1 = rotate90(plan2);
        int[,] rotated2 = rotate90(rotated1);
        int[,] rotated3 = rotate90(rotated2);
        return equals1(plan1, plan2) || equals1(plan1, rotated1) || equals1(plan1, rotated2) || equals1(plan1, rotated3);
        
    }

    private bool equals1(int[,] plan1, int[,] plan2)
    {
        Debug.Log("equals1");
        if (plan1.Length != plan2.Length || plan1.GetLength(0) != plan2.GetLength(0))
        {
            Debug.Log("ina velkost");
            return false;
        }
        for (int i = 0; i < plan1.GetLength(0); i++)
        {
            if (!plan1.Cast<int>().SequenceEqual(plan2.Cast<int>()))
            {
                Debug.Log("false");
                return false;
            }
        }
        Debug.Log("true");
        return true;

    }

}
