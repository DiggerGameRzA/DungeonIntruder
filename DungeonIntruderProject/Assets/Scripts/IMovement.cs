using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void Run();
    void Evade();
    Vector2 GetMovementDir();
    Vector2 GetVelocity();
}
