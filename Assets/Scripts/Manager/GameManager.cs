using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gold;
    public float curHP;
    public float maxHP;

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
    }

    public void OnDamage(float damage)
    {
        curHP -= damage;
    }
}
