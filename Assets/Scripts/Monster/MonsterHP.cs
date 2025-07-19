using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    private float currentHP;
    private bool isDead = false;
    private float hitDuration = 0.5f;
    private Monster monster;
    private MonsterAnimController monsterAnimController;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
        monster = GetComponent<Monster>();
        monsterAnimController = GetComponent<MonsterAnimController>();
    }

    public void OnDamage(float damage)
    {
        if (isDead) return;
        currentHP -= damage;

        StopCoroutine(DamagedProcess(hitDuration));
        StartCoroutine(DamagedProcess(hitDuration));
        if(currentHP <= 0)
        {
            isDead = true;
            monster.OnDead(MonsterDestroyType.Kill);
        }
    }

    IEnumerator DamagedProcess(float duration)
    {
        for (int i = 0; i < 2; i++)
        {
            //적의 투명도 조정
            monsterAnimController.SetMaterialColor(new Color(1, 1, 1, 0.4f));
            yield return new WaitForSeconds(duration / 4.0f);

            monsterAnimController.SetMaterialColor(new Color(1, 1, 1, 1f));

            yield return new WaitForSeconds(duration / 4.0f);
        }
    }

}
