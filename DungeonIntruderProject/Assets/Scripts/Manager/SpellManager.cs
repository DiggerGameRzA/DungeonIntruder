using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : Singleton<SpellManager>
{
    private Player player;
    [SerializeField] private List<SpellInfo> listOfSpellInfos = new List<SpellInfo>();
    [SerializeField] private List<SpellInput> listOfSpellInputs = new List<SpellInput>();

    [Header("UIs")]
    [SerializeField] private GameObject groupPanel;
    [SerializeField] private Transform tabContent;
    [SerializeField] private SpellInputTab tabUp;
    [SerializeField] private SpellInputTab tabDown;
    [SerializeField] private SpellInputTab tabRight;
    [SerializeField] private SpellInputTab tabLeft;
    [SerializeField] private List<SpellInputTab> listOfTabs = new List<SpellInputTab>();
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player != null)
        {
            if (player.State == PlayerState.Casting && listOfSpellInputs.Count < 7)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    listOfSpellInputs.Add(SpellInput.Up);
                    
                    SpellInputTab tab = Instantiate(tabUp, tabContent);
                    listOfTabs.Add(tab);
                    tab.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    listOfSpellInputs.Add(SpellInput.Down);
                    
                    SpellInputTab tab = Instantiate(tabDown, tabContent);
                    listOfTabs.Add(tab);
                    tab.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    listOfSpellInputs.Add(SpellInput.Right);
                    
                    SpellInputTab tab = Instantiate(tabRight, tabContent);
                    listOfTabs.Add(tab);
                    tab.gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    listOfSpellInputs.Add(SpellInput.Left);
                    
                    SpellInputTab tab = Instantiate(tabLeft, tabContent);
                    listOfTabs.Add(tab);
                    tab.gameObject.SetActive(true);
                }
            }
        }
    }

    public void EnableUI(bool isEnable)
    {
        groupPanel.SetActive(isEnable);
        if (!isEnable)
        {
            foreach (var tab in listOfTabs)
            {
                Destroy(tab.gameObject);
            }
            listOfTabs.Clear();
        }
    }

    public void ClearInputList()
    {
        listOfSpellInputs.Clear();
    }

    public void ConfirmCastSpell()
    {
        SpellInfo spell = FindSpellByInputs();
        if (spell == null)
        {
            Debug.Log("Spell not found");
            return;
        }
        Debug.Log("Casting spell");
        
    }

    public SpellInfo FindSpellByInputs()
    {
        foreach (var sInfo in listOfSpellInfos)
        {
            if (sInfo.listOfSpellInputs.Count != listOfSpellInputs.Count)
                continue;
            
            for (int i = 0; i < listOfSpellInputs.Count; i++)
            {
                if (sInfo.listOfSpellInputs[i] != listOfSpellInputs[i])
                {
                    break;
                }

                if (i == listOfSpellInputs.Count - 1)
                {
                    return sInfo;
                }
            }
        }

        return null;
    }
}
