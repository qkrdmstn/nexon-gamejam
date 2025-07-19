using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public bool IsEnd { get; private set; }

    [SerializeField] GameObject tutorialCanvas;
    [SerializeField] List<GameObject> pages;
    [SerializeField] Button btnL;
    [SerializeField] Button btnR;
    [SerializeField] Button btnClose;
    private int curPageIdx;

    private void Start()
    {
        tutorialCanvas.SetActive(false);
        btnL.interactable = false;
        btnR.interactable = true;
        btnClose.gameObject.SetActive(false);
        pages.ForEach(page => page.SetActive(false));
        pages[0].SetActive(true);
        curPageIdx = 0;
        IsEnd = false;
    }

    public void OpenTutorial()
    {
        tutorialCanvas.SetActive(true);
    }

    public void OnClickArrowBtn(bool isNext) //페이지 이동 콜백
    {
        if (isNext) //오른쪽 버튼인 경우
        {
            pages[curPageIdx++].SetActive(false);
            pages[curPageIdx].SetActive(true);
            if (curPageIdx + 1 == pages.Count)
                btnClose.gameObject.SetActive(true);
        }
        else //왼쪽 버튼인 경우
        {
            pages[curPageIdx--].SetActive(false);
            pages[curPageIdx].SetActive(true);
        }

        //좌우 버튼 활성화 유무 결정
        if (curPageIdx >= 1) btnL.interactable = true;
        else btnL.interactable = false;

        if (curPageIdx < pages.Count - 1) btnR.interactable = true;
        else btnR.interactable = false;
    }

    public void OnClickBtnX()
    {
        tutorialCanvas.SetActive(false);
        IsEnd = true;
    }
}
