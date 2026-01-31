using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Camera mainCamera;
    public Scrollbar cameraDimensionsScrollbar;
    public Mold mold;

    void Update()
    {
        mainCamera.orthographicSize = 5f + cameraDimensionsScrollbar.value * 10;
    }

    public void Play()
    {
        mold.StartSearch();
    }

    public void Close()
    {
        Application.Quit();
    }
}
