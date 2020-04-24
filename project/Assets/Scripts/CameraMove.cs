using System;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float intensity = 0.25f;
    public float dempening = 10.0f;
    private Touch touch;
    private Rect touchZone;
    private Vector3 rotation;
    
    private void Start()
    {
        touchZone = new Rect(0, 0, Screen.width*0.5f, Screen.height);
    }

    public void topView()
    {
        rotation.x = 90;
        rotation.y = 0;
    }
    
    public void frontView()
    {
        rotation.x = 0;
        rotation.y = 0; 
    }
    
    public void sideView()
    {
        rotation.x = 0;
        rotation.y = 270;
    }
    
    void Update() {

        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved && touchZone.Contains(touch.position))
            {
                rotation.y += touch.deltaPosition.x * intensity;
                rotation.x -= touch.deltaPosition.y * intensity;
                
                if (rotation.x < 0f)
                    rotation.x = 0f;
                else if (rotation.x > 90f)
                    rotation.x = 90f;
            }

            if (Input.touchCount == 2)
            {
                
            }

            {
                
            }
        }
        Quaternion rotationXY = Quaternion.Euler(rotation.x, rotation.y, 0);
        this.transform.parent.rotation = Quaternion.Lerp(this.transform.parent.rotation, rotationXY, Time.deltaTime*dempening);
    }
}