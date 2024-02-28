using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUICtrl : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] private GameObject groupPanel;
    [SerializeField] private Transform tabContent;
    [SerializeField] private SpellInputTab tabUp;
    [SerializeField] private SpellInputTab tabDown;
    [SerializeField] private SpellInputTab tabRight;
    [SerializeField] private SpellInputTab tabLeft;
    [SerializeField] private List<SpellInputTab> listOfInputTabs = new List<SpellInputTab>();
    
    [SerializeField] private Transform spellContent;
    [SerializeField] private SpellTab spellTab;
    [SerializeField] private List<SpellTab> listOfSpellTabs = new List<SpellTab>();

    public void AddInput(SpellInput input)
    {
        SpellInputTab tab;
        switch (input)
        {
            case SpellInput.Up:
                tab = Instantiate(tabUp, tabContent);
                break;
            case SpellInput.Down:
                tab = Instantiate(tabDown, tabContent);
                break;
            case SpellInput.Left:
                tab = Instantiate(tabLeft, tabContent);
                break;
            case SpellInput.Right:
                tab = Instantiate(tabRight, tabContent);
                break;
            default:
                tab = Instantiate(tabUp, tabContent);
                break;
        }
        
        listOfInputTabs.Add(tab);
        tab.gameObject.SetActive(true);
    }
    
    public void EnableUI(bool isEnable)
    {
        groupPanel.SetActive(isEnable);
        if (!isEnable)
        {
            foreach (var tab in listOfInputTabs)
            {
                Destroy(tab.gameObject);
            }
            listOfInputTabs.Clear();
        }
        else
        {
            RefreshUI();
        }
    }
    
    public void RefreshUI()
    {
        foreach (var spell in listOfSpellTabs)
        {
            Destroy(spell.gameObject);
        }
        listOfSpellTabs.Clear();
        
        spellTab.gameObject.SetActive(false);
        foreach (var spellInfo in SpellManager.Instance.listOfSpellInfos)
        {
            SpellTab tab = Instantiate(spellTab, spellContent);
            tab.gameObject.SetActive(true);
            listOfSpellTabs.Add(tab);
            tab.RefreshUI(spellInfo);
        }
    }
}
