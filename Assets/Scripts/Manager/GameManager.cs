using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gold;
    public int curHP;
    public int maxHP;

    public CinemachineImpulseSource impulseSource;
    [SerializeField] List<TowerShopIcon> towershops;
    public event Action OnGolded; //��� ȹ������ �� �ݹ� �̺�Ʈ
    public event Action OnDamaged; //HP ����� �� �ݹ� �̺�Ʈ
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        { //���� ���̸�
            instance = this; //����
        }
        else if (instance != this)
        { //�̹� �����Ǿ� ������
            Destroy(this.gameObject); //���θ���� ����
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        SetUp();
    }

    public void SetUp()
    {
        curHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetGold(int num)
    {
        gold += num;
        towershops.ForEach(ts => ts.CheckPurchase(gold));
        OnGolded.Invoke();
    }

    public void UseGold(int num)
    {
        gold -= num;
        towershops.ForEach(ts => ts.CheckPurchase(gold));
        OnGolded.Invoke();
    }

    public void RecoverHP(int num = 1)
    {
        curHP = Mathf.Max(maxHP, curHP + num);
    }

    public void OnDamage(int damage = 1)
    {
        curHP -= damage;
        impulseSource.GenerateImpulse();
        OnDamaged.Invoke();
        if (curHP <= 0)
            GameOver();
    }

    public void GameOver()
    {
        Debug.Log("GameOVer");
    }
}
