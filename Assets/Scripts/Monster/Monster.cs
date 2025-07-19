using System.Collections;
using UnityEngine;

public enum MonsterType
{
    Goblin,
    Slime,
    Zombie,
    Skeleton,
}

public class Monster : MonoBehaviour
{
    //�̵� ���� ����
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private int wayPointCnt;
    [SerializeField]
    private int currentIndex;
    [SerializeField]
    public Vector3 moveDir;
    [SerializeField]
    private float moveSpeed;
    private float initMoveSpeed;

    //óġ �� ȹ�� ���
    [SerializeField]
    private int gold = 10;
    private float parrying = 10.0f;

    //��Ÿ ������Ʈ
    private MonsterSpawner monsterSpawner;
    private MonsterAnimController monsterAnimController;

    //���� way point �ʱ� ����
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
        //���� ���� �̵�
        NextMoveTo();
        while (true)
        {
            //�������� ����
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.05f * moveSpeed)
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

    public void RestoreMoveSpeed() //�̵��ӵ� ���� ����
    {
        moveSpeed = initMoveSpeed;
    }

    public void DecreaseMoveSpeed(int decreasePercent) //�̵��ӵ��� decreasePercent%��ŭ ���� 
    {
        moveSpeed = initMoveSpeed * (1 - decreasePercent * 0.01f);
    }
}
