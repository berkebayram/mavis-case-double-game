using Main.Containers;
using Main.Scripts.SaveSystem;
using Zenject;

public class HuntInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Aim>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FtueHand>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BaloonFactory>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SaveSystem>().AsSingle().WithArguments(
                "Hunt",
                    new PlayerPrefSaveService()
                );

        Container.Bind<PlayerEconomy>().AsSingle().NonLazy();
        Container.Bind<MaxScoreHolder>().AsSingle().NonLazy();

        Container.Bind<IGameLogic>().To<HuntLogic>().AsSingle()
            .WithArguments(
                    50,
                    new BaloonLevel[]{
                            BaloonLevel.Easy,
                            BaloonLevel.Medium,
                            BaloonLevel.Hard,
                        }
                    );
    }
}
