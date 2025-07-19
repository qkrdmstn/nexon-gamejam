using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOverUI : MonoBehaviour
{
    public GameObject uiObj;
    public void Retry()
    {
        GameManager.instance.ReloadScene();
    }

    public void SetActiveUI()
    {
        uiObj.SetActive(true);
    }

    public void SetInActiveUI()
    {
        uiObj.SetActive(false);
    }
}
