using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gold;
    public float curHP;
    public float maxHP;
    [SerializeField] List<TowerShopIcon> towershops;

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

    // Update is called once per frame
    void Update()
    {

    }

    public void GetGold(int num)
    {
        gold += num;
        towershops.ForEach(ts => ts.CheckPurchase(gold));
    }

    public void UseGold(int num)
    {
        gold -= num;
        towershops.ForEach(ts => ts.CheckPurchase(gold));
    }

    public void OnDamage(float damage)
    {
        curHP -= damage;
    }
}
