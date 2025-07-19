using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearUI : MonoBehaviour
{
    public GameObject uiObj;
    
    public void NxtStage()
    {
        GameManager.instance.NxtScene();
    }

    public void BackToHome()
    {
        GameManager.instance.ChangeScene(SceneType.Main);
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
