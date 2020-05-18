using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BuildCube : MonoBehaviour
{
    public GameObject newCube;

    public bool locked = false;
    public bool destroyMode = false;
    public Sprite destroyImage;
    public Sprite buildImage;
    public Button button;
    public int SIZE = 5;
    
    private Vector2 TouchStartPosition;
    private Vector2 TouchEndPosition;
    private Vector3 MouseStartPosition;
    private Vector3 MouseEndPosition;
    // Update is called once per frame
    void Update()
    {
        SetPositions();

        if ((!locked && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !TouchMoved())
            || (!locked && Input.touchCount == 0 && Input.GetMouseButtonUp(0))&& !MouseMoved())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 offset = new Vector3(0.5f, 0f, 0.5f);
                Vector3 cubePosition = hit.point + hit.normal / 2.0f;
                
                if (SIZE % 2 == 0) cubePosition += offset;

                cubePosition.x = (float) Math.Round(cubePosition.x, MidpointRounding.AwayFromZero);
                cubePosition.y = (float) Math.Round(cubePosition.y, MidpointRounding.AwayFromZero);
                cubePosition.z = (float) Math.Round(cubePosition.z, MidpointRounding.AwayFromZero);

                if (SIZE % 2 == 0) cubePosition -= offset;
                
                Vector3 touchedCubePosition = cubePosition - hit.normal;
                
                if (destroyMode) DestroyCube(touchedCubePosition);
                
                else if(CanBuildCube(cubePosition)) Instantiate(newCube, cubePosition, Quaternion.identity, this.transform);
            }
        }
    }

    private bool TouchMoved()
    {
        Vector2 delta = TouchStartPosition - TouchEndPosition;
        return delta.x < -15f || delta.x > 15f || delta.y < -15f || delta.y > 15f;
    }

    private bool MouseMoved()
    {
        Vector3 delta = MouseStartPosition - MouseEndPosition;
        return delta.x != 0 || delta.y != 0;
    }

    private void SetPositions()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) TouchStartPosition = touch.position;
            if (touch.phase == TouchPhase.Ended) TouchEndPosition = touch.position;
        }
        if (Input.GetMouseButtonDown(0)) MouseStartPosition = Input.mousePosition;
        if (Input.GetMouseButtonUp(0)) MouseEndPosition = Input.mousePosition;
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

    private bool CanBuildCube(Vector3 cubePosition)
    {
        return cubePosition.y > -1 && cubePosition.y < 4 &&
               cubePosition.x > (-SIZE - 1) / 2f && cubePosition.x < (SIZE + 1) / 2f &&
               cubePosition.z > (-SIZE - 1) / 2f && cubePosition.z < (SIZE + 1) / 2f &&
               (cubePosition.y.Equals(0) || UnderCubeIsCube(cubePosition));
    }

    private void DestroyCube(Vector3 cubePosition)
    {
        foreach (Transform cube1 in transform)
        {
            if (cube1.transform.position.x.Equals(cubePosition.x) &&
                (cube1.transform.position.y >= (cubePosition.y)) &&
                cube1.transform.position.z.Equals(cubePosition.z))
            {
                GameObject.Destroy(cube1.gameObject);
            }
        }
    }
    public void ChangeDestroyModeState()
    {
        destroyMode = !destroyMode;
        if (destroyMode) button.GetComponent<Image>().sprite = buildImage;
        else button.GetComponent<Image>().sprite = destroyImage;
    }

    public void Lock()
    {
        locked = true;
        button.GetComponent<Image>().enabled = false;
        button.enabled = false;
        button.transform.parent.GetComponent<Image>().enabled = false;
    }
    
    public void Unlock()
    {
        locked = false;
        button.GetComponent<Image>().enabled = true;
        button.enabled = true;
        button.transform.parent.GetComponent<Image>().enabled = true;
    }

}
