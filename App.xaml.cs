using Material.ViewModels;
using Material.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Material
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Home, HomeViewModel>();
            containerRegistry.RegisterForNavigation<Management, ManagementlViewModel>();
            containerRegistry.RegisterForNavigation<FIFO, FIFOViewModel>();
            containerRegistry.RegisterDialog<AddFlow,AddFlowViewModel>();
            containerRegistry.RegisterDialog<AddMaterial, AddMaterialViewModel>();
            containerRegistry.RegisterDialog<EditMaterial,EditMaterialViewModel>();
            containerRegistry.RegisterDialog<SubstractFlow, SubstractFlowViewModel>();
        }
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<MainWindow, MainViewModel>();
        }

    }
}
