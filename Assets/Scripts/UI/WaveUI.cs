using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public GameObject ui;
    public Sprite[] waveImages; 
    public void SetActiveUI(bool flag)
    {
        ui.SetActive(flag);
    }

    public void UpdateUI(int curWaveIdx)
    {
        ui.GetComponent<Image>().sprite = waveImages[curWaveIdx];
    }
}
