using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPreFab = null;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(playerPreFab.name, Vector3.zero, Quaternion.identity);
    }

}
