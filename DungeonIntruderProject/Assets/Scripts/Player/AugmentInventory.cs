using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AugmentInventory : MonoBehaviour
{
    [SerializeField] [NonReorderable] private List<RewardInfo> listOfAugments = new List<RewardInfo>();

    public void AddAugment(RewardInfo augmentInfo)
    {
        listOfAugments.Add(augmentInfo);
        Player player = FindObjectOfType<Player>();
        
        switch (augmentInfo.AugmentType)
        {
            case AugmentType.MaxHp:
                player.TakeHeal(augmentInfo.Value);
                break;
            case AugmentType.MoveSpeed:
                break;
            case AugmentType.MaxMana:
                player.RestoreMana(augmentInfo.Value);
                break;
            case AugmentType.Shield:
                break;
            case AugmentType.Gold:
                break;
            case AugmentType.Item:
                break;
            case AugmentType.BulletDmg:
                break;
            case AugmentType.PistolDmg:
                break;
            case AugmentType.SpellDmg:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        FindObjectOfType<UIManager>().UpdateAll();
    }

    public float GetAugmentValue(AugmentType type)
    {
        return listOfAugments.FindAll(x => x.AugmentType == type).Sum(info => info.Value);
    }
}
