using System.Collections;
using UnityEngine;

public enum MonsterType
{
    Monster1,
    Monster2,
    Monster3,
}

public class Monster : MonoBehaviour
{
    //이동 관련 변수
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
    private float initMoveSpeed;

    //처치 시 획득 골드
    [SerializeField]
    private int gold = 10;
    private float parrying = 10.0f;
    
    //기타 컴포넌트
    private MonsterSpawner monsterSpawner;
    private MonsterAnimController monsterAnimController;

    //몬스터 way point 초기 설정
    public void SetUp(Transform[] wayPoints)
    {
        monsterSpawner = FindAnyObjectByType<MonsterSpawner>();
        monsterAnimController = gameObject.GetComponentInChildren<MonsterAnimController>();

        wayPointCnt = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCnt];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;
        StartCoroutine(OnMove());
    }

    private void Start()
    {
        initMoveSpeed = moveSpeed;
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
        while (true)
        {
            //목적지에 도착
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.05f * moveSpeed)
                NextMoveTo();
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        //마지막 노드가 아니라면, 위치 보정 후 방향 수정
        if (currentIndex < wayPointCnt - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            moveDir = direction;

            if (moveDir.x < 0)
                transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            monsterAnimController.SetCurrentAnimation(MonsterAnimState.Run);
        }
        else
            OnDead(MonsterDestroyType.Arrive);
    }

    public void OnDead(MonsterDestroyType type)
    {
        monsterAnimController.SetCurrentAnimation(MonsterAnimState.Dead);
        monsterSpawner.DestoryMonster(type, this, gold);
    }

    public void RestoreMoveSpeed() //이동속도 원상 복구
    {
        moveSpeed = initMoveSpeed;
        Debug.Log("이동속도 원상복구");
    }

    public void DecreaseMoveSpeed(int decreasePercent) //이동속도를 decreasePercent% 감소 
    {
        moveSpeed *= 1 - decreasePercent * 0.01f;
        Debug.Log("이동속도 감소");
    }
}
