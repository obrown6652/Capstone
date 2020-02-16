using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour
{
    public Animator MyAnimator { get; set; }

    private SpriteRenderer spriteRenderer;

    private Animator parentAnimator;

    private AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentAnimator = GetComponentInParent<Animator>();
        MyAnimator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);

        MyAnimator.runtimeAnimatorController = animatorOverrideController;
    }

    public void ActivateLayer(string layerName)
    {
        //for all the layer set them to inactive
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }
        //make only one layer active
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }
    public void SetXAndY(float x, float y)
    {
        MyAnimator.SetFloat("moveX", x);
        MyAnimator.SetFloat("moveY", y);
        setMoving(true);
    }
    public void setMoving(bool Boolean)
    {
        MyAnimator.SetBool("moving", Boolean);
    }
    public void Equip(AnimationClip[] animations)
    {
        spriteRenderer.color = Color.white;

        // animatorOverrideController["Attack_Down"] = animations[0];
        // animatorOverrideController["Attack_Left"] = animations[0];
        //animatorOverrideController["Attack_Right"] = animations[0];
        //animatorOverrideController["Attack_Up"] = animations[0];

        animatorOverrideController["Idle_Down"] = animations[0];
        animatorOverrideController["Idle_Left"] = animations[1];
        animatorOverrideController["Idle_Right"] = animations[2];
        animatorOverrideController["Idle_Up"] = animations[3];

        animatorOverrideController["Walk_Down"] = animations[4];
        animatorOverrideController["Walk_Left"] = animations[5];
        animatorOverrideController["Walk_Right"] = animations[6];
        animatorOverrideController["Walk_Up"] = animations[7];
    }

    public void Dequip()
    {
        animatorOverrideController["Idle_Down"] = null;
        animatorOverrideController["Idle_Left"] = null;
        animatorOverrideController["Idle_Right"] = null;
        animatorOverrideController["Idle_Up"] = null;

        animatorOverrideController["Walk_Down"] = null;
        animatorOverrideController["Walk_Left"] = null;
        animatorOverrideController["Walk_Right"] = null;
        animatorOverrideController["Walk_Up"] = null;

        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }
}
