namespace Main.Scripts.SaveSystem
{
    public interface ISaveable
    {
        public string Key { get; }
        public SaveContainer CreateSnapshot();
        public void Load(string data);
    }
}
