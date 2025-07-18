using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private MonsterSpawner monsterSpawner;
    [SerializeField]
    private int currentWaveIndex = -1;

    public void StartWave()
    {
        if(monsterSpawner.MonsterList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            //인덱스 시작이 -1이므로, 먼저 인덱스 증가
            currentWaveIndex++;
            monsterSpawner.StartWave(waves[currentWaveIndex]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            StartWave();
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxMonsterCount;
    public MonsterType [] monsterSequence;
}