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
    public class EditMaterialViewModel : BindableBase, IDialogAware
    {
        private MaterialTable material=new MaterialTable();
        public MaterialTable Material { get { return material; } set { SetProperty(ref material, value); } }
        public DelegateCommand ConfirmCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public EditMaterialViewModel()
        {
            ConfirmCommand = new DelegateCommand(OnConfirm);
            CancelCommand = new DelegateCommand(OnCancel);
        }
        private void OnConfirm()
        {
            if (!string.IsNullOrEmpty(Material.Name))
            {
                if (Material.Price > 0)
                {
                    Material.InsertDate = DateTime.Now;
                    DialogParameters keys = new DialogParameters() { { "result", Material } };
                    RequestClose.Invoke(new DialogResult(ButtonResult.OK, keys));
                    

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
        private void OnCancel()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
        }
        public string Title => "编辑材料";

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
            Material = parameters.GetValue<MaterialTable>("editmodel");
        }
    }
}
