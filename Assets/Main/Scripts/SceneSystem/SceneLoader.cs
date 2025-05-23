using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Main.Scripts.SceneSystem
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private List<SceneData> data;
        private string _sceneId;
        private AsyncOperationHandle<SceneInstance> _sceneHandle;

        public async void GoMain()
        {
            SceneManager.LoadScene(0);

            if (!string.IsNullOrWhiteSpace(_sceneId))
                await UnloadCurrentSceneAsync();
        }

        public async Task LoadSceneAsync(string sceneId)
        {
            if (sceneId == _sceneId)
                return;

            if (!string.IsNullOrWhiteSpace(_sceneId))
            {
                await UnloadCurrentSceneAsync();
            }

            var sceneData = Find(sceneId);
            if (sceneData == null)
                throw new UnityException($"Scene ID:{sceneId} Not Found");

            _sceneHandle = Addressables.LoadSceneAsync(sceneData.SceneReference);
            await _sceneHandle.Task;

            _sceneId = sceneId;
        }

        public async Task UnloadCurrentSceneAsync()
        {
            await Addressables.UnloadSceneAsync(_sceneHandle).Task;
            _sceneId = "";
        }

        private SceneData Find(string id)
        {
            foreach (var sceneData in data)
            {
                if (sceneData.Id != id)
                    continue;
                return sceneData;
            }
            return null;
        }
    }
}
