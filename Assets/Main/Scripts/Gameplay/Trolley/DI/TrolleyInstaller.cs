using Main.Containers;
using Main.Gameplay;
using Main.Scripts.SaveSystem;
using UnityEngine;
using Zenject;

public class TrolleyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameLevel>().AsSingle();
        Container.Bind<PlayerHealth>().AsSingle();
        Container.Bind<ProductFallManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ProductManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShelfManagerSettings>().AsSingle().WithArguments(3, 4, new Vector2(0, 3), new Vector2(0, -2));
        Container.Bind<SaveSystem>().AsSingle().WithArguments(
                "Trolley",
                    new PlayerPrefSaveService()
                );
        Container.Bind<MaxScoreHolder>().AsSingle().NonLazy();
        Container.Bind<PlayerEconomy>().AsSingle().NonLazy();
        Container.Bind<IGameLogic>().To<TrolleyLogic>().AsSingle();
    }
}
