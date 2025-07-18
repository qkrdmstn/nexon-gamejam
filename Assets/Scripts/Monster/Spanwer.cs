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
            GameObject colne = Instantiate(monsterPrefabs); //적 오브젝트 생성
            Monster monster = colne.GetComponent<Monster>();
            monster.SetUp(wayPoints); //waypoint 정보 설정
            yield return new WaitForSeconds(spawnTime); //spawntime 시간 동안 대기
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
