using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Main.Scripts.SceneSystem
{
    [CreateAssetMenu(menuName = "Game/SceneData")]
    public class SceneData : ScriptableObject
    {
        public string Id;
        public AssetReference SceneReference;
    }
}
