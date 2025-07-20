using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClick : MonoBehaviour, IPointerClickHandler
{
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX(SFX.CLICK);
        Debug.Log("!!!!");
    }
}
