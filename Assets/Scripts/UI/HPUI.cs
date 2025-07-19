using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Image[] images;
    public Sprite[]  sprites;
    // Start is called before the first frame update
    void Start()
    {
        HPUIUpdate();
        GameManager.instance.OnHPChanged += HPUIUpdate;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnHPChanged -= HPUIUpdate;
    }

    private void HPUIUpdate()
    {
        int curHP = GameManager.instance.curHP;
        int i = 0;
        for (i = 0; i < curHP / 2; i++)
        {
            images[i].sprite = sprites[0];
            images[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        if(curHP % 2 == 1)
        {
            images[i].sprite = sprites[1];
            images[i++].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        while (i < images.Length)
            images[i++].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }
}
