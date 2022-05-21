using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    InputManager inputManager;
    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, inputManager.GetMousePosition(transform));
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
