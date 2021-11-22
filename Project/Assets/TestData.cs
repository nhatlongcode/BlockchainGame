using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class CharStat
{
    public string name;
    public byte bitCount;
}
public class TestData : MonoBehaviour
{
    public string idString;
    public List<CharStat> stats;

    public ID id = new ID();
    private void Start() 
    {
        var result = new System.Text.StringBuilder();
        id.bigInt = BigInteger.Parse(idString);
        Debug.Log(id.ToBitString());
        string bits = (id.ToBitString());
        LogStat();
    }

    public void LogStat()
    {
        byte start = (byte)(id.ToBitString().Length - 1);
        foreach(var stat in stats)
        {
            Debug.Log("stat name: " + stat.name);
            Debug.Log("data: " + id.GetBits(start, stat.bitCount));
            start -= stat.bitCount;

        }
    }

}
