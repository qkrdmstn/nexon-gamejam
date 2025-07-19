using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//애니메이션 Enum
public enum PlayerAnimState
{
    Idle, Run, Hit, Dead //front idle, run hit dead, back idle, run, hit 순
}

public enum PlayerXDir
{
    Left, Right
}

public enum PlayerYDir
{
    Front, Back
}

public class PlayerAnimController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimationFront;
    public SkeletonAnimation skeletonAnimationBack;
    public AnimationReferenceAsset[] AnimClip;

    //현재 애니메이션
    private MonsterAnimState _AnimState;
    //현재 애니메이션
    private string currentAnimation;
    private void AsyncAnimation(SkeletonAnimation skeletonAnimation, AnimationReferenceAsset animClip, bool loop, float timeScale)
    {
        if (animClip.name.Equals(currentAnimation)) return;

        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;
        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        currentAnimation = animClip.name;
    }

    public void SetCurrentAnimation(PlayerXDir xDir, PlayerYDir yDir, PlayerAnimState state)
    {
        Debug.Log(xDir + " " + yDir + " " + state);
        if(yDir == PlayerYDir.Front)
        {
            skeletonAnimationFront.gameObject.SetActive(true);
            skeletonAnimationBack.gameObject.SetActive(false);
            if(xDir == PlayerXDir.Left)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            if (state == PlayerAnimState.Idle) AsyncAnimation(skeletonAnimationFront, AnimClip[0], true, 1f);
            else if (state == PlayerAnimState.Run) AsyncAnimation(skeletonAnimationFront, AnimClip[1], true, 1f);
            else if (state == PlayerAnimState.Hit) AsyncAnimation(skeletonAnimationFront, AnimClip[2], false, 1f);
            else if (state == PlayerAnimState.Dead) AsyncAnimation(skeletonAnimationFront, AnimClip[3], false, 1f);
            
        }
        else if(yDir == PlayerYDir.Back)
        {
            skeletonAnimationBack.gameObject.SetActive(true);
            skeletonAnimationFront.gameObject.SetActive(false);
            if (xDir == PlayerXDir.Left)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            if (state == PlayerAnimState.Idle) AsyncAnimation(skeletonAnimationBack, AnimClip[4], true, 1f);
            else if (state == PlayerAnimState.Run) AsyncAnimation(skeletonAnimationBack, AnimClip[5], true, 1f);
            else if (state == PlayerAnimState.Hit) AsyncAnimation(skeletonAnimationBack, AnimClip[6], false, 1f);
        }
    }

    public void SetMaterialColor(Color color)
    {
        if(skeletonAnimationFront.gameObject.activeSelf)
            skeletonAnimationFront.skeleton.SetColor(color);
        if(skeletonAnimationBack.gameObject.activeSelf)
            skeletonAnimationBack.skeleton.SetColor(color);
    }
}
