using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using Utp;

namespace Player
{
    [RequireComponent(typeof(Stats))]
    public class PlayerObject : NetworkBehaviour
    {
        private RelayNetworkManager relayNetworkManager;
        public SyncList<NetworkIdentity> activePlayers = new SyncList<NetworkIdentity>();
        [SerializeField] private readonly List<NetworkIdentity> localPlayers = new List<NetworkIdentity>();
        [SerializeField] private NetworkIdentity networkIdentity;
        private Rigidbody rb;
        [SerializeField] private GameObject prefabBullet;
        [SerializeField] public Transform bulletPos;

        private Collider2D col;
        public PlayerState State;
        // private PlayerMovement movement;
        Stats stats;

        public ParticleSystem healParticle;

        private RewardObject rewardObj = null;
        private PortalObject portalObj = null;

        private bool isLoaded = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            stats = GetComponent<Stats>();
            State = PlayerState.Combat;
            relayNetworkManager = NetworkManager.singleton.GetComponent<RelayNetworkManager>();
        }

        #region Start
        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            relayNetworkManager = NetworkManager.singleton.GetComponent<RelayNetworkManager>();
            CameraCtrl cameraCtrl = FindObjectOfType<CameraCtrl>();
            if (cameraCtrl != null)
            {
                cameraCtrl.AddTargetPlayer(this.transform);
            }
        }
        public override void OnStartServer()
        {
            base.OnStartServer();
            activePlayers.Callback += OnActivePlayersChanged;
        }
        public override void OnStartClient()
        {
            rb = GetComponent<Rigidbody>();
            rb.Sleep();
            transform.position = new Vector3(-4 + netId, 0, 0);
            Debug.Log("Client started — registering SyncList callback");
            activePlayers.Callback += OnActivePlayersChanged;
        }

        public override void OnStopClient()
        {
            activePlayers.Callback -= OnActivePlayersChanged;
        }

        private void OnActivePlayersChanged(SyncList<NetworkIdentity>.Operation op, int index, NetworkIdentity oldItem, NetworkIdentity newItem)
        {
            switch (op)
            {
                case SyncList<NetworkIdentity>.Operation.OP_ADD:
                    localPlayers.Add(newItem);
                    Debug.Log($"Player added: {newItem?.name}");
                    break;

                case SyncList<NetworkIdentity>.Operation.OP_REMOVEAT:
                    localPlayers.Remove(oldItem);
                    Debug.Log($"Player removed: {oldItem?.name}");
                    break;

                case SyncList<NetworkIdentity>.Operation.OP_SET:
                    localPlayers.Remove(oldItem);
                    localPlayers.Add(newItem);
                    Debug.Log($"Player replaced: {oldItem?.name} → {newItem?.name}");
                    break;

                case SyncList<NetworkIdentity>.Operation.OP_CLEAR:
                    localPlayers.Clear();
                    Debug.Log("All players cleared");
                    break;
            }
        }

        [Server]
        public void ServerAddPlayer(NetworkIdentity player)
        {
            if (!activePlayers.Contains(player))
            {
                activePlayers.Add(player);
            }
        }
        [Server]
        public void ServerRemovePlayer(NetworkIdentity player)
        {
            if (activePlayers.Contains(player))
            {
                activePlayers.Remove(player);
            }
        }
        #endregion

        // IEnumerator WaitForGameStart()
        // {
        //     yield return new WaitUntil(() => NetworkManager.Instance.result != null);
        //     isLoaded = true;
        // }

        // [Rpc(RpcSources.All, RpcTargets.All)]
        // public void RPC_SendPosition(Vector2 pos, RpcInfo info = default)
        // {
        //     if (!info.IsInvokeLocal)
        //     {
        //         rb.position = pos;
        //     }
        // }
        // [Rpc(RpcSources.All, RpcTargets.All)]
        // public void RPC_SendMouseRot(Vector2 mousePos, RpcInfo info = default)
        // {
        //     hand.mouseRotZ = InputManager.Instance.GetMousePosition(transform, mousePos);
        // }

        private void Update()
        {
            // if (networkObject.HasInputAuthority)
            // {
            //     RPC_SendPosition(transform.position);
            // }

            // switch (State)
            // {
            //     case PlayerState.Combat:
            //         if (UIManager.Instance.spellUICtrl != null)
            //             UIManager.Instance.spellUICtrl.EnableUI(false);
            //         InputManager.Instance.SetCanMove(true);
            //         InputManager.Instance.SetCanEvade(true);
            //         InputManager.Instance.ObtainReward(rewardObj);
            //         InputManager.Instance.EnterPortal(portalObj);
            //         break;
            //     case PlayerState.Casting:
            //         if (UIManager.Instance.spellUICtrl != null)
            //             UIManager.Instance.spellUICtrl.EnableUI(true);
            //         InputManager.Instance.SetCanMove(false);
            //         InputManager.Instance.SetCanEvade(false);
            //         movement.StopRun();
            //         break;
            //     case PlayerState.StandBy:
            //         break;
            //     case PlayerState.Evading:
            //         InputManager.Instance.SetCanMove(false);
            //         InputManager.Instance.SetCanEvade(false);
            //         StartCoroutine(OnIFraming(0f, 0.4f));
            //         SwitchState(PlayerState.Combat, 0.2f);
            //         break;
            //     default:
            //         break;
            // }
        }

        public void Fire()
        {
            // Debug.Log(this.gameObject.name);
            // Debug.Log("-----");
            // Debug.Log($"is server: {isServer}");
            // Debug.Log($"is client: {isClient}");
            // Debug.Log($"is local player: {isLocalPlayer}");
            // Debug.Log($"is owned: {isOwned}");
            // Debug.Log($"auth: {authority}");
            if (isServer)
            {
                RpcFire(netId);
            }
            else if (isClient)
            {
                CmdFire(netId);
                LocalFire();
            }
        }
        #region Network Methods
        [Command]
        private void CmdFire(uint _netId)
        {
            Debug.Log("Fire from Client");
            Debug.Log($"Sender: {_netId}");

            NetworkIdentity targetIden = null;
            if (isServer)
            {
                targetIden = relayNetworkManager.GetPlayerByNetworkIden(_netId);
            }
            else if (isClient)
            {
                targetIden = GetPlayerByNetworkIden(_netId);
            }
            PlayerObject targetPlayer = targetIden.GetComponent<PlayerObject>();
            GameObject go = Instantiate(targetPlayer.prefabBullet, targetPlayer.bulletPos.position, targetPlayer.bulletPos.rotation);

            go.GetComponent<Rigidbody>().velocity = targetPlayer.bulletPos.forward * 50f;
        }
        [ClientRpc]
        private void RpcFire(uint _netId)
        {
            Debug.Log("Fire from Server");
            Debug.Log($"Sender: {_netId}");

            NetworkIdentity targetIden = null;
            if (isServer)
            {
                targetIden = relayNetworkManager.GetPlayerByNetworkIden(_netId);
            }
            else if (isClient)
            {
                targetIden = GetPlayerByNetworkIden(_netId);
            }
            PlayerObject targetPlayer = targetIden.GetComponent<PlayerObject>();
            GameObject go = Instantiate(targetPlayer.prefabBullet, targetPlayer.bulletPos.position, targetPlayer.bulletPos.rotation);

            go.GetComponent<Rigidbody>().velocity = targetPlayer.bulletPos.forward * 50f;
        }
        private void LocalFire()
        {
            GameObject go = Instantiate(prefabBullet, bulletPos.position, bulletPos.rotation);

            go.GetComponent<Rigidbody>().velocity = bulletPos.forward * 50f;
        }
        #endregion

        private NetworkIdentity GetPlayerByNetworkIden(uint _netId)
        {
            foreach (var player in activePlayers)
            {
                if (player.netId == _netId)
                {
                    return player;
                }
            }
            return null;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Item"))
            {
                // FindObjectOfType<InputManager>().CollectItem(col.GetComponent<Item>());
            }
            else if (col.CompareTag("Reward"))
            {
                rewardObj = col.GetComponent<RewardObject>();
                portalObj = col.GetComponent<PortalObject>();
            }
        }
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Item"))
            {

            }
            else if (col.CompareTag("Reward"))
            {
                rewardObj = null;
                portalObj = null;
            }
        }

        public void SwitchState(PlayerState state, float delay = 0f)
        {
            StartCoroutine(OnDelaySwitchState(state, delay));
        }

        private IEnumerator OnDelaySwitchState(PlayerState state, float delay = 0f)
        {
            yield return new WaitForSeconds(delay);
            State = state;
        }

        public Rigidbody GetRigidBody()
        {
            return rb;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public Stats GetStats()
        {
            return stats;
        }

        // public PlayerMovement GetMovement()
        // {
        //     return movement;
        // }

        public void FlipPlayerSprite(bool flip)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = flip;
        }

        public IEnumerator OnIFraming(float onBegin, float onEnding)
        {
            yield return new WaitForSeconds(onBegin);
            col.enabled = false;

            yield return new WaitForSeconds(onEnding);
            col.enabled = true;
        }

        public float GetTrueMaxHP()
        {
            if (AugmentInventory.Instance == null)
                return stats.maxHP;
            return stats.maxHP + AugmentInventory.Instance.GetAugmentValue(AugmentType.MaxHp);
        }
        public float GetTrueMoveSpeed()
        {
            if (stats == null)
                stats = GetComponent<Stats>();
            if (AugmentInventory.Instance == null)
                return stats.movementSpeed;
            return stats.movementSpeed * (1 + (AugmentInventory.Instance.GetAugmentValue(AugmentType.MoveSpeed)) / 100f);
        }
        public float GetTrueMaxMana()
        {
            if (AugmentInventory.Instance == null)
                return stats.maxMana;
            return stats.maxMana + AugmentInventory.Instance.GetAugmentValue(AugmentType.MaxMana);
        }

        public void RestoreMana(float mana)
        {
            stats.currentMana += mana;
            if (stats.currentMana >= GetTrueMaxMana())
            {
                stats.currentMana = GetTrueMaxMana();
            }
            UIManager.Instance.UpdateMana();
        }
        public void CostMana(float mana)
        {
            stats.currentMana -= mana;
            if (stats.currentMana <= 0)
            {
                stats.currentMana = 0;
            }
            UIManager.Instance.UpdateMana();
        }

        public void TakeHeal(float heal)
        {
            stats.currentHP += heal;
            if (stats.currentHP >= GetTrueMaxHP())
            {
                stats.currentHP = GetTrueMaxHP();
            }
            UIManager.Instance.UpdateHP();
        }

        public void TakeDamage(float dmg)
        {
            stats.currentHP -= dmg;
            if (stats.currentHP <= 0)
            {
                stats.currentHP = 0;
                OnDead();
            }
            UIManager.Instance.UpdateHP();
        }

        public void OnDead()
        {
            gameObject.SetActive(false);
        }
    }
    public enum PlayerState
    {
        StandBy,
        Combat,
        Evading,
        Casting
    }

}