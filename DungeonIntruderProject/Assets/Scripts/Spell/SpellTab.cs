using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellTab : MonoBehaviour
{
    [SerializeField] private SpellInfo spellInfo;
    [SerializeField] private Text name;
    [SerializeField] private Transform inputContent;
    [SerializeField] private SpellInputTab tabSpellUp;
    [SerializeField] private SpellInputTab tabSpellDown;
    [SerializeField] private SpellInputTab tabSpellRight;
    [SerializeField] private SpellInputTab tabSpellLeft;
    [SerializeField] private List<SpellInputTab> listOfInputTabs= new List<SpellInputTab>();

    public void RefreshUI(SpellInfo spellInfo)
    {
        this.spellInfo = spellInfo;
        name.text = spellInfo.name;

        tabSpellUp.gameObject.SetActive(false);
        tabSpellDown.gameObject.SetActive(false);
        tabSpellRight.gameObject.SetActive(false);
        tabSpellLeft.gameObject.SetActive(false);
        
        foreach (var input in spellInfo.listOfSpellInputs)
        {
            SpellInputTab tab;
            switch (input)
            {
                case SpellInput.Up:
                    tab = Instantiate(tabSpellUp, inputContent);
                    listOfInputTabs.Add(tab);
                    tab.gameObject.SetActive(true);
                    break;
                case SpellInput.Down:
                    tab = Instantiate(tabSpellDown, inputContent);
                    listOfInputTabs.Add(tab);
                    tab.gameObject.SetActive(true);
                    break;
                case SpellInput.Left:
                    tab = Instantiate(tabSpellLeft, inputContent);
                    listOfInputTabs.Add(tab);
                    tab.gameObject.SetActive(true);
                    break;
                case SpellInput.Right:
                    tab = Instantiate(tabSpellRight, inputContent);
                    listOfInputTabs.Add(tab);
                    tab.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}
