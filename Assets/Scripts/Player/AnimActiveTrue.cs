using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class AnimActiveTrue : MonoBehaviour
{
    private void OnDisable()
    {
        GetComponent<SkeletonAnimation>().skeleton.SetColor(new Color(1.0f, 1.0f, 1.0f, 1.0f));
    }
}
