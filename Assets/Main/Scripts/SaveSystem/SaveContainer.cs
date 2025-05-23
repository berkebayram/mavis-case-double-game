using System;

namespace Main.Scripts.SaveSystem
{
    [Serializable]
    public class SaveContainer
    {
        public string Key;
        public string Data;

        public SaveContainer(string key, string data)
        {
            Key = key;
            Data = data;
        }
    }
}

