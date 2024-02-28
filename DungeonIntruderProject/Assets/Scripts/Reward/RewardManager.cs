using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager>
{
    [SerializeField] [NonReorderable] private List<RewardInfo> augmentPool = new List<RewardInfo>();
    [SerializeField] [NonReorderable] private List<RewardInfo> gunPool = new List<RewardInfo>();

    private RewardInfo GetRandomAugment()
    {
        int randInt = Random.Range(0, augmentPool.Count);
        
        return augmentPool[randInt];
    }
    private RewardInfo GetRandomGun()
    {
        int randInt = Random.Range(0, gunPool.Count);
        
        return gunPool[randInt];
    }
    
    public RewardInfo[] GetCommonAugment()
    {
        List<RewardInfo> rewardOutput = new List<RewardInfo>();

        for (int i = 0; i < 3; i++)
        {
            RewardInfo randInfo = GetRandomAugment();

            while (rewardOutput.Contains(randInfo))
            {
                if (augmentPool.IndexOf(randInfo) + 2 > augmentPool.Count)
                {
                    randInfo = augmentPool[0];
                }
                else
                {
                    randInfo = augmentPool[augmentPool.IndexOf(randInfo) + 1];
                }
            }

            rewardOutput.Add(randInfo);
        }

        return rewardOutput.ToArray();
    }
    
    public RewardInfo[] GetGuns()
    {
        List<RewardInfo> rewardOutput = new List<RewardInfo>();

        for (int i = 0; i < 3; i++)
        {
            RewardInfo randInfo = GetRandomGun();

            while (rewardOutput.Contains(randInfo))
            {
                if (gunPool.IndexOf(randInfo) + 2 > gunPool.Count)
                {
                    randInfo = gunPool[0];
                }
                else
                {
                    randInfo = gunPool[gunPool.IndexOf(randInfo) + 1];
                }
            }

            rewardOutput.Add(randInfo);
        }

        return rewardOutput.ToArray();
    }
}
