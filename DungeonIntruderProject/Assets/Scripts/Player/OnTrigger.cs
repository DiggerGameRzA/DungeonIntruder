using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            InputManager.instance.CollectItem(col.GetComponent<IInventoryItem>());
        }
    }
}
