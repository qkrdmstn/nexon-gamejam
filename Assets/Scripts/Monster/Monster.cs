using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Life Info")]
    public int maxHP;
    public int curHP;

    [Header("Move Info")]
    public Transform[] wayPoints;
    public int wayPointCnt;
    public int currentIndex;
    public Vector3 moveDir;
    public float moveSpeed;

    //몬스터 way point 초기 설정
    public void SetUp(Transform[] wayPoints)
    {
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
        //다음 노드로 이동
        NextMoveTo();
        while(true)
        {
            //몬스터 회전
            //transform.Rotate(Vector3.forward * 10);
            
            //목적지에 도착
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * moveSpeed)
                NextMoveTo();
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        //마지막 노드가 아니라면, 위치 보정 후 방향 수정
        if(currentIndex < wayPointCnt - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            moveDir = direction;
        }
        else
            Destroy(gameObject);
    }

    private void OnDamaged(int damage)
    {
        curHP -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            OnDamaged(1);
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
