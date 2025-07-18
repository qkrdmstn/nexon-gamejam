using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanwer : MonoBehaviour
{
    [SerializeField]
    private GameObject monsterPrefabs;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private int waveNum;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        int waveCnt = 0;
        while (waveCnt < waveNum)
        {
            GameObject colne = Instantiate(monsterPrefabs); //�� ������Ʈ ����
            Monster monster = colne.GetComponent<Monster>();
            monster.SetUp(wayPoints); //waypoint ���� ����
            yield return new WaitForSeconds(spawnTime); //spawntime �ð� ���� ���
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
