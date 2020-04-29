using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToScene(int sceneToChange)
    {
        //Application.LoadLevel(sceneToChange);
        SceneManager.LoadScene(sceneToChange);
    }
    
    public void DiscardCreatedTask()
    {
        Data.DATA.createdTask = false;
    }
    
    public void Reset()
    {
        Data.DATA.ResetData();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
