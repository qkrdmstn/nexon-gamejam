using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    private float currentHP;
    private bool isDead = false;
    private Monster monster;
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
        monster = GetComponent<Monster>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnDamage(float damage)
    {
        if (isDead) return;
        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");
        if(currentHP <= 0)
        {
            isDead = true;
            monster.OnDead(MonsterDestroyType.Kill);
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;

        //적의 투명도 조정
        color.a = 0.4f;
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}
