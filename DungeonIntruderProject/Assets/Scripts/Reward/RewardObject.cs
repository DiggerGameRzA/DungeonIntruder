using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardObject : MonoBehaviour
{
    [SerializeField] private GameObject interactUI;

    [SerializeField] [NonReorderable] private List<RewardInfo> listOfRewardInfos = new List<RewardInfo>();

    private void Start()
    {
        interactUI.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactUI.SetActive(false);
        }
    }

    public void EnableRewardsUI()
    {
        RewardInfo[] randRewardInfos = RewardManager.Instance.GetCommonAugment();

        listOfRewardInfos = randRewardInfos.ToList();
        FindObjectOfType<UIManager>().RefreshGetReward(listOfRewardInfos);
        
        // gameObject.SetActive(false);
    }
}
