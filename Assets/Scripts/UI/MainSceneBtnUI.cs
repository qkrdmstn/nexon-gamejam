using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneBtnUI : MonoBehaviour
{
    public void GameStart()
    {
        GameManager.instance.NxtScene();
        SoundManager.Instance.PlayBGM(BGM.CUTSCENE_FIRST);
    }
}
