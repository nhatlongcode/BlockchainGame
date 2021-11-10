using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public abstract class ISkill : MonoBehaviourPunCallbacks
{
    public abstract bool KeyProcess(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform);
    public abstract void Action(float time, float deltaTime, Rigidbody2D rigidbody, Transform transform);
    public abstract bool OnExitSkill(float time);

    public abstract void SyncData();
}
public static class Extension
{
    public static Dictionary<Command, List<KeyCode>> validKeys = new Dictionary<Command, List<KeyCode>>
    {
        {Command.Down , new List<KeyCode>{KeyCode.S, KeyCode.DownArrow} },
        {Command.Up   , new List<KeyCode>{KeyCode.W, KeyCode.UpArrow} },
        {Command.Left , new List<KeyCode>{KeyCode.A, KeyCode.LeftArrow} },
        {Command.Right, new List<KeyCode>{KeyCode.D, KeyCode.RightArrow} },
        {Command.Space, new List<KeyCode>{KeyCode.Q, KeyCode.Space} },
        {Command.Enter, new List<KeyCode>{KeyCode.E, KeyCode.Return} },
    };

    public static bool checkInput(this ISkill skill, Command cmd)
    {
        foreach (KeyCode key in validKeys[cmd])
        {
            if (Input.GetKey(key))
                return true;
        }
        return false;
    }
}
