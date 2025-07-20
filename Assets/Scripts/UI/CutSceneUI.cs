using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneUI : MonoBehaviour
{
    public Sprite [] sprites;
    public Image img;
    public int curIdx;

    private void Start()
    {
        curIdx = 0;
    }

    public void NxtBtn()
    {
        if (curIdx == sprites.Length - 1)
        {
            GameManager.instance.NxtScene();
            return;
        }
        curIdx = Mathf.Min(curIdx + 1, sprites.Length - 1);
        img.sprite = sprites[curIdx];
    }

    public void PrevBtn()
    {
        curIdx = Mathf.Max(curIdx - 1, 0);
        img.sprite = sprites[curIdx];
    }
}
