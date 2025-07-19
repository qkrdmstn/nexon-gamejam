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
    public event Action OnGolded; //골드 획득했을 때 콜백 이벤트
    public event Action OnHPChanged; //HP 닳았을 때 콜백 이벤트
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        { //생성 전이면
            instance = this; //생성
        }
        else if (instance != this)
        { //이미 생성되어 있으면
            Destroy(this.gameObject); //새로만든거 삭제
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        SetUp();
        SoundManager.Instance.PlayBGM(BGM.STAGE);
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
        curHP = Mathf.Min(maxHP, curHP + num);
        OnHPChanged.Invoke();
    }

    public void OnDamage(int damage = 1)
    {
        curHP -= damage;
        impulseSource.GenerateImpulse();
        OnHPChanged.Invoke();
        if (curHP <= 0)
            GameOver();
    }

    public void GameOver()
    {
        Debug.Log("GameOVer");
    }
}
