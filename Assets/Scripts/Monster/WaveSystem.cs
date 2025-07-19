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
    private int uiInterval;
    public WaveUI waveUI;
    [SerializeField]
    private bool isStage0;
    [SerializeField]
    private Tutorial tutorial;

    private void Start()
    {
        waveUI = FindObjectOfType<WaveUI>();
        StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        if (isStage0) //0스테이지 튜토리얼 진입
        {
            tutorial.OpenTutorial();
            yield return new WaitUntil(() => tutorial.IsEnd);
        }

        while (currentWaveIndex < waves.Length - 1)
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
    public MonsterType[] monsterSequence;
}