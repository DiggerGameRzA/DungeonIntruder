using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        var position = transform.position;
        if (Camera.main != null) position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = new Vector3(position.x, position.y, 0);
        transform.position = position;
    }
}
