using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance;
    private Monster targetMonster;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void SetUp(Monster target)
    {
        targetMonster = target;
        targetTransform = target.transform;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        if (targetMonster.moveDir.x < 0)
            distance = new Vector3(-0.3f, 1.2f, 0.0f);
        else
            distance = new Vector3(0.3f, 1.2f, 0.0f);

        rectTransform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTransform.position + distance) - new Vector2(Screen.width/2f, Screen.height / 2f);
    }
}
