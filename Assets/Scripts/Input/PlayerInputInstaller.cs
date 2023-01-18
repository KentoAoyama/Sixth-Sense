using UnityEngine;
using Zenject;

public class PlayerInputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<IInputProvider>() // IInputProviderに対して
            .To<PlayerInputProvider>() // PlayerInputProviderのインスタンスを注入する
            .AsCached(); // 利用できるインスタンスがすでにあればそれで実行
    }
}