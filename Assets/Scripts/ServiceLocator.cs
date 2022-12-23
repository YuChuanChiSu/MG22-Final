using DG.Tweening;
using GenericToolKit.Common;
using GenericToolKit.DependencyInjection;
using GenericToolKit.Mvvm;
using GenericToolKit.Mvvm.Async;
using GenericToolKit.Mvvm.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;

public class ServiceLocator : SingletonAutoMono<ServiceLocator>
{
    public SavePointViewModel SavePoint
        => IocContainer.Instance.GetRequiredService<SavePointViewModel>();

    public HidePanelViewModel HidePanel
        => IocContainer.Instance.GetRequiredService<HidePanelViewModel>();

    public PausePanelViewModel PausePanel
        => IocContainer.Instance.GetRequiredService<PausePanelViewModel>();

    public AchievementCollection AchievementCollection
        => IocContainer.Instance.GetRequiredService<AchievementCollection>();

    public AchievementTipVM AchievementTipVM
        => IocContainer.Instance.GetRequiredService<AchievementTipVM>();

    private void Awake()
    {
        IServiceRegister register = IocContainer.BuildDefalutRegister();
        register.TryRegisterSingleton<ISyncDispatcher, SyncDispatcher>();
        register.TryRegisterSingleton<IJsonStorage, JsonStorage>();
        register.TryRegisterSingleton<SavePointViewModel>((IServiceProvider provider)
            => new SavePointViewModel((ISyncDispatcher)provider.GetService(typeof(ISyncDispatcher))), true);
        register.TryRegisterSingleton<HidePanelViewModel>((IServiceProvider provider)
            => new HidePanelViewModel(), true);
        register.TryRegisterSingleton<PausePanelViewModel>((IServiceProvider provider)
            => new PausePanelViewModel(), true);
        register.TryRegisterSingleton<AchievementCollection>((IServiceProvider provider)
            => new AchievementCollection((IJsonStorage)provider.GetService(typeof(IJsonStorage))), true);
        register.TryRegisterSingleton<AchievementTipVM>((IServiceProvider provider)
            => new AchievementTipVM(), true);

        IocContainer.ConfigureDefalutService(register);
    }

    protected override void OnDestroy()
    {
        AchievementCollection.OnApplicationQuit();
        base.OnDestroy();
        IocContainer.Instance.Dispose();
    }
}
