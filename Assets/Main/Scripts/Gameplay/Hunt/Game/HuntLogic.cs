using Main.Scripts.Events;
using Main.Scripts.EventSystem;

using Main.Containers;
using UnityEngine;
using Main.Scripts.SaveSystem;

public class HuntLogic : IGameLogic
{
    private readonly int _needed;
    private readonly BaloonLevel[] _types;
    private int _index = 0;
    private Aim _aim;
    private PlayerEconomy _economy;
    private SaveSystem _saveSystem;

    public HuntLogic(int needed, BaloonLevel[] baloonType, Aim aim, PlayerEconomy economy, SaveSystem saveSystem)
    {
        _needed = needed;
        _types = baloonType;
        _aim = aim;
        _economy = economy;
        _saveSystem = saveSystem;
    }

    public void Setup()
    {
        _economy.Subscribe(HandleMoneyChange);
        Dispatcher.Subscribe<BaloonPopEvent>(HandlePop);
        Dispatcher.Subscribe<GameRestartEvent>(HandleRestart);
        Dispatcher.Subscribe<GameSuccessEvent>(HandleSuccess);
        Dispatcher.Subscribe<GameFailEvent>(HandleFail);
        _saveSystem.LoadGame();
    }

    private void HandleFail(GameFailEvent @event)
    {
        _aim.Hide();
        _aim.transform.position = Vector3.down * 4f;
    }

    private void HandleSuccess(GameSuccessEvent @event)
    {
        _aim.Hide();
        _aim.transform.position = Vector3.down * 4f;
    }

    public void Dispose()
    {
        _economy.Unsubscribe(HandleMoneyChange);
        Dispatcher.Unsubscribe<BaloonPopEvent>(HandlePop);
        Dispatcher.Unsubscribe<GameRestartEvent>(HandleRestart);
        Dispatcher.Unsubscribe<GameSuccessEvent>(HandleSuccess);
        Dispatcher.Unsubscribe<GameFailEvent>(HandleFail);
    }

    private void HandleRestart(GameRestartEvent @event)
    {
        _index = 0;
    }

    private void HandlePop(BaloonPopEvent @event)
    {
        if (_index >= _types.Length)
        {
            Dispatcher.Dispatch<GameSuccessEvent>(new GameSuccessEvent());
            return;
        }

        Dispatcher.Dispatch(new SpawnBaloonEvent() { BaloonLevel = _types[_index] });
        _index++;
    }

    void HandleMoneyChange(int newValue)
    {
        if (newValue >= _needed)
        {
            Dispatcher.Dispatch<GameSuccessEvent>(new GameSuccessEvent());
            return;
        }
    }
}
