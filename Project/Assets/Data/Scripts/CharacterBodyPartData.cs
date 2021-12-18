using UnityEngine;
using System.Numerics;
using System.Collections.Generic;

[System.Serializable]
public class BodyPart
{
    public string name;
    public Sprite sprite;
}
[CreateAssetMenu(fileName = "CharacterBodyPartData", menuName = "Data/CharacterBodyPartData", order = 0)]
public class CharacterBodyPartData : ScriptableObject 
{
    public List<BodyPart> bodySet;
    public List<BodyPart> faceSet;
    public List<BodyPart> hairSet;
    public List<BodyPart> mouthSet;
    public List<BodyPart> breadSet;
    public List<BodyPart> tailSet;
    public List<BodyPart> earSet;

    public BodyPart GetBody(int rawID)
    {
        return bodySet[rawID % bodySet.Count];
    }
    public BodyPart GetEye(int rawID)
    {
        return faceSet[rawID % faceSet.Count];
    }
    public BodyPart GetHair(int rawID)
    {
        return hairSet[rawID % hairSet.Count];
    }
    public BodyPart GetMouth(int rawID)
    {
        return mouthSet[rawID % mouthSet.Count];
    }
    public BodyPart GetBeard(int rawID)
    {
        return breadSet[rawID % breadSet.Count];
    }
    public BodyPart GetTail(int rawID)
    {
        return tailSet[rawID % tailSet.Count];
    }
    public BodyPart GetEar(int rawID)
    {
        return earSet[rawID % earSet.Count];
    }


}