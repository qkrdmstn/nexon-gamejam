using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gold;
    public float curHP;
    public float maxHP;
    [SerializeField] List<TowerShopIcon> towershops;
    public event Action OnGolded; //골드 획득했을 때 콜백 이벤트
    public event Action OnDamaged; //HP 닳았을 때 콜백 이벤트

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

    public void OnDamage(float damage)
    {
        curHP -= damage;
        OnDamaged.Invoke();
    }
}
