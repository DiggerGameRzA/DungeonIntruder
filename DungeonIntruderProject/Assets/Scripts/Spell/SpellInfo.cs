using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell1", menuName = "ScriptableObjects/Spells", order = 1)]
public class SpellInfo : ScriptableObject
{
    public List<SpellInput> listOfSpellInputs;
    public SpellType spellType;
    public float actionValue = 10f;
    public float actionDistance = 5f;

    public void CastSpell()
    {
        switch (spellType)
        {
            case SpellType.Heal:
                Debug.Log("Heal");
                break;
            case SpellType.Damage:
                Debug.Log("Damage");
                break;
            case SpellType.DamageAoE:
                Debug.Log("Damage AoE");
                break;
        }
    }
}
public enum SpellInput
{
    Up,
    Down,
    Left,
    Right
}
public enum SpellType
{
    Heal,
    Damage,
    DamageAoE,
}
