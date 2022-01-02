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
    public Text tailText;
    public Image tailImage;
    public Text bodyText;
    public Image bodyImage;
    public Text eyeText;
    public Image eyeImage;
    public Text beardText;
    public Image beardImage;
    public Text mouthText;
    public Image mouthImage;
    public Text earText;
    public Image earImage;
    public Text hairText;
    public Image hairImage;
    public Text moveSpeedText;
    public Text shotSpeedText;
    
    public Text sizeText;
    public Text shotRadiusText;
    public Text shotDamageText;
    public Text HpText;
    public Text MpText;

    public void ShowCharacterStat(PlayerStat stat)
    {
        skill1Text.text = ChooseSkill(stat.Skill1, DataManager.Instance.data.skill1Set.skills).name;
        skill2Text.text = ChooseSkill(stat.Skill2, DataManager.Instance.data.skill2Set.skills).name;
        skill3Text.text = ChooseSkill(stat.Skill3, DataManager.Instance.data.skill3Set.skills).name;
        skill4Text.text = ChooseSkill(stat.Skill4, DataManager.Instance.data.skill4Set.skills).name;
        
        characterVisual.ShowCharacter(stat);
        
        AssignBodyPartInfo(tailText, tailImage, DataManager.Instance.data.bodyPartData.GetTail(stat.Tail));
        AssignBodyPartInfo(bodyText, bodyImage, DataManager.Instance.data.bodyPartData.GetBody(stat.Body));
        AssignBodyPartInfo(eyeText, eyeImage, DataManager.Instance.data.bodyPartData.GetEye(stat.Eye));
        AssignBodyPartInfo(beardText, beardImage, DataManager.Instance.data.bodyPartData.GetBeard(stat.Beard));
        AssignBodyPartInfo(mouthText, mouthImage, DataManager.Instance.data.bodyPartData.GetMouth(stat.Mouth));
        AssignBodyPartInfo(hairText, hairImage, DataManager.Instance.data.bodyPartData.GetHair(stat.Hair));
        AssignBodyPartInfo(earText, earImage, DataManager.Instance.data.bodyPartData.GetEar(stat.Ear));

        damageText.text = CalcStat(DataManager.Instance.data.baseDamage,stat.DamageModifier).ToString();
        moveSpeedText.text = CalcStat(DataManager.Instance.data.baseMoveSpeed,stat.MoveSpeedModifier).ToString();
        shotSpeedText.text = CalcStat(DataManager.Instance.data.baseShotSpeed,stat.ShotSpeedModifier).ToString();
        sizeText.text = CalcStat(DataManager.Instance.data.baseHitboxSize,stat.HitboxSizeModifier).ToString();
        shotRadiusText.text = CalcStat(DataManager.Instance.data.baseShotRadius,stat.ShotRadiusModifier).ToString();
        shotDamageText.text = CalcStat(DataManager.Instance.data.baseShotDamage,stat.ShotDamageModifier).ToString();
        HpText.text = CalcStat(DataManager.Instance.data.baseHP,stat.HP_Modifier).ToString();
        MpText.text = CalcStat(DataManager.Instance.data.baseMP,stat.MP_Modifier).ToString();
    }

    public SkillData ChooseSkill(int index, List<SkillData> list)
    {
        //Debug.Log(index);
        //if (index >= list.Count || index < 0) return list[0];
        return list[index % list.Count];
    }


    public void AssignBodyPartInfo(Text infoText, Image infoImage, BodyPart part)
    {
        infoText.text = part.name;
        infoImage.sprite = part.sprite;
    }

    public float CalcStat(float baseStat, float modifier)
    {
        return baseStat * modifier;
    }

    public void SetActive(bool status)
    {
        //ShowCharacterStat(stat);
        this.gameObject.SetActive(status);
    }

}
