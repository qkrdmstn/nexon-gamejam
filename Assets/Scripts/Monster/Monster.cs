using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MonsterType
{
    Monster1,
    Monster2,
    Monster3,
}

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private int wayPointCnt;
    [SerializeField]
    private int currentIndex;
    [SerializeField]
    private Vector3 moveDir;
    [SerializeField]
    private float moveSpeed;
    public float arrivedDamage;

    private MonsterSpawner monsterSpawner;
    [SerializeField]
    int gold = 10;

    //���� way point �ʱ� ����
    public void SetUp(Transform[] wayPoints)
    {
        monsterSpawner = FindAnyObjectByType<MonsterSpawner>();

        wayPointCnt = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCnt];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;
        StartCoroutine(OnMove());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private IEnumerator OnMove()
    {
        //���� ���� �̵�
        NextMoveTo();
        while(true)
        {
            //���� ȸ��
            //transform.Rotate(Vector3.forward * 10);
            
            //�������� ����
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * moveSpeed)
                NextMoveTo();
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        //������ ��尡 �ƴ϶��, ��ġ ���� �� ���� ����
        if (currentIndex < wayPointCnt - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            moveDir = direction;
        }
        else
            OnDead(MonsterDestroyType.Arrive);
    }

    public void OnDead(MonsterDestroyType type)
    {
        monsterSpawner.DestoryMonster(type, this, gold);
    }
}
