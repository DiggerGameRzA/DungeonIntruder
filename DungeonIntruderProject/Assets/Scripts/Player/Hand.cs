using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    InputManager inputManager;
    private Player player;
    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (player.State == PlayerState.Combat)
        {
            transform.rotation = Quaternion.Euler(0, 0, inputManager.GetMousePosition(transform));
        }
    }
    public void FlipGun(bool flip)
    {
        GameObject gun = transform.GetChild(0).gameObject;
        SpriteRenderer[] guns = gun.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].flipY = flip;
        }
    }
}
