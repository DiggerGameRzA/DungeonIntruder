using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickUp();
    void ShowUI(bool show);
    IGunStats GetIGunStats();
}
