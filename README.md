# Modular Mini-Game Framework – Unity Project

## Project Version

- Unity Version: `2022.3.35f1`
- Dev Env: `Mac v15.3 & neovim & DotNET LSP`

## Architecture Overview

### 1. Save System

The save system is built around a central `SaveSystem` and consists of modules implementing `ISaveable`. Each `ISaveable` registers itself to the `SaveSystem` at scene start.

- Dependency Injection (DI) is used for system initialization.
- Each mini-game maintains its own save data.
- Supports multiple save backends (file-based, PlayerPrefs).

**Example:**

```csharp
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


var saveSystem = new SaveSystem("TestGame", new JsonSaveService(""));
var mockUser = new User(new UserData() { Username = "Sanar" }, "berke");
saveSystem.Subscribe(mockUser);
saveSystem.SaveGame();

mockUser.Info.Username = "ChangedUsername";
saveSystem.LoadGame();
```

### 2. Event System

The event system is a Dispatcher based on the Event Bus pattern using Observer principles.

- `Dispatcher.Subscribe<T>()`, `Dispatcher.Unsubscribe<T>()`, and `Dispatcher.Dispatch()` are used.
- `Dispatcher.Clear()` removes all listeners.

**Example:**

```csharp
float firstSubject = 0f;
Action<float> setAction = (val) => {
	firstSubject = val;
};
Dispatcher.Subscribe<FloatTestEvent>(setAction);
Dispatcher.Dispatch(new FloatTestEvent() { Value = 5f }); // firstSubject = 5f
Dispatcher.Unsubscribe<FloatTestEvent>(setAction);
Dispatcher.Dispatch(new FloatTestEvent() { Value = 0f }); // firstSubject = 5f
Dispatcher.Clear(); // Clears All Subscribers
```

### 3. Scene Management System

- Scenes are identified via unique string IDs and structured using ScriptableObjects.
- Uses Unity Addressables for dynamic loading.
- Built on C# `Task` for async scene loading with loading progression support.

**Usage:**

```csharp
await SceneLoader.LoadSceneAsync("YourSceneId"); // if it does not exist, stays silent
SceneLoader.GoMain(); // goes main scene synchronously
```

### 4. Mini-Game Framework

- Built-in common events like `GameSuccess`, `GameFail`, etc.
- Includes standard classes like `PlayerHealth`.
- Uses `IGameLogic` for individual game behavior with `Setup()` and `Dispose()` methods.

## Developer Notes

1. **Custom Animation System**: A lightweight tweening system (similar to DOTween) was implemented internally to avoid using external tools. Concurrency handling could be improved.

2. **Event System Choices**: Opted for a type-based Dispatcher over UnityEvent or ScriptableObject approaches due to time and design constraints.

3. **Save System**: The current save format is JSON with support for PlayerPrefs. For large-scale games, binary serialization, versioning, and encryption would be necessary. 

4. **Mini-Game Art**: Visual assets generated with Stable Diffusion and enhanced using tween animations.

5. **Scene Management**: Task-based loading is not compatible with WebGL and Chromium-based environments. Coroutine-based fallbacks recommended for those platforms. Lacking of content availability check due to the time limitation.

6. **Level Editor Tool**: Planned to create an editor tool for the Hunt game (to define paths for balloons), but it wasn't completed due to time constraints.

7. **Unit Tests**: All core systems have unit tests. `SceneSystem` tests are limited due to the time complexity of scene bootstrapping in test environments.

8. **Dependency Injection**: Utilized Zenject for DI. Avoided Zenject Signals and used custom Delegate Containers for features like `PlayerHealth` and `PlayerEconomy`.

## Features

-  Two mini-games: Drag&Drop Hitting Targets, Product Catching Kart Game
-  Save/Load system
-  Event-driven architecture
-  Custom lightweight tweening animation system
-  Unit tests for core systems
-  Addressables-based scene management
-  Extensible mini-game logic

## Installation

1. Clone this repository.
2. Open the project in Unity `2022.3.35f1`.
3. Open and run the `Main` scene.
Optional ( UI is designer for the resolution of `1080 x 1920` )

## License

This project is intended for demonstration.

