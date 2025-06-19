using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Player Controller
/// Controls all the actions player will perform
/// </summary>
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed=7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform weaponHoldPoint;
    [SerializeField] private Rigidbody playerRigidBody;

    private bool isRunning;
    private Vector3 lastInteractDir;
    private BaseWeapon selectedWeapon;
    private BaseWeapon pickedWeapon;

    public event EventHandler<OnSelectedWeaponChangedEventArgs> OnSelectedWeaponChanged;
    public class OnSelectedWeaponChangedEventArgs : EventArgs
    {
        public BaseWeapon selectedWeapon;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player Instance!");
        }
        Instance = this;

        playerRigidBody = GetComponent<Rigidbody>();
        
    }

    
    private void Start()
    {
        gameInput.OnCollectWeapon += GameInput_OnCollectWeapon;
    }

    private void GameInput_OnCollectWeapon(object sender, EventArgs e)
    {
        if (selectedWeapon != null)
        {
            selectedWeapon.Collected(this);
        }
    }

    private void FixedUpdate()
    {
        Movements();
        //WeaponCollection();
        
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    private void Movements()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
;        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        //collision detection
        float moveDistance = moveSpeed * Time.deltaTime;
        //float playerRadius = 0.7f;
        //float playerHeight = 2f;

        
        //bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //if (canMove)
        //{
        //Vector3 targetPos = moveDir * moveDistance;
        //playerRigidBody.MovePosition(targetPos);
        //}
        //if (!canMove)
        //{
        //    //if cannot move towards moveDir,
        //    //attempt to move (X) only
        //    Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
        //    //moveDir.x=!=0 is only if we are attempting to move in x direction
        //    canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
        //    if (canMove)
        //    {
        //        moveDir = moveDirX; 
        //    }
        //    else
        //    {
        //        //if Cannot move only on the X, attempt Z
        //        Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
        //        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
        //        if (canMove)
        //        {
        //            moveDir = moveDirZ; 
        //        }
        //        else
        //        {


        //        }
        //    }
        //}

        //if (canMove)
        //{
        //transform.position += moveDir * moveDistance;
        //}

        bool isMoving = moveDir != Vector3.zero;

        if (isMoving)
        {
            playerRigidBody.velocity = moveDir * moveSpeed;
        //    float rotateSpeed = 7f;
        //    transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
        else
        {
            playerRigidBody.velocity = Vector3.zero;
        }

        isRunning = isMoving;

        float rotateSpeed = 7f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // interpolate between two vectors and with a flaot value

    }

    private void WeaponCollection()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 5f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, layerMask))
        {

            if (raycastHit.transform.TryGetComponent(out BaseWeapon baseWeapon))
            {
                
                if (baseWeapon != selectedWeapon)
                {
                    selectedWeapon = baseWeapon;

                    SetSelectedWeapon(selectedWeapon);
                }

            }
            else
            {
                SetSelectedWeapon(null);
            }


        }
        else
        {
            SetSelectedWeapon(null);
        }
    }

    public void SetSelectedWeapon(BaseWeapon selectedWeapon)
    {
        this.selectedWeapon = selectedWeapon;

        OnSelectedWeaponChanged?.Invoke(this, new OnSelectedWeaponChangedEventArgs
        {
            selectedWeapon = selectedWeapon
        });
    }

    public void SetPickededWeapon(BaseWeapon pickedWeapon)
    {
        this.pickedWeapon = pickedWeapon;

    }
    public Transform GetWeaponFollowTransform()
    {
        return weaponHoldPoint;
    }

    public BaseWeapon GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public BaseWeapon GetPickedWeapon()
    {
        return pickedWeapon;
    }

    public void ClearWeapon()
    {
        selectedWeapon = null;
    }

    public bool HasPickedWeapon()
    {
        return pickedWeapon != null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
    }


}


