using System.Collections.Generic;
namespace Main.Scripts.SaveSystem
{
    public class SaveSystem : ISaveSystem
    {
        private readonly string _id;
        private readonly ISaveService _service;
        private readonly Dictionary<string, ISaveable> _saveables = new();

        public SaveSystem(string gameIdentifier, ISaveService service)
        {
            _id = gameIdentifier;
            _service = service;
        }

        public void LoadGame()
        {
            var wrapper = _service.Read(_id);
            if (wrapper == null || wrapper.Data == null)
                return;
            foreach (var saveData in wrapper.Data)
            {
                if (!_saveables.TryGetValue(saveData.Key, out ISaveable saveable))
                    continue;

                saveable.Load(saveData.Data);
            }
        }

        public void SaveGame()
        {
            var wrapperContext = new List<SaveContainer>();
            foreach (var saveable in _saveables.Values)
            {
                wrapperContext.Add(saveable.CreateSnapshot());
            }
            _service.Write(new SaveWrapper(wrapperContext), _id);
        }

        public void Subscribe(ISaveable saveable)
        {
            _saveables.TryAdd(saveable.Key, saveable);
        }

        public void Unsubscribe(ISaveable saveable)
        {
            _saveables.Remove(saveable.Key);
        }

    }
}
