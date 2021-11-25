using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BodyPart
{
    public string name;
    public int id;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "BodySet", menuName = "Data/BodySet", order = 0)]
public class BodySet : ScriptableObject {

    public int id;
    public string name;
    public BodyPart defaultBodypart;
    public List<BodyPart> bodyParts;
    
}