using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void SetUp(Transform target)
    {
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        rectTransform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTransform.position + distance) - new Vector2(Screen.width/2f, Screen.height / 2f);
    }
}
