using Zenject;
using Main.Scripts.SceneSystem;

public class BootInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromComponentInHierarchy().AsSingle();
    }
}


