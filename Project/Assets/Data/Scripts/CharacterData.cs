using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData", order = 0)]
public class CharacterData : ScriptableObject 
{
    [Header("Skills")]
    public CharacterSkillsData skill1Set;
    public CharacterSkillsData skill2Set;
    public CharacterSkillsData skill3Set;
    public CharacterSkillsData skill4Set;

    [Header("Stats")]
    public float baseDamage;
    public float baseMoveSpeed;
    public float baseShotSpeed;
    public float baseHitboxSize;
    public float baseShotRadius;
    public float baseShotDamage;
    public float baseHP;
    public float baseMP;
    [Header("Body parts")]
    public CharacterBodyPartData bodyPartData;


}
