using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildCube : MonoBehaviour
{
    public GameObject newCube;

    public bool locked = false;
    public Sprite lockImage;
    public Sprite unlockImage;
    public Button button;

    public int SIZE = 5;
    
    // Update is called once per frame
    void Update()
    {
        if (!locked && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                
                Vector3 offset = new Vector3(0.5f, 0f, 0.5f);
                Vector3 cubePosition = hit.point + hit.normal/2.0f;
                if (SIZE % 2 == 0) cubePosition = hit.point + offset + hit.normal/2.0f;
                
                cubePosition.x = (float) Math.Round(cubePosition.x, MidpointRounding.AwayFromZero);
                cubePosition.y = (float) Math.Round(cubePosition.y, MidpointRounding.AwayFromZero);
                cubePosition.z = (float) Math.Round(cubePosition.z, MidpointRounding.AwayFromZero);

                if (SIZE % 2 == 0)
                {
                    cubePosition.x -= 0.5f;
                    cubePosition.z -= 0.5f;
                }

                if (cubePosition.y > -1 && cubePosition.y < 4 &&
                    cubePosition.x > (-SIZE-1)/2f && cubePosition.x < (SIZE+1)/2f &&
                    cubePosition.z > (-SIZE-1)/2f && cubePosition.z < (SIZE+1)/2f)
                {
                    if (cubePosition.y.Equals(0) || UnderCubeIsCube(cubePosition))
                    {
                        GameObject cube = (GameObject) Instantiate(newCube, cubePosition, Quaternion.identity);
                        cube.transform.parent = this.transform;
                    }
                }
            }
        }
    }

    private bool UnderCubeIsCube(Vector3 cubePosition)
    {
        foreach (Transform cube1 in transform)
        {
            if (cube1.transform.position.x.Equals(cubePosition.x) &&
                (cube1.transform.position.y.Equals(cubePosition.y - 1)) &&
                cube1.transform.position.z.Equals(cubePosition.z))
            {
                return true;
            }
        }

        return false;
    }

    public void ChangeLockedState()
    {
        locked = !locked;
        if (locked) button.GetComponent<Image>().sprite = lockImage;
        else button.GetComponent<Image>().sprite = unlockImage;
    }

    public void Lock()
    {
        locked = true;
    }
    
    public void Unlock()
    {
        locked = false;
    }

}
