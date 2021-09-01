using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace Player {
    public class WorldPlayer : NetworkBehaviour {
        public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings {
            WritePermission = NetworkVariablePermission.ServerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        });

        public override void NetworkStart()
        {
            Move();
        }

        public void Move() {
            // print("move");
            if (NetworkManager.Singleton.IsServer) {
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
                Position.Value = randomPosition;
                Debug.Log(Position.Value);
            }
            else {
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default) {
            Position.Value = GetRandomPositionOnPlane();
            Debug.Log(Position.Value);
        }

        static Vector3 GetRandomPositionOnPlane() {
            // print("random pos");
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        void Update() {
            transform.position = Position.Value;
        }
    }
}