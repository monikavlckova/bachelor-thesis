using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Plan
{
    private int[,] plan;
    private Random rnd = new Random();
    private int level;

    public int[,] getPlan()
    {
        return plan;
    }

    public Plan(int size, int floors, int progress)
    {
        if (progress > 20) progress = 20; 
        int min = progress + 2;
        int max = min + 1 + progress/2;
        plan = Generate(size, floors, min, max);
    }

    public Plan(int[,] plan)
    {
        this.plan = plan;
    }

    public int[,] Generate(int size, int floors, int min, int max)
    {
        int[,] buildingPlan = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int num = rnd.Next(floors + 1);
                int cubes = rnd.Next(num + 1);
                buildingPlan[i, j] = cubes;
            }
        }
        while (!isBuilding(buildingPlan, min, max))
        {
            buildingPlan = Generate(size, floors, min, max);
        }

        return buildingPlan;
    }

    private bool isBuilding(int[,] buildingPlan, int min, int max)
    {
        int size = buildingPlan.GetLength(0);

        int numOfCubes = numberOfCubes(buildingPlan);
        int buildingSize = cubesInFloors(buildingPlan)[0];
        if (numOfCubes < min) return false;
        if (numOfCubes > max) return false;
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
                if (value > 0) value = rnd.Next(4)+1;
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
        int[] values = new int[4];
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
        int[,] rotated1 = rotate90(plan2);
        int[,] rotated2 = rotate90(rotated1);
        int[,] rotated3 = rotate90(rotated2);
        return equals1(plan1, plan2) || equals1(plan1, rotated1) || equals1(plan1, rotated2) || equals1(plan1, rotated3);
        
    }

    private bool equals1(int[,] plan1, int[,] plan2)
    {
        if (plan1.Length != plan2.Length || plan1.GetLength(0) != plan2.GetLength(0))
        {
            return false;
        }
        for (int i = 0; i < plan1.GetLength(0); i++)
        {
            if (!plan1.Cast<int>().SequenceEqual(plan2.Cast<int>()))
            {
                return false;
            }
        }
        return true;
    }

    private string print(int[,] plan)
    {
        string s = "[";
        for (int i = 0; i < plan.GetLength(0); i++)
        {
            s += "[";
            for (int j = 0; j < plan.GetLength(1); j++)
            {
                s += plan[i, j] + ",";
            }
            s += "]";
        }
        s += "]";
        return s;
    }

}
