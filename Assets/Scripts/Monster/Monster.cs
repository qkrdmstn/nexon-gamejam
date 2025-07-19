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
    //�̵� ���� ����
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
    private bool isDead;

    //óġ �� ȹ�� ���
    [SerializeField]
    private int gold = 10;
    public float parrying = 10.0f;
    
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
        if (isDead) return;
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
        StartCoroutine(OnDeadCoroutine(type));
    }

    public IEnumerator OnDeadCoroutine(MonsterDestroyType type)
    {
        isDead = true;
        if (type == MonsterDestroyType.Kill)
        {
            monsterAnimController.SetCurrentAnimation(MonsterAnimState.Dead);
            yield return new WaitForSeconds(0.7f);
        }
        monsterSpawner.DestoryMonster(type, this, gold);
    }

    public void RestoreMoveSpeed() //�̵��ӵ� ���� ����
    {
        moveSpeed = initMoveSpeed;
        Debug.Log("�̵��ӵ� ���󺹱�");
    }

    public void DecreaseMoveSpeed(int decreasePercent) //�̵��ӵ��� decreasePercent% ���� 
    {
        moveSpeed *= 1 - decreasePercent * 0.01f;
        Debug.Log("�̵��ӵ� ����");
    }
}
