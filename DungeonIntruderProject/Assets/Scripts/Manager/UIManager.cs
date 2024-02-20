using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ammoText;
    public Text gunText;
    public Text slotIndex;
    public Text hpText;
    public Text manaText;
    Player player;

    [SerializeField] private GameObject groupReward;
    [SerializeField] private Transform contentReward;
    [SerializeField] private AugmentTab prefabAugTab;
    [SerializeField] private List<AugmentTab> listOfAugTabs = new List<AugmentTab>();

    void Start()
    {
        player = FindObjectOfType<Player>();
        // ammoText.text = FindObjectOfType<Player>().GetComponent<Stats>().currentAmmo.ToString();
    }

    void Update()
    {
        slotIndex.text = "slot " + GunInventory.Instance.currentSlot;
    }

    public void RefreshGetReward(List<RewardInfo> rewardInfos)
    {
        groupReward.SetActive(true);

        foreach (var tab in listOfAugTabs)
        {
            Destroy(tab.gameObject);
        }
        listOfAugTabs.Clear();

        prefabAugTab.gameObject.SetActive(false);
        foreach (var rewardInfo in rewardInfos)
        {
            AugmentTab tab = Instantiate(prefabAugTab, contentReward);
            tab.RefreshUI(rewardInfo);
            listOfAugTabs.Add(tab);
            tab.gameObject.SetActive(true);
        }
    }

    public void DisableGetReward()
    {
        groupReward.SetActive(false);
    }

    public void UpdateAll()
    {
        UpdateAmmo();
        UpdateMana();
        UpdateHP();
    }
    public void UpdateAmmo()
    {
        // ammoText.text = player.GetStats().currentAmmo.ToString();
    }
    public void PickUpText(IInventoryItem item)
    {
        gunText.text = "You got " + item.GetIGunStats().ModifierInfo.name + " " + item.Name;
    }

    public void UpdateHP()
    {
        hpText.text = $"HP: {player.GetStats().currentHP}/{player.GetTrueMaxHP()}";
    }
    public void UpdateMana()
    {
        manaText.text = $"HP: {player.GetStats().currentMana}/{player.GetTrueMaxMana()}";
    }
}
