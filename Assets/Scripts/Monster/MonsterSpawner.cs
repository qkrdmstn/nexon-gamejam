using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterDestroyType
{
    Arrive,
    Kill
}
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject [] monsterPrefabs;
    [SerializeField]
    private GameObject monsterHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private Transform[] wayPoints;
    private Wave currentWave; //현재 웨이브 정보
    private List<Monster> monsterList; //현재 맵에 존재하는 모든 적의 정보
    [SerializeField]
    private GameObject goldPrefab;

    public List<Monster> MonsterList => monsterList;
    // Start is called before the first frame update
    void Awake()
    {
        monsterList = new List<Monster>();
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        //현재 웨이브에서 생성한 적 숫자
        int spawnMonsterCount = 0;
        while (spawnMonsterCount < currentWave.maxMonsterCount)
        {
            int enemyIdx = (int)currentWave.monsterSequence[spawnMonsterCount];
            GameObject clone = Instantiate(monsterPrefabs[enemyIdx]); //적 오브젝트 생성
            Monster monster = clone.GetComponent<Monster>();

            monster.SetUp(wayPoints); //waypoint 정보 설정
            monsterList.Add(monster);

            SpawnerMonsterHPSlider(clone);

            spawnMonsterCount++;
            yield return new WaitForSeconds(currentWave.spawnTime); //spawntime 시간 동안 대기
        }
    }
    public void DestoryMonster(MonsterDestroyType type, Monster monster, int gold)
    {
        if (type == MonsterDestroyType.Arrive)
        {
            GameManager.instance.OnDamage(monster.arrivedDamage);
        }
        else if (type == MonsterDestroyType.Kill)
        {
            GameObject goldObj = Instantiate(goldPrefab, monster.transform.position, Quaternion.identity);
            goldObj.GetComponent<Gold>().quantity = gold;
        }

        monsterList.Remove(monster);
        Destroy(monster.gameObject);
    }

    private void SpawnerMonsterHPSlider(GameObject monster)
    {
        //체력 UI 생성
        GameObject sliderClone = Instantiate(monsterHPSliderPrefab);

        //계층구조 설정
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        //체력 몬스터 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().SetUp(monster.transform);
        sliderClone.GetComponent<MonsterHPViewer>().SetUp(monster.GetComponent<MonsterHP>());
    }
}
