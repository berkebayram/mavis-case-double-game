using NUnit.Framework;
using UnityEngine;
using System;
using System.IO;

namespace Main.Scripts.SaveSystem.Tests
{
    public class FunctionalityTest
    {
        [Serializable]
        private class User : ISaveable
        {
            public string Key { get; private set; }
            public UserData Info;

            public User(UserData userData, string key)
            {
                Key = key;
                Info = userData;
            }

            public SaveContainer CreateSnapshot()
            {
                return new SaveContainer(Key, Info.Serialize());
            }

            public void Load(string data)
            {
                Info ??= new UserData();
                Info.Deserialize(data);
            }
        }

        [Serializable]
        private class UserData : ISaveData
        {
            public string Username;
            public void Deserialize(string data)
            {
                var val = JsonUtility.FromJson<UserData>(data);
                Username = val.Username;
            }

            public string Serialize()
            {
                return JsonUtility.ToJson(this);
            }
        }

        [Test]
        public void JsonLoaderTest()
        {
            var saveSystem = new SaveSystem("TestGame", new JsonSaveService(""));
            var mockUser = new User(new UserData() { Username = "Sanar" }, "berke");

            saveSystem.Subscribe(mockUser);
            saveSystem.SaveGame();

            mockUser.Info.Username = "ChangedUsername";
            saveSystem.LoadGame();

            Assert.IsTrue(File.Exists(Path.Combine("", "TestGame.json")));
            Assert.AreEqual("Sanar", mockUser.Info.Username);
        }

        [Test]
        public void PrefLoaderTest()
        {
            var saveSystem = new SaveSystem(
                    "TestGame",
                    new PlayerPrefSaveService()
                    );
            var mockUser = new User(new UserData() { Username = "Sanar" }, "berke");

            saveSystem.Subscribe(mockUser);
            saveSystem.SaveGame();

            mockUser.Info.Username = "ChangedUsername";

            saveSystem.LoadGame();

            Assert.AreEqual("Sanar", mockUser.Info.Username);
        }

        [TearDown]
        public void CleanUp()
        {
            PlayerPrefs.SetString("TestGame", "");

            var path = Path.Combine("", "TestGame.json");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
