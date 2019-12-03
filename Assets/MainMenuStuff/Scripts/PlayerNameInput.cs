using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

namespace DiceRoller.Menus
{
    public class PlayerNameInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button continueButton = null;

        private const string PlayerPrefsNameKey = "PlayerName";

        private void Start() => SetUpInputField();

        private void SetUpInputField()
        {
            nameInputField.onValueChanged.AddListener(delegate { 
                SetPlayerName(nameInputField.text);
            });

            if(!PlayerPrefs.HasKey(PlayerPrefsNameKey)){
                return;
            }

            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            nameInputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string name)
        {
            print("Changed: " + name);
            continueButton.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName(){
            string playerName = nameInputField.text;

            PhotonNetwork.NickName = playerName;

            PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
        }
    }

}
