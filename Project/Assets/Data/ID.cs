using System.Collections;
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
        }
    }
    private BigInteger _bigInt;
    public byte[] ToBytes()
    {
        return _bigInt.ToByteArray();
    }
    public int GetValue(int from, int to)
    {

        return 1;
    }

    public string ToBitString()
    {
        BitArray bits = new BitArray(ToBytes());
        var sb = new System.Text.StringBuilder();

        for (int i = bits.Count - 1; i >= 0; i--)
        {
            char c = bits[i] ? '1' : '0';
            sb.Append(c);
        }

        return sb.ToString();
    }

    public string GetBits(byte start, byte count)
    {
        string bits = ToBitString();
        var sb = new System.Text.StringBuilder();

        for (int i = start - count + 1; i <= start; i++)
        {
            char c = bits[i];
            sb.Append(c);
        }

        return sb.ToString();
    }

}