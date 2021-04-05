using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementController : MonoBehaviour, IDamageable
{
    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    [Header("PlayerModel Animator")]
    [SerializeField] private Animator animator;

    [Header("Player parameters")]
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float shotDamage;

    private PlayerControls playerControls;
    private CharacterController controller;
    private Vector3 deltaInput;
    private Vector3 startingRotation;
    private Vector2 movement2D;

    void Awake()
    {
        playerControls = new PlayerControls();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        movement2D = Vector2.zero;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        DoRotation();
        MovePlayer();
        Fire();
    }

    private void LateUpdate()
    {
        SetAnimatorParams();
    }

    private void DoRotation()
    {
        //rotate player
        Quaternion desiredRotation = new Quaternion(0f, cameraTransform.rotation.y, 0f, transform.rotation.w);
        transform.rotation = desiredRotation;

        
        
    }

    private void MovePlayer()
    {
        //move character according to camera direction
        movement2D = GetPlayerMovement();
        Vector3 movement3D = new Vector3(movement2D.x, 0f, movement2D.y);
        movement3D = cameraTransform.forward * movement3D.z +cameraTransform.right * movement3D.x;
        movement3D.y = 0f;
        controller.Move(movement3D * playerSpeed * Time.deltaTime);
    }

    private void Fire()
    {
        if( PlayerFired() )
        {
        RaycastHit hit;
        Vector3 direction = cameraTransform.forward;
        if(Physics.Raycast(transform.position, direction, out hit))
        {
            IDamageable objectToDamage = hit.collider.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
            if(objectToDamage != null)
            {
                objectToDamage.TakeDamage(shotDamage);
                Debug.Log("Hit!");
            }
        }
        }
    }

    public void Kill()
    {

    }

    public void TakeDamage(float damage)
    {
        
    }

    private void SetAnimatorParams()
    {
        animator.SetFloat("DirectionX", movement2D.x);
        animator.SetFloat("DirectionY", movement2D.y);
    }
    public Vector2 GetPlayerMovement()
    {
        return playerControls.Default.Move.ReadValue<Vector2>();
    }

    public Vector2 GetRotationDelta()
    {
        return playerControls.Default.Look.ReadValue<Vector2>();
    }

    public bool PlayerFired()
    {
        return playerControls.Default.Fire.triggered;
    }
}
