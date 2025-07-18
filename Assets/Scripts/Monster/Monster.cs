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

    //���� way point �ʱ� ����
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
