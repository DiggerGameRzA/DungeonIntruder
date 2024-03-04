using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : Singleton<SpellManager>
{
    private Player player;
    [SerializeField] public List<SpellInfo> listOfSpellInfos = new List<SpellInfo>();
    [SerializeField] private List<SpellInput> listOfSpellInputs = new List<SpellInput>();
    
    [SerializeField] private GameObject prefabExplosion;
    void Start()
    {
        
    }

    void Update()
    {
        SpellUICtrl uiCtrl = UIManager.Instance.spellUICtrl;
        if (player != null)
        {
            if (player.State == PlayerState.Casting && listOfSpellInputs.Count < 7)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    listOfSpellInputs.Add(SpellInput.Up);
                    uiCtrl.AddInput(SpellInput.Up);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    listOfSpellInputs.Add(SpellInput.Down);
                    uiCtrl.AddInput(SpellInput.Down);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    listOfSpellInputs.Add(SpellInput.Right);
                    uiCtrl.AddInput(SpellInput.Right);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    listOfSpellInputs.Add(SpellInput.Left);
                    uiCtrl.AddInput(SpellInput.Left);
                }
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        spell.CastSpell(player);
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
    
    public void InitExplosion(Player player, SpellInfo spellInfo)
    {
        GameObject go = Instantiate(prefabExplosion, player.transform.position, Quaternion.identity);
        go.GetComponent<Explosion>().damage = spellInfo.actionValue;
        go.GetComponent<Explosion>().radius = spellInfo.actionDistance;
        Destroy(go, 1f);
    }
}
