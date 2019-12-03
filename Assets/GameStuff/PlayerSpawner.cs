using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player1PreFab = null;
    [SerializeField] private GameObject player2PreFab = null;

    // Start is called before the first frame update
    void Start()
    {
        //Change dice color if is not master client
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.Instantiate(player1PreFab.name, Vector3.zero, Quaternion.identity);
        }else{
            PhotonNetwork.Instantiate(player2PreFab.name, Vector3.zero, Quaternion.identity);
        }


    }

}
