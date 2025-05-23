using UnityEngine;

namespace Main.Scripts.SaveSystem
{
    public class PlayerPrefSaveService : ISaveService
    {
        public void Write(SaveWrapper wrapper, string path)
        {
            var json = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(path, json);
        }

        public SaveWrapper Read(string path)
        {
            var json = PlayerPrefs.GetString(path, "");
            return JsonUtility.FromJson<SaveWrapper>(json);
        }
    }
}
