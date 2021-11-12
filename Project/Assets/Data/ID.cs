using System;
using System.Numerics;
public class ID
{
    public BigInteger bigInt
    {
        get 
        {
            return bigInt;
        }
        set
        {
            bytes = bigInt.ToByteArray();
        }
    }
    public byte[] bytes { get; private set; }

    public int GetValue(int from, int to)
    {

        return 1;
    }

}