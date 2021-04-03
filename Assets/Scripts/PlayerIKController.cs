using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIKController : MonoBehaviour
{
    Animator animator;
    //[SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;
    [SerializeField] private Transform cameraTransform;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        //animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);

        //animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);

        Quaternion desiredRotation = new Quaternion(cameraTransform.rotation.x, 0f, 0f, cameraTransform.rotation.w);
        animator.SetBoneLocalRotation(HumanBodyBones.Spine, desiredRotation);
    }
}
