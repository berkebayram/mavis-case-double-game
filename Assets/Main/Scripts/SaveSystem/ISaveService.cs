namespace Main.Scripts.SaveSystem
{
    public interface ISaveService 
    {
        public void Write(SaveWrapper wrapper, string path);
        public SaveWrapper Read(string path);
    }
}
