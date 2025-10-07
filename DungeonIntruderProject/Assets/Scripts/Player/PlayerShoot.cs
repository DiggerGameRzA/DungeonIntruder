using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        private PlayerObject player;
        // Start is called before the first frame update
        void Start()
        {
            player = GetComponent<PlayerObject>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire") && player.isLocalPlayer)
            {
                player.Fire();
            }
        }
    }
}