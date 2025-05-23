using System.Threading.Tasks;

namespace Main.Scripts.SceneSystem
{
    public interface ISceneLoader 
    {
        Task LoadSceneAsync(string sceneId);
        Task UnloadCurrentSceneAsync();
    }
}
