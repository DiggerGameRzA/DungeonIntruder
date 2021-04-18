using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    IPlayer player;
    public static bool evade = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<IPlayer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Evade"))
        {
            if (player.GetMovement().GetMovementDir() != Vector2.zero)
            {
                evade = true;
                player.GetMovement().Evade();
                Debug.Log("Do evade!");
            }
            else
            {
                Debug.Log("Can not evade");
            }
        }
        if(evade && (player.GetMovement().GetVelocity().x <= 1f || player.GetMovement().GetVelocity().y <= 1f))
        {
            evade = false;
        }
    }
    public static float GetVerInput()
    {
        return Input.GetAxis("Vertical");
    }
    public static float GetHorInput()
    {
        return Input.GetAxis("Horizontal");
    }
}
