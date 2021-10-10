using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyControlledPlayer : MonoBehaviour
{
    public enum Command
    {
        Up, Down, Left, Right, Space, Enter, 
    }
    enum State
    {
        OnLadder = 1,           // TODO: Add snap to ladder feature
        OnGround = 2,           // TODO: Add snap to ground feature

    }

    public Dictionary<Command, List<KeyCode>> validKeys = new Dictionary<Command, List<KeyCode>>
    {
        {Command.Down , new List<KeyCode>{KeyCode.S, KeyCode.DownArrow} },
        {Command.Up   , new List<KeyCode>{KeyCode.W, KeyCode.UpArrow} },
        {Command.Left , new List<KeyCode>{KeyCode.A, KeyCode.LeftArrow} },
        {Command.Right, new List<KeyCode>{KeyCode.D, KeyCode.RightArrow} },
        {Command.Space, new List<KeyCode>{KeyCode.Q, KeyCode.Space} },
        {Command.Enter, new List<KeyCode>{KeyCode.E, KeyCode.Return} },
    };

    Vector2 direction(Command dir)
    {
        switch (dir)
        {
            case Command.Up:
                return new Vector2(0, 1);
            case Command.Down:
                return new Vector2(0, -1);
            case Command.Left:
                return new Vector2(-1, 1);
            case Command.Right:
                return new Vector2(1, 1);
            default:
                return new Vector2(0, 0);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool checkInput(Command cmd)
    {
        foreach (KeyCode key in validKeys[cmd])
        {
            if (Input.GetKeyDown(key))
                return true;
        }
        return false;
    }

    void LeftRightMovement()
    {
        if (checkInput(Command.Left))
        {

        }
    }
}