using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell1", menuName = "ScriptableObjects/Spells", order = 1)]
public class SpellInfo : ScriptableObject
{
    public string name = "Unnamed Spell";
    public List<SpellInput> listOfSpellInputs;
    public SpellType spellType;
    public float actionCost = 20f;
    public float actionValue = 10f;
    public float actionDistance = 5f;

    public void CastSpell(Player player)
    {
        if (player.GetStats().currentMana - actionCost < 0)
            return;
        
        player.CostMana(actionCost);
        switch (spellType)
        {
            case SpellType.Heal:
                Debug.Log("Heal");
                player.TakeHeal(actionValue);
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
