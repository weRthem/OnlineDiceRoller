using UnityEngine;
using TMPro;
using Photon.Pun;

namespace DiceRoller.Menus
{
    public class MainMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject loginPanel = null;
        [SerializeField] private GameObject findOpponentsPanel = null;
        [SerializeField] private GameObject waitingStatusPanel = null;
        [SerializeField] private TextMeshProUGUI waitingStatusText;

        private bool isConnecting = false;

        private const string GameVersion = "0.1";
        private const int maxPlayersPerRoom = 2;

        private void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void FindOpponent(){
            isConnecting = true;

            findOpponentsPanel.SetActive(false);
            loginPanel.SetActive(false);
            waitingStatusPanel.SetActive(true);

            waitingStatusText.text = "Searching...";

            if(PhotonNetwork.IsConnected){
                PhotonNetwork.JoinRandomRoom();
            }else{
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster(){
            Debug.Log("Connected To Master");

            if(isConnecting){
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(Photon.Realtime.DisconnectCause cause){
            waitingStatusPanel.SetActive(false);
            findOpponentsPanel.SetActive(true);

            Debug.Log($"Disconnected due to: {cause}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message){
            Debug.Log("No Clients Are Waiting for opponetns, creating a new room");

            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions{MaxPlayers = maxPlayersPerRoom});
        }

        public override void OnJoinedRoom(){
            Debug.Log("Joined Room");

            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

            if(playerCount != maxPlayersPerRoom){
                waitingStatusText.text = "Waiting for more players";
                Debug.Log("Client is Waiting for opponents");
            }else{
                waitingStatusText.text = "Ready to Start";
                Debug.Log("Ready to start");
            }
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer){
            if(PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom){
                PhotonNetwork.CurrentRoom.IsOpen = false;

                Debug.Log("Ready to start");
                waitingStatusText.text = "ready to start";

                PhotonNetwork.LoadLevel("GameScene");
            }
        }

    }

}
