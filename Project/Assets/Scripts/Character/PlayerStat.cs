using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

class PlayerStat
{
    public BigInteger Id { get => new BigInteger(id); }
    private byte[] id;
    public PlayerStat(BigInteger id)
    {
        this.id = id.ToByteArray();
        Array.Resize(ref this.id, 32);
        this.id = this.id.Reverse().ToArray();
        CalculateWindPattern();
        CalculateCritChance();
    }
    public PlayerStat(byte[] id)
    {
        this.id = id.Clone() as byte[];
        Array.Resize(ref this.id, 32);
        CalculateWindPattern();
        CalculateCritChance();
    }

    #region SkillID
    public int Skill1
    {
        get
        {
            return id[0] << 8 + id[1];
        }
    }

    public int Skill2
    {
        get
        {
            return id[2] << 8 + id[3];
        }
    }

    public int Skill3
    {
        get
        {
            return id[4] << 8 + id[5];
        }
    }

    public int Skill4
    {
        get
        {
            return id[6] << 8 + id[7];
        }
    }
    #endregion

    #region DamageModifier
    public float DamageModifier
    {
        get => (92 + PatternMatching(pattern1, 8, 9)) / 100.0f;
    }
    public float MoveSpeedModifier
    {
        get => (92 + PatternMatching(pattern2, 8, 9)) / 100.0f;
    }
    public float ShotSpeedModifier
    {
        get => (92 + PatternMatching(pattern3, 8, 9)) / 100.0f;
    }
    public float HitboxSizeModifier
    {
        get => (92 + PatternMatching(pattern1, 10, 11)) / 100.0f;
    }
    public float ShotRadiusModifier
    {
        get => (92 + PatternMatching(pattern2, 10, 11)) / 100.0f;
    }
    public float ShotDamageModifier
    {
        get => (92 + PatternMatching(pattern3, 10, 11)) / 100.0f;
    }
    public float HP_Modifier
    {
        get => (84 + PatternMatching(pattern1, 12, 13, 14, 15)) / 100.0f;
    }
    public float MP_Modifier
    {
        get => (84 + PatternMatching(pattern2, 12, 13, 14, 15)) / 100.0f;
    }
    #endregion

    #region Skill Modifier
    public int Skill1Mod
    {
        get
        {
            return id[16] << 8 + id[17];
        }
    }

    public int Skill2Mod
    {
        get
        {
            return id[18] << 8 + id[19];
        }
    }

    public int Skill3Mod
    {
        get
        {
            return id[20] << 8 + id[21];
        }
    }

    public int Skill4Mod
    {
        get
        {
            return id[22] << 8 + id[23];
        }
    }
    #endregion

    #region Luck
    private int WindPatternAlphaX;
    private int WindPatternDeltaX;
    private int WindPatternAlphaY;
    private int WindPatternDeltaY;
    public static Vector2 GetWind(float t, params PlayerStat[] players)
    {
        Vector2 result = new Vector2();
        foreach (var player in players)
        {
            result.X += (float) Math.Sin(player.WindPatternAlphaX * t + player.WindPatternDeltaX);
            result.Y += (float) Math.Sin(player.WindPatternAlphaY * t + player.WindPatternDeltaY);
        }
        return result;
    }
    public readonly int[] primes = new int[20]{2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67,71};
    protected void CalculateWindPattern()
    {
        WindPatternAlphaX = primes[PatternMatching(pattern1, 24, 25)];
        WindPatternDeltaX = PatternMatching(pattern2, 24, 25);
        WindPatternAlphaY = primes[PatternMatching(pattern3, 24, 25)];
        WindPatternDeltaY = PatternMatching(pattern4, 24, 25);
    }
    
    public int CritDenominator;
    protected void CalculateCritChance()
    {
        CritDenominator = primes[PatternMatching(pattern3, 26, 27) + 1];
    }
    public static bool IsCrit(float time, PlayerStat player)
    {
        return (int)Math.Floor(time) % player.CritDenominator == 0;
    }

