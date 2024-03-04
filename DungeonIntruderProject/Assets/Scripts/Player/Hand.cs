using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    InputManager inputManager;
    private Player player;
    private float mouseRotZ;
    private void Start()
    {
        inputManager = InputManager.Instance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        mouseRotZ = inputManager.GetMousePosition(transform);
        if (player.State == PlayerState.Combat || player.State == PlayerState.Casting)
        {
            transform.rotation = Quaternion.Euler(0, 0, mouseRotZ);
        }
        if (player.State == PlayerState.Combat || player.State == PlayerState.Casting)
        {
            if (mouseRotZ > 90 || mouseRotZ < -90)
            {
                FlipGun(true);
                player.FlipPlayerSprite(true);
            }
            else
            {
                FlipGun(false);
                player.FlipPlayerSprite(false);
            }
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
