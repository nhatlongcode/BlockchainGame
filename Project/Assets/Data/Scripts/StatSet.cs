using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Stat
{
    public int id;
    public string name;
    public int baseValue;
}

[CreateAssetMenu(fileName = "StatSet", menuName = "Data/StatSet", order = 0)]
public class StatSet : ScriptableObject {
    
    public Stat defaultStat;
    public List<Stat> stats;

}