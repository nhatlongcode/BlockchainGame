using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Matchmaking : MonoBehaviourPunCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
{
    public float TimeUntilRestartPrompt = 10;

    void Update()
    {
        TimeUntilRestartPrompt -= Time.deltaTime;
        if (TimeUntilRestartPrompt < 0)
            ;// TODO
    }

    // Start is called before the first frame update
    void Start()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("Joining Lobby");
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connecting");
        }
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinRandomOrCreateRoom();
        Debug.Log("Joining room");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
        Debug.Log("Joining Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        PhotonNetwork.JoinRandomOrCreateRoom();
        Debug.Log("Joining room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        PhotonNetwork.JoinRandomOrCreateRoom();
        Debug.Log("Joining room");
    }

    /*
    [SerializeField]
    PlayerMatchmakingModel myModel;

    static Matchmaking inst;
    public static Matchmaking Inst { get => inst; }
    public HashSet<PlayerMatchmakingModel> PlayerPool;

    bool AcceptedMatch = false;
    bool AwaitAcceptMatch = true;

    public float ResetTime = 1f;
    float countdown = 0;

    Matchmaking()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 0;

        PhotonNetwork.JoinOrCreateRoom("Matchmaking", roomOptions, TypedLobby.Default);

        var go = PhotonNetwork.Instantiate("PlayerMatchmakingModel", new Vector3(), Quaternion.identity);
        myModel = go.GetComponent<PlayerMatchmakingModel>();
    }
    
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            countdown = ResetTime;
            PlayerPool.Clear();
            AwaitAcceptMatch = false;
        }
    }


    
    static bool CheckCompatibleMatch(PlayerMatchmakingModel one, PlayerMatchmakingModel other)
    {
        return true;
    }

    string RoomName()
    {
        return CreateRandomName();
    }

    void OfferMatch(PlayerMatchmakingModel other)
    {
        if (!AwaitAcceptMatch && CheckCompatibleMatch(myModel, other))
        {
            photonView.RPC("AcceptMatch", other.player, myModel, RoomName());
            AwaitAcceptMatch = true;
        }
    }

    [PunRPC]
    void AcceptMatch(PlayerMatchmakingModel other, string roomName)
    {
        if (!AwaitAcceptMatch && !AcceptedMatch && CheckCompatibleMatch(myModel, other))
        {
            EnterMatch(roomName);
            AcceptedMatch = true;
            photonView.RPC("AcceptMatch", other.player, myModel, roomName);
        }
    }

    void EnterMatch(string roomName)
    {
        PhotonNetwork.LeaveRoom();
    }

    static string CreateRandomName()
    {
        string name = "";

        for (int counter = 1; counter <= 10; ++counter)
        {
            bool upperCase = (Random.Range(0, 2) == 1);

            int rand = 0;
            if (upperCase)
            {
                rand = Random.Range(65, 91);
            }
            else
            {
                rand = Random.Range(97, 123);
            }

            name += (char)rand;
        }

        return name;
    }
    //*/
}
