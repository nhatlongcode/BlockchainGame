using UnityEngine.UI;
using System.Collections.Generic;
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
