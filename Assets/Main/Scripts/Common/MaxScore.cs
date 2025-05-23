using Main.Scripts.SaveSystem;
using System;
using UnityEngine;

[Serializable]
public class MaxScore : ISaveData
{
    public int Val;
    
    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }

    public void Deserialize(string data)
    {
        var obj = JsonUtility.FromJson<MaxScore>(data);
        if (obj != null)
            this.Val = obj.Val;
    }

}
