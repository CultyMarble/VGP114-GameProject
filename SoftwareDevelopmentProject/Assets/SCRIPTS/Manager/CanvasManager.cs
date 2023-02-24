using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject mainViewUI;

    //===========================================================================
    public void StartGame()
    {
        if (mainViewUI.activeInHierarchy)
            mainViewUI.SetActive(false);

        // Reset Game Parameters
    }
}