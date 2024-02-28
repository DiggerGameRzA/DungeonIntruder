using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AugmentTab : MonoBehaviour
{
    [SerializeField] private RewardInfo rewardInfo;
    [SerializeField] private Text textDes;
    [SerializeField] private RewardObject obj;

    public void RefreshUI(RewardInfo rewardInfo, RewardObject obj)
    {
        this.rewardInfo = rewardInfo;
        this.obj = obj;

        switch (rewardInfo.AugmentType)
        {
            case AugmentType.MaxHp:
                textDes.text = $"Increase Max HP by {rewardInfo.Value}";
                break;
            case AugmentType.MoveSpeed:
                textDes.text = $"Increase Movement Speed by {rewardInfo.Value}%";
                break;
            case AugmentType.MaxMana:
                textDes.text = $"Increase Max Mana by {rewardInfo.Value}";
                break;
            case AugmentType.Shield:
                textDes.text = $"Obtain Shield by {rewardInfo.Value}";
                break;
            case AugmentType.Gold:
                textDes.text = $"Obtain {rewardInfo.Value} Gold";
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
    }

    public void OnChooseAugment()
    {
        AugmentInventory.Instance.AddAugment(rewardInfo);
        FindObjectOfType<UIManager>().DisableGetReward();
        obj.OnChoseReward();
    }
}
