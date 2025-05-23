using System.Collections.Generic;
using Main.Containers;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

public class TrolleyLogic : IGameLogic
{

    private GameLevel _level;
    private PlayerHealth _health;
    public TrolleyLogic(GameLevel level, PlayerHealth health)
    {
        _level = level;
        _health = health;
        _level.Setup(new(){
                CreateLevel(0,.6f,5),
                CreateLevel(1,.4f,10),
                CreateLevel(2,.2f,20),
            }); // Can be given from level manager or di
    }
    public void Setup()
    {
        Dispatcher.Subscribe<NoProductEvent>(HandleNoProduct);
    }

    ChangeLevelEvent CreateLevel(int level, float cd, int prize)
    {
        return new ChangeLevelEvent()
        {
            level = level,
            cooldown = cd,
            productPrize = prize,
        };
    }

    public void Dispose()
    {
        Dispatcher.Unsubscribe<NoProductEvent>(HandleNoProduct);
    }

    private void HandleNoProduct(NoProductEvent @event)
    {
        if (_level.HasLevel() && _health.Current > 0)
        {
            _level.LevelUp();
            return;
        }

        Dispatcher.Dispatch<GameSuccessEvent>(new GameSuccessEvent());
    }
}
