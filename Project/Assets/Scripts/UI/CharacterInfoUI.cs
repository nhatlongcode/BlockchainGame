using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
public class CharacterInfoUI : MonoBehaviour
{
    public CharacterVisual characterVisual;
    public Text IDText;
    public Text skill1Text;
    public Text skill2Text;
    public Text skill3Text;
    public Text skill4Text;
    public Text damageText;
    public Text moveSpeedText;
    public Text shotSpeedText;
    
    public Text sizeText;
    public Text shotRadiusText;
    public Text shotDamageText;
    public Text HpText;
    public Text MpText;

    public void ShowCharacterStat(PlayerStat stat)
    {
        skill1Text.text = ChooseSkillID(stat.Skill1, DataManager.Instance.data.skill1Set.skills).name;
        skill2Text.text = ChooseSkillID(stat.Skill2, DataManager.Instance.data.skill2Set.skills).name;
        skill3Text.text = ChooseSkillID(stat.Skill3, DataManager.Instance.data.skill3Set.skills).name;
        skill4Text.text = ChooseSkillID(stat.Skill4, DataManager.Instance.data.skill4Set.skills).name;
        damageText.text = CalcStat(DataManager.Instance.data.baseDamage,stat.DamageModifier).ToString();
        moveSpeedText.text = CalcStat(DataManager.Instance.data.baseMoveSpeed,stat.MoveSpeedModifier).ToString();
        shotSpeedText.text = CalcStat(DataManager.Instance.data.baseShotSpeed,stat.ShotSpeedModifier).ToString();
        sizeText.text = CalcStat(DataManager.Instance.data.baseHitboxSize,stat.HitboxSizeModifier).ToString();
        shotRadiusText.text = CalcStat(DataManager.Instance.data.baseShotRadius,stat.ShotRadiusModifier).ToString();
        shotDamageText.text = CalcStat(DataManager.Instance.data.baseShotDamage,stat.ShotDamageModifier).ToString();
        HpText.text = CalcStat(DataManager.Instance.data.baseHP,stat.HP_Modifier).ToString();
        MpText.text = CalcStat(DataManager.Instance.data.baseMP,stat.MP_Modifier).ToString();
    }

    public SkillData ChooseSkillID(int index, List<SkillData> list)
    {
        if (index >= list.Count || index < 0) return list[0];
        return list[index];
    }

    public float CalcStat(float baseStat, float modifier)
    {
        return baseStat * modifier;
    }
}
