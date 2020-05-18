using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToScene(int sceneToChange)
    {
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

    public void ShowHide(GameObject obj)
    {
        obj.GetComponent<CanvasGroup>().alpha = obj.GetComponent<CanvasGroup>().alpha == 0f ? 1f : 0f;
        obj.GetComponent<CanvasGroup>().blocksRaycasts = !obj.GetComponent<CanvasGroup>().blocksRaycasts;
        
    }
}
