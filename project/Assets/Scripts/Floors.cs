using UnityEngine;
using UnityEngine.UI;

public class Floors : MonoBehaviour
{
    public Text[] textFloors = new Text[4];
    public int[] values = new int[4];
    public int level;
    public Button[] buttons = new Button[8]; 

    private void Start()
    {
        if (Data.DATA.progressInLvls[level - 1] % 2 == 1 || (Data.DATA.createdTask && !Data.DATA.buildBuilding))
        {
            if (!Data.DATA.createdTask)
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
            }
            else
            {
                values = Data.DATA.floorsValues;
            }
            for (int i = 0; i < values.Length; i++)
            {
                textFloors[i].text = values[i].ToString();
            }
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].enabled = false;
                buttons[i].image.enabled = false;
            }
        }
    }

    public void increase(int floor)
    {
        if ((level == 3 && floor == 1 && values[floor-1] < 4) || level == 4 || (level == 3  && floor != 1))
        {
            values[floor - 1] += 1;
            textFloors[floor - 1].text = values[floor - 1].ToString();
        }
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
