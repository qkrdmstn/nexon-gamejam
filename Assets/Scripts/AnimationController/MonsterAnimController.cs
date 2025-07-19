using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//애니메이션 Enum
public enum MonsterAnimState
{
    Run, Dead
}

public class MonsterAnimController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] AnimClip;

    //현재 애니메이션
    private MonsterAnimState _AnimState;
    //현재 애니메이션
    private string currentAnimation;
    private void AsyncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        if (animClip.name.Equals(currentAnimation)) return;

        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        currentAnimation = animClip.name;
    }

    public void SetCurrentAnimation(MonsterAnimState state)
    {
        switch (state)
        {
            case MonsterAnimState.Run:
                AsyncAnimation(AnimClip[(int)MonsterAnimState.Run], true, 1f);
                break;
            case MonsterAnimState.Dead:
                AsyncAnimation(AnimClip[(int)MonsterAnimState.Dead], true, 1f);
                break;
        }
    }

    public void SetMaterialColor(Color color)
    {
        skeletonAnimation.skeleton.SetColor(color);
    }
}
