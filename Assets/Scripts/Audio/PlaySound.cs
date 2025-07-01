using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private Player player;
    private float footStepTimer;
    private float footStepTimerMax = 0.4f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer < 0)
        {
            footStepTimer = footStepTimerMax;

            if (player.IsRunning())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootStepSounds(player.transform.position, volume);
            }

        }
    }
}
