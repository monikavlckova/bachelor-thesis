using UnityEngine;
using UnityEngine.UI;

public class Floors : MonoBehaviour
{
    public Text[] textFloors = new Text[5];
    public int[] values = new int[5];
    public int level;
    public Button[] buttons = new Button[10]; 

    private void Start()
    {
        if (Data.DATA.progressInLvls[level - 1] % 2 == 1 && !Data.DATA.createdTask)
        {
            Plan plan = GetComponent<Building>().getPlan();
            if (level == 3)
            {
                values[0] = plan.numberOfFloors(plan.getPlan());
                values[1] = plan.numberOfCubes(plan.getPlan());
            }
            if (level == 4)
            {
                values = plan.cubesInFloors(plan.getPlan());
            }
            for (int i = 0; i < values.Length; i++)
            {
                textFloors[i].text = values[i].ToString();
            }
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].enabled = false;
            }
        }
    }

    public void increase(int floor)
    {
        values[floor - 1] += 1;
        textFloors[floor - 1].text = values[floor - 1].ToString();
    }
    
    public void decrease(int floor)
    {
        if (values[floor - 1] > 0)
        {
            values[floor - 1] -= 1;
            textFloors[floor - 1].text = values[floor - 1].ToString();
        }
    }
    
    

}
