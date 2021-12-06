using UnityEngine;
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
}