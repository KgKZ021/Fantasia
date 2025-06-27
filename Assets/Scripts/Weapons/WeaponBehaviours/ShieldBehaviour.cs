using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MeleeWeaponBehaviour
{
    private List<GameObject> markedMonsters;
    protected override void Start()
    {
        base.Start();

        markedMonsters = new List<GameObject>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") && !markedMonsters.Contains(other.gameObject))
        {
            MonsterStats monster = other.GetComponent<MonsterStats>();
            monster.TakeDamage(GetCurrentDamage());

            markedMonsters.Add(other.gameObject);
        }
        else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out BreakableProps breakableProps) && !markedMonsters.Contains(other.gameObject))
            {
                breakableProps.TakeDamage(GetCurrentDamage());

                markedMonsters.Add(other.gameObject);
            }
        }
    }
}
