using Material.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Material.ViewModels
{
    public class MainViewModel:BindableBase
    {
        private readonly IRegionManager regionManager;
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public MainViewModel(IRegionManager  region )
        {
            regionManager = region;
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(Home));
            NavigateCommand = new DelegateCommand<string>(NavigatePath);
        }
        private void NavigatePath(string navigatePath)
        {
            if(navigatePath!=null)
            {
                regionManager.RequestNavigate("ContentRegion", navigatePath);
            }
        }
    }
}
