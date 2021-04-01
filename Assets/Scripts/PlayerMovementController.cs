using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Camera settings")]
    [SerializeField] private Transform cameraFollowTarget;
    [SerializeField] private float verticalCameraSpeed = 20.0f;
    [SerializeField] private float horizontalCameraSpeed = 20.0f;
    [SerializeField] private float cameraClampAngle = 90.0f;

    private PlayerControls playerControls;
    private Vector3 deltaInput;
    private Vector3 startingRotation;

    void Awake()
    {
        playerControls = new PlayerControls();
        startingRotation = cameraFollowTarget.localRotation.eulerAngles;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        DoRotation();
    }

    private void DoRotation()
    {
        deltaInput = GetRotationDelta();
        startingRotation.x += deltaInput.x * verticalCameraSpeed * Time.deltaTime;
        startingRotation.y += deltaInput.y * horizontalCameraSpeed * Time.deltaTime;
        startingRotation.y = Mathf.Clamp(startingRotation.y, -cameraClampAngle, cameraClampAngle);
        cameraFollowTarget.rotation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Default.Move.ReadValue<Vector2>();
    }

    public Vector2 GetRotationDelta()
    {
        return playerControls.Default.Look.ReadValue<Vector2>();
    }
}
