using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TestData : MonoBehaviour
{

    public string idString;
    public ID id = new ID();
    private void Start() {
        var result = new System.Text.StringBuilder();
        id.bigInt = BigInteger.Parse(idString);
        
        foreach(var bite in id.bytes)
        {
            var bits = new BitArray(bite);
            result.Append(ToBitString(bits));
        }
        result.ToString();
        Debug.Log(result);
    }

    public string ToBitString(BitArray bits)
    {
        var sb = new System.Text.StringBuilder();

        for (int i = 0; i < bits.Count; i++)
        {
            char c = bits[i] ? '1' : '0';
            sb.Append(c);
        }

        return sb.ToString();
    }
}
