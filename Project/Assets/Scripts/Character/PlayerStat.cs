using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

public class PlayerStat
{
    public BigInteger Id { get => new BigInteger(id); }
    private byte[] id;
    public PlayerStat(BigInteger id)
    {
        this.id = id.ToByteArray();
        Assert.AreEqual(this.id.Length, 32);
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
}
