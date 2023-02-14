using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ServerManagement : MonoBehaviourPunCallbacks
{
    private const string Arena = nameof(Arena);//room name
    private const string Paladin = nameof(Paladin);// player character prefab name


    // players spawn points
    [SerializeField] private Transform[] spawnPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // connect to server 
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the server");
        PhotonNetwork.JoinLobby(); // connect to lobby
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to the lobby");
        PhotonNetwork.JoinOrCreateRoom(Arena, new RoomOptions { MaxPlayers = 10, IsOpen = true, IsVisible = true}, TypedLobby.Default);
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("Connected to the Room");
        GameObject myObject = PhotonNetwork.Instantiate(Paladin, spawnPoints[Random.Range(0,spawnPoints.Length)].position, Quaternion.identity, 0, null);
    
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left the Room");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Left the Lobby");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could not join any room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Could not join any random room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could not create room");
    }


}