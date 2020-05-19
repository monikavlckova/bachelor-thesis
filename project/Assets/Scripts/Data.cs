using System;
using UnityEngine;
using System.IO;

public class Data : MonoBehaviour
{
    private string path;

    public static Data DATA;
    public int[] progressInLvls = new int[4];
    public int highestLevelCompleted;
    public bool createdTask = false;
    public int[,] createdBuildingPlan;
    public bool buildBuilding;
    public int[] floorsValues;
    public int[,] createdPlan1;
    public int[,] createdPlan2;
    public int[,] createdPlan3;
    public int[,] createdPlan4;
    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "data.txt");
        Debug.Log(path);
        try
        {
            if (!File.Exists(path))
            {
                StreamWriter file = File.CreateText(path);
                file.WriteLine("0\n0\n0\n0\n0");
                file.Close();
            }
        }
        catch (Exception _)
        {
            // ignored
        }

        LoadData();
    }

    public void LoadData()
    {
        path = Path.Combine(Application.persistentDataPath, "data.txt");

        string[] text = File.ReadAllLines(path);
        progressInLvls[0] = int.Parse(text[0]);
        progressInLvls[1] = int.Parse(text[1]);
        progressInLvls[2] = int.Parse(text[2]);
        progressInLvls[3] = int.Parse(text[3]);
        highestLevelCompleted = int.Parse(text[4]);
    }

    public void UpdateData(int level, int progress)
    {
        increaseHighestLevel(level, progress);
        path = Path.Combine(Application.persistentDataPath, "data.txt");
        string data = "";

        for (int i = 0; i < level - 1; i++) data += progressInLvls[i] + "\n";
        data += progress + "\n";
        for (int i = level; i < 4; i++) data += progressInLvls[i] + "\n";
        data += highestLevelCompleted;

        File.WriteAllText(path, data);
        LoadData();
    }

    public void ResetData()
    {
        path = Path.Combine(Application.persistentDataPath, "data.txt");
        string data = "0\n0\n0\n0\n0";

        File.WriteAllText(path, data);
        LoadData();
    }
    
    private void increaseHighestLevel(int level, int progress)
    {
        if (progress > 9 && level > highestLevelCompleted)
        {
            highestLevelCompleted = level;
        }
    }

    void Awake()
    {
        if (DATA == null)
        {
            DontDestroyOnLoad(gameObject);
            DATA = this;
        }
        else if (DATA != this)
        {
            Destroy(gameObject);
        } 
    }
}