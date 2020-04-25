using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class Confirm : MonoBehaviour
{
    public GameObject[] backlights = new GameObject[4];
    private IEnumerator _enumerator;
    

    public void CheckLevel1()
    {
        int index = this.GetComponent<PlanGenerator>().getIndex();
        if (backlights[index].GetComponent<Backlight>().isOn)
        {
            _enumerator = GetComponent<Notification>().show(true);
            this.StartCoroutine(_enumerator);
            _enumerator = loadNext(1);
            this.StartCoroutine(_enumerator);
        }
        else
        {
            _enumerator = GetComponent<Notification>().show(false);
            this.StartCoroutine(_enumerator);
        }
    }

    public void CheckLevel2()
    {
        Plan plan = GetComponent<Building>().getPlan();
        int[,] createdPlan = GameObject.Find("buildingPlan").GetComponent<PlanDrawer>().TranslateImagePlanToPlan();
        int[,] buildingPlan = GetComponent<Building>().TranslateBuildingToPlan();
        if (plan.equals(plan.getPlan(), createdPlan) && plan.equals(plan.getPlan(), buildingPlan))
        {
            _enumerator = GetComponent<Notification>().show(true); 
            this.StartCoroutine(_enumerator);
            _enumerator = loadNext(2);
            this.StartCoroutine(_enumerator);
        }
        else
        {
            _enumerator = GetComponent<Notification>().show(false);
            this.StartCoroutine(_enumerator);
        }
    }

    public void CheckLevel3()
    {
        Plan plan = new Plan(GetComponent<Building>().TranslateBuildingToPlan());
        int floors = plan.numberOfFloors(plan.getPlan());
        int cubes = plan.numberOfCubes(plan.getPlan());
        int[] values = GetComponent<Floors>().values;
        if (values[0] == floors && values[1] == cubes)
        {
            _enumerator = GetComponent<Notification>().show(true);
            this.StartCoroutine(_enumerator);
            _enumerator = loadNext(3);
            this.StartCoroutine(_enumerator);
        }
        else
        {
            _enumerator = GetComponent<Notification>().show(false);
            this.StartCoroutine(_enumerator);
        }
    }
    
    public void CheckLevel4()
    {
        Plan plan = new Plan(GetComponent<Building>().TranslateBuildingToPlan());
        int[] realValues = plan.cubesInFloors(plan.getPlan());
        int[] values = GetComponent<Floors>().values;
        
        if (values.SequenceEqual(realValues))
        {
            _enumerator = GetComponent<Notification>().show(true);
            this.StartCoroutine(_enumerator);
            _enumerator = loadNext(4);
            this.StartCoroutine(_enumerator);
        }
        else
        {
            _enumerator = GetComponent<Notification>().show(false);
            this.StartCoroutine(_enumerator);
        }
    }
    
        private IEnumerator loadNext(int level) 
        {
        yield return new WaitForSecondsRealtime(1);
        if (Data.DATA.createdTask)
        {
            Data.DATA.createdTask = false;
            Application.LoadLevel(level + 4);
        }
        else
        {
            Data.DATA.UpdateData(level, Data.DATA.progressInLvls[level-1]+1);
            Application.LoadLevel(level);
        }
    }
    

}
