using System.Collections;
using UnityEngine;

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
    private bool isStage0;
    [SerializeField]
    private Tutorial tutorial;

    private void Start()
    {
        StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        if (isStage0) //0�������� Ʃ�丮�� ����
        {
            tutorial.OpenTutorial();
            yield return new WaitUntil(() => tutorial.IsEnd);
        }

        while (currentWaveIndex < waves.Length - 1)
        {
            yield return new WaitForSeconds(waveInterval);
            currentWaveIndex++;
            monsterSpawner.StartWave(waves[currentWaveIndex]);
            yield return new WaitUntil(() => monsterSpawner.MonsterList.Count == 0);
        }
        WaveEnd();
    }

    //�������� Ŭ����
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
    public MonsterType[] monsterSequence;
}