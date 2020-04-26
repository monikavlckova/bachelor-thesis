using System.Collections;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToScene(int sceneToChange)
    {
        Application.LoadLevel(sceneToChange);
    }
    
    public void DiscardCreatedTask()
    {
        Data.DATA.createdTask = false;
    }
    
    public void Reset()
    {
        Data.DATA.ResetData();
    }
}
