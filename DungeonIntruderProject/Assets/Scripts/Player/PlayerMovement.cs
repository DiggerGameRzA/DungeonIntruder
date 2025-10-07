using System.Collections;
using System.Collections.Generic;
using Mirror;
using Player;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerObject player;
        private Stats stats;
        private Rigidbody rb;
        [SerializeField] private bool canMove;
        [SerializeField] private bool canRotate;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material redMaterial;
        void Start()
        {
            player = GetComponent<PlayerObject>();
            if (player == null)
            {
                return;
            }

            rb = player.GetRigidBody();
            stats = player.GetStats();
        }
        void Update()
        {
            if (player.isLocalPlayer)
            {
                meshRenderer.material = redMaterial;
            }
        }
        void FixedUpdate()
        {
            if (player.isLocalPlayer)
            {
                if (canMove)
                {
                    Run();
                }

                if (canRotate)
                {
                    LookAtMouse();
                }
            }
            // if (InputManager.Instance.canMove)
            // {
            // if (movement == null)
            // {
            //     movement = gameObject.AddComponent<PlayerMovement>();
            //     movement.SetInfos(this);
            // }

            // data.direction.Normalize();
            // rb.velocity = data.direction.normalized * GetTrueMoveSpeed();
            // }

            // if (isLoaded)
            //     hand.mouseRotZ = InputManager.Instance.GetMousePosition(transform, data.mousePos);
        }
        private void Run()
        {
            rb.AddForce(GetMovementDir().normalized * player.GetTrueMoveSpeed(), ForceMode.Force);
        }
        private void LookAtMouse()
        {
            Quaternion mouseRot = Quaternion.Euler(transform.rotation.x, GetMouseDir(), transform.rotation.z);
            if (transform.rotation == mouseRot)
            {
                return;
            }
            rb.MoveRotation(mouseRot);
        }

        public void StopRun()
        {
            rb.velocity = Vector2.zero;
        }
        public IEnumerator Evade()
        {
            if (GetMovementDir().normalized != Vector3.zero)
            {
                player.SwitchState(PlayerState.Evading);
                InputManager.Instance.tempEvadeTime = stats.evadeCooldown;

                Vector3 startPos = transform.position;

                while (stats.evadeDistance > Vector3.Distance(startPos, transform.position))
                {
                    rb.velocity = GetMovementDir().normalized * stats.evadeSpeed;
                    yield return null;
                }
            }
        }
        public Vector3 GetMovementDir()
        {
            Vector3 right = Vector3.right;
            Vector3 forward = Vector3.forward;

            float _hor = Input.GetAxis("Horizontal");
            float _ver = Input.GetAxis("Vertical");

            Vector3 dir = right * _hor + forward * _ver;
            return dir;
        }
        public float GetMouseDir()
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 halfScreen = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
            Vector2 mouseCenter = new Vector2(mousePos.x - halfScreen.x, mousePos.y - halfScreen.y);
            Vector3 mouseNormalize = new Vector3(mouseCenter.x, 0, mouseCenter.y).normalized;

            float mouseRotZ = Mathf.Atan2(mouseNormalize.x, mouseNormalize.z) * Mathf.Rad2Deg;
            return mouseRotZ;
        }
    }
}