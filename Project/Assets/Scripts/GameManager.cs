using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Init positions will be 180 degrees oposite of each other
    public Vector2 PosChar1;
    public Quaternion RotChar1;
    public Vector2 PosChar2;
    public Quaternion RotChar2;
    public Vector2 PosChar3;
    public Quaternion RotChar3;

    public GameObject[] Char = new GameObject[3];
    public GameObject[] CharEnemy = new GameObject[3];

    public List<ISkill> EnemySkills;

    // Start is called before the first frame update
    void Start()
    {
        Char[0] = PhotonNetwork.Instantiate("Player", PosChar1, RotChar1);




    }

    void PublishActions()
    {

    }

    void SyncForEnemy()
    {
        foreach(var item in EnemySkills)
        {
            item.SyncData();
        }
    }

    void OnDestroy()
    {
        foreach (var item in Char)
            if (item != null)
                PhotonNetwork.Destroy(item);

        foreach (var item in CharEnemy)
            if (item != null)
                PhotonNetwork.Destroy(item);
    }
}
