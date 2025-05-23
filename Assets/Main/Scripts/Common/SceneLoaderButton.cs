using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Main.Scripts.SceneSystem;

public class SceneLoaderButton : MonoBehaviour
{
    [SerializeField] private string sceneId;
    [SerializeField] private Button btn;
    [Inject] private SceneLoader loader;

    void Start()
    {
        btn.onClick.AddListener(HandleClick);
    }

    async void HandleClick()
    {
        await loader.LoadSceneAsync(sceneId);
    }
}
