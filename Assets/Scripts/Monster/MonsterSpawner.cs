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
    private Wave currentWave; //���� ���̺� ����
    private List<Monster> monsterList; //���� �ʿ� �����ϴ� ��� ���� ����
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
        //���� ���̺꿡�� ������ �� ����
        int spawnMonsterCount = 0;
        while (spawnMonsterCount < currentWave.maxMonsterCount)
        {
            int enemyIdx = (int)currentWave.monsterSequence[spawnMonsterCount];
            GameObject clone = Instantiate(monsterPrefabs[enemyIdx]); //�� ������Ʈ ����
            Monster monster = clone.GetComponent<Monster>();

            monster.SetUp(wayPoints); //waypoint ���� ����
            monsterList.Add(monster);

            SpawnerMonsterHPSlider(clone);

            spawnMonsterCount++;
            yield return new WaitForSeconds(currentWave.spawnTime); //spawntime �ð� ���� ���
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
        //ü�� UI ����
        GameObject sliderClone = Instantiate(monsterHPSliderPrefab);

        //�������� ����
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        //ü�� ���� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().SetUp(monster.transform);
        sliderClone.GetComponent<MonsterHPViewer>().SetUp(monster.GetComponent<MonsterHP>());
    }
}
