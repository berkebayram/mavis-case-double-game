namespace Main.Scripts.SaveSystem
{
    public interface ISaveSystem
    {
        public void SaveGame();
        public void LoadGame();
        public void Subscribe(ISaveable saveable);
        public void Unsubscribe(ISaveable saveable);
    }
}

