using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    CharacterController GetController();
    Transform GetTransform();
    Stats GetStats();
    PlayerMovement GetMovement();
    void FlipPlayerSprite(bool flip);
    bool isEvade { get; set; }
}
