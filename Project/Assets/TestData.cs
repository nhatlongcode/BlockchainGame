using System.Collections;
using System.Collections.Generic;

using System.Numerics;
using UnityEngine;

public class TestData : MonoBehaviour
{
    public CharacterInfoUI infoUI;
    public string idString;

    private void Start() 
    {

        PlayerStat character = new PlayerStat(BigInteger.Parse(idString));
        infoUI.ShowCharacterStat(character);
        LogStat(character);
        
    }

    public void LogStat(PlayerStat character)
    {
        Debug.Log("Skill1: " + character.Skill1);
        Debug.Log("Skill2: " + character.Skill2);
        Debug.Log("Skill3: " + character.Skill3);
        Debug.Log("Skill4: " + character.Skill4);
        Debug.Log("DamageModifier: " + character.DamageModifier);
        Debug.Log("MoveSpeedModifier: " + character.MoveSpeedModifier);
        Debug.Log("ShotSpeedModifier: " + character.ShotSpeedModifier);
        Debug.Log("HitboxSizeModifier: " + character.HitboxSizeModifier);
        Debug.Log("ShotRadiusModifier: " + character.ShotRadiusModifier);
        Debug.Log("ShotDamageModifier: " + character.ShotDamageModifier);
        Debug.Log("HP_Modifier: " + character.HP_Modifier);
        Debug.Log("MP_Modifier: " + character.MP_Modifier);
        Debug.Log("Skill1Mod: " + character.Skill1Mod);
        Debug.Log("Skill2Mod: " + character.Skill2Mod);
        Debug.Log("Skill3Mod: " + character.Skill3Mod);
        Debug.Log("Skill4Mod: " + character.Skill4Mod);
    }


}
