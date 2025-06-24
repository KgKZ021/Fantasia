using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] PlayerSO playerSO;

    //current stats
    private float currentHealth;
    private float currentRecovery;
    private float currentMoveSpeed;
    private float currentMight;
    private float currentProjectileSpeed;

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
        currentHealth = playerSO.MaxHealth;
        currentRecovery = playerSO.Recovery;
        currentMoveSpeed = playerSO.MoveSpeed;
        currentMight = playerSO.Might;
        currentProjectileSpeed = playerSO.ProjectileSpeed;
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
}
