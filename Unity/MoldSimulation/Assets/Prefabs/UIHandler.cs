using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Camera mainCamera;
    public Scrollbar cameraDimensionsScrollbar;
    public Mold mold;
    public int useAstar = 0;
    public Text useAstarButtonText;
    public FixedJoystick joystick;

    void Update()
    {
        mainCamera.orthographicSize = 5f + cameraDimensionsScrollbar.value * 10;
    }

    void FixedUpdate()
    {
        UnityEngine.Vector3 cameraMovement = UnityEngine.Vector3.up * joystick.Vertical + UnityEngine.Vector3.right * joystick.Horizontal;
        mainCamera.transform.position += cameraMovement * 3f * Time.fixedDeltaTime;
    }

    public void ToggleAStarButton()
    {
        if(useAstar == 0)
        {
            useAstar = 1;
            useAstarButtonText.text = "Use BFS";
        }
        else if(useAstar == 1)
        {
            useAstar = 2;
            useAstarButtonText.text = "Use BFS + A*";
        }
        else
        {
            useAstar = 0;
            useAstarButtonText.text = "Use A*";
        }
    }

    public void Play()
    {
        mold.StartSearch(useAstar);
    }

    public void Close()
    {
        Application.Quit();
    }
}
