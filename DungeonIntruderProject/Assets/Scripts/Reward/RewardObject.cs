using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardObject : MonoBehaviour
{
    [SerializeField] private GameObject interactUI;
    [SerializeField] private bool isGun;
    [SerializeField] private bool isRestart;

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
        RewardInfo[] randRewardInfos;
        if (isGun)
        {
            randRewardInfos = RewardManager.Instance.GetGuns();
        }
        else
        {
            randRewardInfos = RewardManager.Instance.GetCommonAugment();
        }

        listOfRewardInfos = randRewardInfos.ToList();
        FindObjectOfType<UIManager>().RefreshGetReward(listOfRewardInfos, this);
        
        // gameObject.SetActive(false);
    }

    public void OnChoseReward()
    {
        if (isRestart)
            Invoke(nameof(RestartScene), 0.5f);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