    // TODO: 
    #endregion

    public const uint pattern1 = 0x00000000;
    public const uint pattern2 = 0x55555555;
    public const uint pattern3 = 0x33333333;
    public const uint pattern4 = 0x0f0f0f0f;
    public int PatternMatching(uint pattern, params int[] bytes)
    {
        int result = 0;
        foreach (var item in bytes)
        {
            uint countMatch = id[item] ^ pattern;
            int offset = item * 8;
            for (int i = 7; i >= 0; i--)
            {
                if (countMatch % 2 == 0)
                    result++;
                countMatch /= 2;
            }
            pattern >>= 8;
        }
        return result;
    }

    public const int BigPrime = 1000000007;

    /// WARNING: parent order dependant
    public static bool VerifyIsChild(byte[] parent1, byte[] parent2, byte[] child)
    {
        if (parent1.Length != 32)
            return false;
        if (parent2.Length != 32)
            return false;
        if (child.Length != 32)
            return false;

        return child.Equals(Breed(parent1, parent2));
    }
    
    public static byte[] Breed(byte[] parent1, byte[] parent2)
    {
        Array.Resize(ref parent1, 32);
        Array.Resize(ref parent1, 32);

        byte[] result = new byte[32];

        byte carry = 0;

        // Randomize stat & skill stat & luck modifier: function = circle_shift(a,4,8) + b
        for (int i = 31; i > 7; i--) { 
            result[i] = Add(parent1[i], parent2[i], carry, out carry);
        }
        // This stat is 4 byte wide. Recalculate.
        /*
        result[15] = Add(parent1[15], parent2[15], 0, out carry);
        result[14] = Add(parent1[14], parent2[14], carry, out carry);
        result[13] = Add(parent1[13], parent2[13], carry, out carry);
        result[12] = Add(parent1[12], parent2[12], carry, out _);
        //*/

        BigInteger seed = new BigInteger(result.Skip(8).ToArray());
        BigInteger modulo = new BigInteger(BigPrime);
        int random = (int)(seed % modulo);

        byte[] refpar1;
        byte[] refpar2;
        // Half chance for main class skill
        if (random % 2 == 0)
        {
            refpar1 = parent1;
            refpar2 = parent2;
        }
        else
        {
            refpar1 = parent2;
            refpar2 = parent1;
        }
        result[0] = refpar1[0];
        result[1] = refpar1[1];
        // 3/7 chance to inherit from secondary skill. 1/7 chance to inherit from parent main skill
        switch (random % 7)
        {
            case 1:
            case 2:
            case 3:
                result[2] = refpar1[2];
                result[3] = refpar1[3];
                break;
            case 4:
            case 5:
            case 6:
                result[2] = refpar2[2];
                result[3] = refpar2[3];
                break;
            case 0:
                result[2] = refpar2[0];
                result[3] = refpar2[1];
                break;
        }
        switch (random % 5)
        {
            case 0:
            case 1:
                result[4] = parent1[4];
                result[5] = parent1[5];
                break;
            case 2:
            case 3:
            case 4:
                result[4] = refpar1[6];
                result[5] = refpar1[7];
                break;
            default:
                break;
        }
        switch (random % 5)
        {
            case 1:
            case 3:
                result[6] = parent2[4];
                result[7] = parent2[5];
                break;
            case 0:
            case 2:
                result[6] = parent2[6];
                result[7] = parent2[7];
                break;
            case 4:
                result[6] = refpar1[4];
                result[7] = refpar1[5];
                break;
            default:
                break;
        }
        return result;
    }

    private static byte Add(byte a, byte b, byte incarry , out byte outcarry)
    {
        int result = (byte)((a << 4) | (a >> 4)) + b + incarry;
        outcarry = (byte)((result >> 8) % 2);
        return (byte)result;
    }
}
