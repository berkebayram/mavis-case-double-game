namespace Main.Scripts.SaveSystem
{
    public interface ISaveData
    {
        public string Serialize();
        public void Deserialize(string data);
    }
}
