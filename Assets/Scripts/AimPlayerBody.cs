using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPlayerBody : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnAimatorIK()
    {
        Debug.Log("hello");
        animator.SetBoneLocalRotation(HumanBodyBones.Spine, cameraTransform.rotation);
    }
}
