using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToScene(int sceneToChange)
    {
        SceneManager.LoadScene(sceneToChange);
    }
    
    public void ChangeToBetweenScene(int sceneToChange)
    {
        if (sceneToChange > 0 && sceneToChange < 5 && Data.DATA.progressInLvls[sceneToChange] == 10 )
        {
            SceneManager.LoadScene(sceneToChange+8);
        }
        else SceneManager.LoadScene(sceneToChange);
    }
    
    public void DiscardCreatedTask()
    {
        Data.DATA.createdTask = false;
    }
    
    public void Reset()
    {
        Data.DATA.ResetData();
    }

    public void ResetLevel(int level)
    {
        Data.DATA.UpdateData(level, 0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowHide(GameObject obj)
    {
        obj.GetComponent<CanvasGroup>().alpha = obj.GetComponent<CanvasGroup>().alpha == 0f ? 1f : 0f;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = !obj.GetComponent<CanvasGroup>().blocksRaycasts;
        
    }
}
