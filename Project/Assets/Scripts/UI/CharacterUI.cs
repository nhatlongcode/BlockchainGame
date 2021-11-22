
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [Header("Data")]
    public SkillSet skillSet1;
    public SkillSet skillSet2;
    public SkillSet skillSet3;
    public SkillSet skillSet4;

    [Header("Skills")]
    public Text Skill1Text;
    public Text Skill2Text;
    public Text Skill3Text;
    public Text Skill4Text;

    [Header("Stats")]
    
    [Header("Bodypart")]   
    public Text text;

    public void AssignDataToVisual(PlayerStat player)
    {
        AssignSkill1(player.Skill1);
        AssignSkill2(player.Skill2);
        AssignSkill3(player.Skill3);
        AssignSkill4(player.Skill4);
    }

    private void AssignSkill1(int id)
    {
        if (id < skillSet1.skills.Count)
        {
            Skill1Text.text = skillSet1.skills[id].name;
        }
        else
        {
            Skill1Text.text = skillSet1.defaultSkill.name;
        }
    }

    private void AssignSkill2(int id)
    {
        if (id < skillSet2.skills.Count)
        {
            Skill1Text.text = skillSet2.skills[id].name;
        }
        else
        {
            Skill1Text.text = skillSet2.defaultSkill.name;
        }
    }

    private void AssignSkill3(int id)
    {
        if (id < skillSet3.skills.Count)
        {
            Skill1Text.text = skillSet3.skills[id].name;
        }
        else
        {
            Skill1Text.text = skillSet3.defaultSkill.name;
        }
    }

    private void AssignSkill4(int id)
    {
        if (id < skillSet4.skills.Count)
        {
            Skill1Text.text = skillSet4.skills[id].name;
        }
        else
        {
            Skill1Text.text = skillSet4.defaultSkill.name;
        }
    }
}
