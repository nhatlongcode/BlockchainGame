using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Skill
{
    public string name;
    public int id;
    
}

[CreateAssetMenu(fileName = "SkillSet", menuName = "Data/SkillSet", order = 0)]
public class SkillSet : ScriptableObject {
    public Skill defaultSkill;
    public List<Skill> skills;

}