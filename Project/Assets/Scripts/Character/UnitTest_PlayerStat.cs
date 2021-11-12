using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using UnityEngine;

public class UnitTest_PlayerStat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string one = "0123456789abcdef000000000000000000000000000000000000000000000000";
        string two = "123456789abcdef0121212121212121212121212121212121212121212121212";
        BigInteger parent1 = BigInteger.Parse(one, NumberStyles.AllowHexSpecifier);
        Debug.Log(parent1.ToString());
        BigInteger parent2 = BigInteger.Parse(two, NumberStyles.AllowHexSpecifier);

        PlayerStat unit1 = new PlayerStat(parent1);
        PlayerStat unit2 = new PlayerStat(parent2);

        PlayerStat child = new PlayerStat(PlayerStat.Breed(unit1.Id.ToByteArray(), unit2.Id.ToByteArray()));
        Debug.Log(child.Id.ToString("X"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
