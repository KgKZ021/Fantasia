using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PassiveItemsSO", menuName = "ScriptableObjects/PassiveItems")]
public class PassiveItemsSO : ScriptableObject
{
    [SerializeField] private float multiplier;
    public float Mulltiplier { get => multiplier; set => multiplier = value; }

    [SerializeField] private int level;
    public int Level { get => level; private set => level = value; }

    [SerializeField] private GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField] new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField] string description;
    public string Description { get => description; private set => description = value; }

    [SerializeField] Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

}
