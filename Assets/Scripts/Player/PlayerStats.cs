using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerSO playerSO;

    //current stats
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentRecovery;
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentMight;
    [HideInInspector] public float currentProjectileSpeed;
    [HideInInspector] public float currentMagnet;

    //Spawned Weapons
    public List<GameObject> spawnedWeapons;

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

    private void Awake()
    {
        playerSO = CharacterSelector.GetData();
        CharacterSelector.Instance.DestorySingleton();

        currentHealth = playerSO.MaxHealth;
        currentRecovery = playerSO.Recovery;
        currentMoveSpeed = playerSO.MoveSpeed;
        currentMight = playerSO.Might;
        currentProjectileSpeed = playerSO.ProjectileSpeed;
        currentMagnet = playerSO.Magnet;

        SpawnWeapon(playerSO.StartingWeapon);
    }

    private void Start()
    {
        experienceCap = levelsRanges[0].experienceCapIncrease;
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
        }
    }

    public void TakeDamage(float damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
        
    }

    public void Kill()
    {
        Debug.Log("KILLED!!!");
    }

    public void RestoreHealth(float amount)
    {
        if(currentHealth < playerSO.MaxHealth)
        {
            currentHealth += amount;

            if(currentHealth > playerSO.MaxHealth)
            {
                currentHealth = playerSO.MaxHealth;
            }
        }
        
    }

    private void Recover()
    {
        if(currentHealth < playerSO.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;

            //fail safe
            if(currentHealth > playerSO.MaxHealth)
            {
                currentHealth = playerSO.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon , transform.position,Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);
    }
}
