using Material.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Material.ViewModels
{
    public class AddMaterialViewModel :BindableBase ,IDialogAware
    {
        private readonly MaterialProvider materialProvider=new MaterialProvider();
        private MaterialTable material=new MaterialTable();
        public MaterialTable Kind
        {
            get { return material; }
            set { SetProperty(ref material, value); }
        }
        
        public DelegateCommand ComfirmCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public AddMaterialViewModel()
        {
            CancelCommand = new DelegateCommand(OnCancelCommand);
            ComfirmCommand = new DelegateCommand(OnConfirmCommand); 
        }
        private void OnConfirmCommand()
        {
            if (!string.IsNullOrEmpty(Kind.Name) )
            {
                if(Kind.Price>0)
                {
                    Kind.InsertDate = DateTime.Now;
                    DialogParameters keys = new DialogParameters() ;
                    keys.Add("result",Kind);
                    RequestClose.Invoke(new DialogResult(ButtonResult.OK, keys));
                    Kind = new MaterialTable();

                }
                else
                {
                    MessageBox.Show("价格不符合实际", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
            else
            {
                MessageBox.Show("数据为空", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private void OnCancelCommand()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
        }
        public string Title => "添加材料";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
