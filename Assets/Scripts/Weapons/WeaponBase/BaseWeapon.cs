using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] private WeaponsSO weaponsSO;
    [SerializeField] protected GameInput gameInput;

    private float weaponTimer;
    //private bool isHeldByPlayer;
    private Player player;

    protected virtual void Start()
    {
        weaponTimer = weaponsSO.Duration;
    }
    protected virtual void Update()
    {

        //if (isHeldByPlayer)
        //{
        //    weaponTimer -= Time.deltaTime;
        //    if (weaponTimer <= 0)
        //    {
        //        Debug.Log($"[TIMER EXPIRED] Destroying weapon: {gameObject.name}");

        //        DestorySelf();


        //        isHeldByPlayer = false;
        //    }
        //}

        weaponTimer -= Time.deltaTime;
        if (weaponTimer <= 0)
        {
            Attack();
        }

    }

    protected virtual void Attack()
    {
        weaponTimer = weaponsSO.Duration;
    }
    public virtual void Collected(Player player)
    {
        Debug.LogError("BaseCounter.Intercat();");
    }

    public WeaponsSO GetWeaponSO()
    {
        return weaponsSO;
    }

    public void SetWeaponParent(Player player)
    {
        if (this.player != null) //old parent
        {
            this.player.ClearWeapon();
        }

        //new parent
        this.player = player;

        if (player.HasPickedWeapon())
        {
            Debug.LogError("Player Already has a Weapon");
            return;
        }

        player.SetPickededWeapon(this);

        transform.parent = player.GetWeaponFollowTransform();
        transform.localPosition = Vector3.zero;

        //isHeldByPlayer = true;
        weaponTimer = weaponsSO.Duration;
        Debug.Log($"[SetWeaponParent] {gameObject.name} is now held by player. Timer = {weaponTimer}");

    }

    //public IKitchenObjectParent GetKitchenObjParent()
    //{
    //    return kitchenObjParent;
    //}

    public void DestorySelf()
    {
        Player.Instance.ClearWeapon();
        Destroy(gameObject);
    }


    //public bool TryGetPlate(out PlateKitchenObj plateKitchenObj)
    //{
    //    if (this is PlateKitchenObj)
    //    {
    //        plateKitchenObj = this as PlateKitchenObj;
    //        return true;
    //    }
    //    else
    //    {
    //        plateKitchenObj = null; //need to set out parameter before exiting function
    //        return false;
    //    }
    //}

}
