
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Main.Scripts.SaveSystem
{
    public class JsonSaveService : ISaveService
    {

        private readonly string _basePath;
        public JsonSaveService(string basePath)
        {
            _basePath = basePath;
        }

        public void Write(SaveWrapper wrapper, string path)
        {
            if (!path.EndsWith(".json"))
                path += ".json";

            var extendedPath = Path.Combine(_basePath, path);
            var json = JsonUtility.ToJson(wrapper, true);
            File.WriteAllText(extendedPath, json);
        }

        public SaveWrapper Read(string path)
        {
            if (!path.EndsWith(".json"))
                path += ".json";

            var extendedPath = Path.Combine(_basePath, path);

            if (!File.Exists(extendedPath))
            {
                return new SaveWrapper(new List<SaveContainer>());
            }

            var json = File.ReadAllText(extendedPath);
            return JsonUtility.FromJson<SaveWrapper>(json);
        }
    }
}
