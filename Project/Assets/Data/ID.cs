using System;
using System.Numerics;
public class ID
{
    public BigInteger bigInt
    {
        get 
        {
            return _bigInt;
        }
        set
        {
            _bigInt = value;
            bytes = _bigInt.ToByteArray();
        }
    }
    private BigInteger _bigInt;
    public byte[] bytes { get; private set; }

    public int GetValue(int from, int to)
    {

        return 1;
    }

}