using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayerSO playerSO;

    //current stats
    private float currentHealth;
    private float currentRecovery;
    private float currentMoveSpeed;
    private float currentMight;
    private float currentProjectileSpeed;
    private float currentMagnet;


    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            //check if the value changed
            if (currentHealth != value)
            {
                currentHealth = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentHealthDisplay.text = "Health: " + currentHealth;
                }
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            //check if the value changed
            if (currentRecovery != value)
            {
                currentRecovery = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            //check if the value changed
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentMoveSpeedDisplay.text = "Move Speed : " + currentMoveSpeed;
                }
            }
        }
    }
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            //check if the value changed
            if (currentMight != value)
            {
                currentMight = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            //check if the value changed
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
            }
        }
    }
    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            //check if the value changed
            if (currentMagnet != value)
            {
                currentMagnet = value;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
            }
        }
    }
    #endregion


    [Header("Experience/Level")]
    [SerializeField] private int experience = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int experienceCap;

    //nested class
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;

    }

    [Header("I-Frames")] // invincibility frames
    [SerializeField] private float invincibilityDuration;
    private float invincibilityTimer;
    private bool isInvincible;

    [SerializeField] private List<LevelRange> levelsRanges;

    InventoryManager inventoryManager;
    [SerializeField] private int weaponIndex;
    [SerializeField] private int passiveItemIndex;

    [Header("UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI levelText;

   // public GameObject secondWeaponTest;
   // public GameObject firstPassiveItemTest;
    public GameObject secondPassiveItemTest;

    private void Awake()
    {
        playerSO = UICharacterSelector.GetData();
        //UICharacterSelector.Instance.DestorySingleton();

        inventoryManager = GetComponent<InventoryManager>();
        

        CurrentHealth = playerSO.MaxHealth;
        CurrentRecovery = playerSO.Recovery;
        CurrentMoveSpeed = playerSO.MoveSpeed;
        CurrentMight = playerSO.Might;
        CurrentProjectileSpeed = playerSO.ProjectileSpeed;
        CurrentMagnet = playerSO.Magnet;

        SpawnWeapon(playerSO.StartingWeapon);
        //SpawnWeapon(secondWeaponTest);

        //SpawnPassiveItems(firstPassiveItemTest);
        SpawnPassiveItems(secondPassiveItemTest);
    }

    private void Start()
    {
        experienceCap = levelsRanges[0].experienceCapIncrease;

        GameManager.Instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.Instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.Instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
        GameManager.Instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.Instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
        GameManager.Instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;

        GameManager.Instance.AssignChosenCharacterUI(playerSO);

        UpdateHealthbar();
        UpdateExpBar();
        UpdateLevelText();
    }

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if(isInvincible)
        {
            isInvincible = false;
        }

        Recover();
    }
    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();

        UpdateExpBar();
    }

    private void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelsRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;

            UpdateLevelText();

            GameManager.Instance.StartLevelUp();
        }
    }

    public void TakeDamage(float damage)
    {
        if(!isInvincible)
        {
            CurrentHealth -= damage;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            
            if (CurrentHealth <= 0)
            {
                Kill();
               
            }

            UpdateHealthbar();
        }
        
    }

    public void Kill()
    {
        
        if (!GameManager.Instance.isGameOver)
        {
            GameManager.Instance.AssignLevelReached(level);
            GameManager.Instance.AssignChosenWeaponAndPassiveItemUI(inventoryManager.weaponUISlots, inventoryManager.passiveItemUISlots);
            GameManager.Instance.GameOver();
        }
    }

    public void RestoreHealth(float amount)
    {
        if(CurrentHealth < playerSO.MaxHealth)
        {
            CurrentHealth += amount;

            if(CurrentHealth > playerSO.MaxHealth)
            {
                CurrentHealth = playerSO.MaxHealth;
            }
        }
        
    }

    private void Recover()
    {
        if(CurrentHealth < playerSO.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            //fail safe
            if(CurrentHealth > playerSO.MaxHealth)
            {
                CurrentHealth = playerSO.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if(weaponIndex >= inventoryManager.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);

        inventoryManager.AddWeapon(weaponIndex,spawnedWeapon.GetComponent<BaseWeapon>());

        weaponIndex++;
    }

    public void SpawnPassiveItems(GameObject passiveItems)
    {
        if (passiveItemIndex >= inventoryManager.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        GameObject spawnedPassiveItem = Instantiate(passiveItems, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);

        inventoryManager.AddPassiveItems(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItems>());

        passiveItemIndex++;
    }

    private void UpdateHealthbar()
    {
        healthBar.fillAmount = currentHealth / playerSO.MaxHealth;

    }

    private void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }

    private void UpdateLevelText()
    {
        levelText.text = "LV " + level.ToString();
    }

}
