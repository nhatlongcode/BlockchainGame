using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
using System;
using ExitGames.Client.Photon;

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

    public List<ISkill> YourSkills;

    public static HashSet<Type> SerializeTypesToRegister = new HashSet<Type>();
    static GameManager inst;
    public GameManager Inst { get => inst; }
    public GameManager()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Char[0] = PhotonNetwork.Instantiate("Player", PosChar1, RotChar1);

        foreach (var item in Char)
            if (item != null)
            {
                ArrowKeyControlledPlayer character = item.GetComponent<ArrowKeyControlledPlayer>();
                foreach (var skill in character.SkillList)
                    if (skill != null)
                    {
                        YourSkills.Add(skill);
                    }
                character.mine = true;
            }
        byte count = 255;
        List<Type> types = new List<Type>(SerializeTypesToRegister);
        types.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
        foreach(var type in types)
        {
            Serializer.RegisterCustomType(count, type);
            count--;
        }
    }

    public bool sync;
    void LateUpdate()
    {
        if (sync)
        {
            SyncForEnemy();
            sync = false;
        }
    }

    void SyncForEnemy()
    {
        foreach(var item in YourSkills)
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

        PhotonNetwork.Disconnect();
    }
}
