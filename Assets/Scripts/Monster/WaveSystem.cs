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

    private void Start()
    {
        StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        while(currentWaveIndex < waves.Length - 1)
        {
            yield return new WaitForSeconds(waveInterval);
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