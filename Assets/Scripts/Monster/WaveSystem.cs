using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private MonsterSpawner monsterSpawner;
    [SerializeField]
    private int currentWaveIndex = -1;
    [SerializeField]
    private int waveInterval;
    [SerializeField]
    private int uiInterval;

    public WaveUI waveUI;

    private void Start()
    {
        waveUI = FindObjectOfType<WaveUI>();
        StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        while(currentWaveIndex < waves.Length - 1)
        {
            waveUI.UpdateUI(currentWaveIndex + 1);
            waveUI.SetActiveUI(true);
            yield return new WaitForSeconds(uiInterval);
            waveUI.SetActiveUI(false);

            yield return new WaitForSeconds(waveInterval - uiInterval);
            currentWaveIndex++;
            monsterSpawner.StartWave(waves[currentWaveIndex]);
            yield return new WaitUntil(() => monsterSpawner.MonsterList.Count == 0);

        }
        WaveEnd();
    }


    //스테이지 클리어
    public void WaveEnd()
    {
        GameManager.instance.StageClear();
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxMonsterCount;
    public MonsterType [] monsterSequence;
}