using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AudioClipRefSO",menuName ="ScriptableObjects/AudioClipRef")]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] footStep;
    public AudioClip[] success;
    public AudioClip[] coin;
    public AudioClip[] dagger;
    public AudioClip[] shield;
    public AudioClip[] pickUp;
    public AudioClip[] monsterBite;
    public AudioClip[] killed;
}