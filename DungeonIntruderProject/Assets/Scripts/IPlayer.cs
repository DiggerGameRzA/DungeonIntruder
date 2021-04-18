using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    Rigidbody2D GetRigidbody2D();
    Stats GetStats();
    IMovement GetMovement();
}
