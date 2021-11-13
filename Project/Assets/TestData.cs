using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TestData : MonoBehaviour
{

    public string idString;
    public ID id = new ID();
    private void Start() 
    {
        var result = new System.Text.StringBuilder();
        id.bigInt = BigInteger.Parse(idString);
        var bits = new BitArray(id.bytes);
        Debug.Log(ToBitString(bits));
        // foreach(var bite in id.bytes)
        // {
        //     var bits = new BitArray()
        //     //result.Append(ToBitString(bits));
        //     //Debug.Log(ToBitString(bits));
        //     Debug.Log(bite);
        //     Debug.Log(ToBitString(bits))
        // }
        //result.ToString();
        //Debug.Log(result);
    }

    public string ToBitString(BitArray bits)
    {
        var sb = new System.Text.StringBuilder();

        for (int i = bits.Count - 1; i >= 0; i--)
        {
            char c = bits[i] ? '1' : '0';
            sb.Append(c);
        }

        return sb.ToString();
    }
}
