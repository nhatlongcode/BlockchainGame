using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{
    public List<HistoryObject> historyObjectList;
    static History _instance;
    public static History Inst { get => _instance; }

    public float initialTime;
    public bool paused;
    public float time;

    public History()
    {
        if (_instance == null)
            _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
            time += Time.deltaTime;
        List<HistoryObject> historyObjectListClone = new List<HistoryObject>(historyObjectList);
        foreach (HistoryObject item in historyObjectListClone)
        {
            item.currentTime = time;
            item.MyUpdate();
        }
    }
}
