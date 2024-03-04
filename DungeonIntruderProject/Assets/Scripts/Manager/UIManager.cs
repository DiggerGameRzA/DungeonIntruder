using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text ammoText;
    public Text gunText;
    public Text slotIndex;
    public Text hpText;
    public Text manaText;
    Player player;

    [SerializeField] public SpellUICtrl spellUICtrl;
    
    [SerializeField] private GameObject groupReward;
    [SerializeField] private Transform contentReward;
    [SerializeField] private AugmentTab prefabAugTab;
    [SerializeField] private List<AugmentTab> listOfAugTabs = new List<AugmentTab>();

    [Header("Gun Replace")] 
    [SerializeField] private GameObject groupReplace;
    [SerializeField] private Image imgGun1;
    [SerializeField] private Image imgGun2;
    [SerializeField] private Text textGun1;
    [SerializeField] private Text textGun2;
    [SerializeField] private Image imgNewGun;
    [SerializeField] private Text textNewGun;

    void Start()
    {
        player = FindObjectOfType<Player>();
        Invoke(nameof(UpdateAll), 0.2f);
        // ammoText.text = FindObjectOfType<Player>().GetComponent<Stats>().currentAmmo.ToString();
    }

    void Update()
    {
        slotIndex.text = "slot " + GunInventory.Instance.currentSlot;
    }

    public void RefreshGetReward(List<RewardInfo> rewardInfos, RewardObject obj)
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
            tab.RefreshUI(rewardInfo, obj);
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
    public void PickUpText(Item item)
    {
        gunText.text = "You got " + item.GetGunStats().ModifierInfo.name + " " + item.Name;
    }

    public void UpdateHP()
    {
        hpText.text = $"HP: {player.GetStats().currentHP}/{player.GetTrueMaxHP()}";
    }
    public void UpdateMana()
    {
        manaText.text = $"Mana: {player.GetStats().currentMana}/{player.GetTrueMaxMana()}";
    }

    private GunStats replaceGun = null;
    public void ShowReplaceGun(GunStats newGun)
    {
        replaceGun = newGun;
        GunInventory gunInv = GunInventory.Instance;
        
        groupReplace.SetActive(true);

        imgGun1.sprite = gunInv.gSlots[0].Sprite.sprite;
        imgGun1.color = gunInv.gSlots[0].Sprite.color;
        
        imgGun2.sprite = gunInv.gSlots[1].Sprite.sprite;
        imgGun2.color = gunInv.gSlots[1].Sprite.color;
        
        textGun1.text = gunInv.gSlots[0].GunName;
        textGun2.text = gunInv.gSlots[1].GunName;

        imgNewGun.sprite = newGun.Sprite.sprite;
        imgNewGun.color = newGun.Sprite.color;

        textNewGun.text = newGun.GunName;
    }

    public void OnClick_ReplaceGun(int slot)
    {
        if (replaceGun!= null)
        {
            GunInventory.Instance.AddGunToSlot(replaceGun, slot);
        }
        replaceGun = null;
        groupReplace.SetActive(false);
    }
}
