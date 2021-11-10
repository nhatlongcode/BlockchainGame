using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMatchmakingModel : MonoBehaviour
{
    public string PlayerName;
    public string PlayerID;
    // Ranking -1 is unranked
    public int Ranking;
    public Player player;

    public float TimeInMatchmaking = 0;

    [PunRPC]
    public void UploadData(Player player, int Ranking = -1, string PlayerID = null, string PlayerName = null)
    {
        this.player = player;
        this.Ranking = Ranking;
        this.PlayerID = PlayerID;
        this.PlayerName = PlayerName;
    }

    public void Update()
    {
        TimeInMatchmaking += Time.deltaTime;
    }
}
