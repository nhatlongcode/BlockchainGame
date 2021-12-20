using UnityEngine.UI;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [Header("Body part")]
    public Image bodyImage;
    public Image eyeImage;
    public Image hairImage;
    public Image mouthImage;
    public Image beardImage;
    public Image tailImage;
    public Image earLefImage;
    public Image earRightImage;
    public PlayerStat playerStat;
    public void ShowCharacter(PlayerStat stat)
    {
        playerStat = stat;
        AssignBody(DataManager.Instance.data.bodyPartData.GetBody(stat.Body).sprite);
        AssignEye(DataManager.Instance.data.bodyPartData.GetEye(stat.Eye).sprite);
        AssignHair(DataManager.Instance.data.bodyPartData.GetHair(stat.Hair).sprite);
        AssignMouth(DataManager.Instance.data.bodyPartData.GetMouth(stat.Mouth).sprite);
        AssignBeard(DataManager.Instance.data.bodyPartData.GetBeard(stat.Beard).sprite);
        AssignTail(DataManager.Instance.data.bodyPartData.GetTail(stat.Tail).sprite);
        AssignEar(DataManager.Instance.data.bodyPartData.GetEar(stat.Ear).sprite);
    }

    public void ClearCharacter()
    {
        bodyImage.sprite = null;
        eyeImage.sprite = null;
        hairImage.sprite = null;
        mouthImage.sprite = null;
        beardImage.sprite = null;
        tailImage.sprite = null;
        earRightImage.sprite = null;
        earLefImage.sprite = null;
        playerStat = null;
    }

    public void AssignBody(Sprite sprite)
    {
        bodyImage.sprite = sprite;
        bodyImage.SetNativeSize();
    }

    public void AssignEye(Sprite sprite)
    {
        eyeImage.sprite = sprite;
        eyeImage.SetNativeSize();
    }

    public void AssignHair(Sprite sprite)
    {
        hairImage.sprite = sprite;
        hairImage.SetNativeSize();
    }
    public void AssignMouth(Sprite sprite)
    {
        mouthImage.sprite = sprite;
        mouthImage.SetNativeSize();
    }
    public void AssignBeard(Sprite sprite)
    {
        beardImage.sprite = sprite;
        beardImage.SetNativeSize();
    }
    public void AssignTail(Sprite sprite)
    {
        tailImage.sprite = sprite;
        tailImage.SetNativeSize();
    }
    public void AssignEar(Sprite sprite)
    {
        earRightImage.sprite = sprite;
        earLefImage.sprite = sprite;
        earRightImage.SetNativeSize();
        earLefImage.SetNativeSize();
    }

}
