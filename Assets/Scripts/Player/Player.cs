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

    [SerializeField] private GameObject barbarian;
    [SerializeField] private GameObject rogue;
    [SerializeField] private PlayerSO daggerCharacter;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform weaponHoldPoint;
    [SerializeField] private Rigidbody playerRigidBody;

    private bool isRunning;
    private Vector3 lastInteractDir;
    private BaseWeapon selectedWeapon;
    private BaseWeapon pickedWeapon;
    private PlayerStats playerStats;

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
        playerStats = GetComponent<PlayerStats>();
        if(playerStats.playerSO == daggerCharacter)
        {
            barbarian.SetActive(false);
            rogue.SetActive(true);
        }
        else
        {
            rogue.SetActive(false);
            barbarian.SetActive(true);
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
        if (GameManager.Instance.isGameOver)
        {
            return;
        }

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
;       Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        
        bool isMoving = moveDir != Vector3.zero;

        if (isMoving)
        {
            playerRigidBody.velocity = moveDir * playerStats.CurrentMoveSpeed;
        }
        else
        {
            playerRigidBody.velocity = Vector3.zero;
        }

        isRunning = isMoving;

        float rotateSpeed = 7f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); // interpolate between two vectors and with a flaot value

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
        //Debug.Log("Collided with: " + collision.gameObject.name);
    }


}


