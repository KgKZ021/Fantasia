using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {  get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        //// Singleton pattern to avoid duplicates
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject); // Persist between scenes
        //}
        //else
        //{
        //    Destroy(gameObject); // Kill duplicate on new scene
        //}
        Instance = this;
    }

    private void Start()
    {

        DaggerController.OnDaggerAttack += DaggerController_OnDaggerAttack;
        ShieldController.OnShieldAttack += ShieldController_OnShieldAttack;
        ExpOrbs.OnExpOrbPickUp += ExpOrbs_OnExpOrbPickUp;
        HealthPotion.OnHealthPotionPickUp += HealthPotion_OnHealthPotionPickUp;
        CoinPickUp.OnCoinPickUp += CoinPickUp_OnCoinPickUp;
        GameManager.Instance.OnGameOver += Instance_OnGameOver;       
        MonsterStats.OnMonsterKilledSound += MonsterStats_OnMonsterKilledSound;
        MonsterStats.OnMonsterBite += MonsterStats_OnMonsterBite;

        
    }

    private void CoinPickUp_OnCoinPickUp(object sender, System.EventArgs e)
    {
        CoinPickUp coinPickUp = (CoinPickUp)sender;
        PlaySound(audioClipRefsSO.coin, coinPickUp.transform.position);
    }

    private void MonsterStats_OnMonsterBite(object sender, System.EventArgs e)
    {
        MonsterStats monsterStats = (MonsterStats)sender;
        PlaySound(audioClipRefsSO.monsterBite, monsterStats.transform.position);
        
    }

    private void MonsterStats_OnMonsterKilledSound(object sender, System.EventArgs e)
    {
        MonsterStats monsterStats = (MonsterStats)sender;
        PlaySound(audioClipRefsSO.killed, monsterStats.transform.position);
    }

    private void Instance_OnGameOver(object sender, System.EventArgs e)
    {
        GameManager gameManager = GameManager.Instance;
        PlaySound(audioClipRefsSO.success,gameManager.transform.position);
    }

    private void HealthPotion_OnHealthPotionPickUp(object sender, System.EventArgs e)
    {
        HealthPotion healthPotion = (HealthPotion)sender;
        PlaySound(audioClipRefsSO.pickUp, healthPotion.transform.position);
    }

    private void ExpOrbs_OnExpOrbPickUp(object sender, System.EventArgs e)
    {
        
        ExpOrbs expOrbs =(ExpOrbs)sender;
        PlaySound(audioClipRefsSO.pickUp, expOrbs.transform.position);
    }

    private void ShieldController_OnShieldAttack(object sender, System.EventArgs e)
    {
        ShieldController shieldController = (ShieldController)sender;
        PlaySound(audioClipRefsSO.shield, shieldController.transform.position);
    }

    private void DaggerController_OnDaggerAttack(object sender, System.EventArgs e)
    {
        DaggerController daggerController = (DaggerController)sender;
        PlaySound(audioClipRefsSO.dagger,daggerController.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);

    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);

    }

    public void PlayFootStepSounds(Vector3 position, float volume) //or we can just normally take with serailize field in PlayerSound.cs for audioClipArray
    {
        PlaySound(audioClipRefsSO.footStep, position, volume);
    }
}
