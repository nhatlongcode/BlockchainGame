using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class SkillData
{
    public string name;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "CharacterSkillsData", menuName ="Data/CharacterSkillsData", order = 0)]
public class CharacterSkillsData : ScriptableObject 
{
    public List<SkillData> skills;    
}