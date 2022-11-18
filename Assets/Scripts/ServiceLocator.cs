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
using UnityEngine;

public class ServiceLocator : SingletonAutoMono<ServiceLocator>
{
    public SavePointViewModel SavePoint
        => IocContainer.Instance.GetRequiredService<SavePointViewModel>();

    public HidePanelViewModel HidePanel
        => IocContainer.Instance.GetRequiredService<HidePanelViewModel>();

    private void Awake()
    {
        IServiceRegister register = IocContainer.BuildDefalutRegister();
        register.TryRegisterSingleton<ISyncDispatcher, SyncDispatcher>();
        register.TryRegisterSingleton<SavePointViewModel>((IServiceProvider provider)
            => new SavePointViewModel((ISyncDispatcher)provider.GetService(typeof(ISyncDispatcher))), true);
        register.TryRegisterSingleton<HidePanelViewModel>((IServiceProvider provider)
            => new HidePanelViewModel(), true);

        IocContainer.ConfigureDefalutService(register);
    }

    private void OnDestroy()
    {
        IocContainer.Instance.Dispose();
    }
}
