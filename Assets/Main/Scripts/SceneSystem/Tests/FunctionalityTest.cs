using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Main.Scripts.SceneSystem.Tests
{
    public class FunctionalityTest
    {
        private class TestSceneLoader : ISceneLoader
        {
            public string SceneId;

            public Task LoadSceneAsync(string sceneId)
            {
                SceneId = sceneId;
                return Task.CompletedTask;
            }

            public Task UnloadCurrentSceneAsync()
            {
                SceneId = "";
                return Task.CompletedTask;
            }
        }

        private ISceneLoader _sceneLoader;

        [SetUp]
        public void Setup()
        {
            _sceneLoader = new TestSceneLoader();
        }

        private IEnumerator AsCoroutine(Task task)
        {
            while (!task.IsCompleted) yield return null;
            // if task is faulted, throws the exception
            task.GetAwaiter().GetResult();
        }

        [UnityTest]
        public IEnumerator LoadTest()
        {
            yield return AsCoroutine(_sceneLoader.LoadSceneAsync("trolley"));
            var mock = (TestSceneLoader)_sceneLoader;
            Assert.AreEqual("trolley", mock.SceneId);
            yield return null;
        }

        [UnityTest]
        public IEnumerator UnloadTest()
        {
            yield return AsCoroutine(_sceneLoader.LoadSceneAsync("trolley"));
            yield return AsCoroutine(_sceneLoader.UnloadCurrentSceneAsync());
            var mock = (TestSceneLoader)_sceneLoader;
            Assert.IsTrue(string.IsNullOrWhiteSpace(mock.SceneId));
        }

        [UnityTest]
        public IEnumerator LoadAndUnloadMultiple()
        {
            yield return AsCoroutine(_sceneLoader.LoadSceneAsync("trolley"));
            yield return AsCoroutine(_sceneLoader.UnloadCurrentSceneAsync());
            yield return AsCoroutine(_sceneLoader.LoadSceneAsync("duck-hunt"));

            var mock = (TestSceneLoader)_sceneLoader;
            Assert.AreEqual("duck-hunt", mock.SceneId);
        }
    }
}
