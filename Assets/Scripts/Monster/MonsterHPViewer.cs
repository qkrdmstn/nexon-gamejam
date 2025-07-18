using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPViewer : MonoBehaviour
{
    private MonsterHP monsterHP;
    private Slider hpSlider;

    public void SetUp(MonsterHP monsterHP)
    {
        this.monsterHP = monsterHP; ;
        hpSlider = GetComponent<Slider>();
    }

    void Update()
    {
        hpSlider.value = monsterHP.CurrentHP / monsterHP.MaxHP;
    }
}
