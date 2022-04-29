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
    public class SubstractFlowViewModel : BindableBase, IDialogAware
    {
        private List<MaterialTable> tables= new List<MaterialTable>();
        private FlowTable flow = new FlowTable();
        public List<MaterialTable> Tables{ get { return tables; } set { SetProperty(ref tables, value); } }
       
        public FlowTable Flow { get { return flow; } set { SetProperty(ref flow, value); } }

        public DelegateCommand ComfirmCommand { get; }
        public DelegateCommand CancelCommand { get; }
        
        public SubstractFlowViewModel()
        {
            Tables = new MaterialProvider().SelectAll();   
            ComfirmCommand = new DelegateCommand(OnConfirm);
            CancelCommand = new DelegateCommand(OnCancel);
        }
        private void OnConfirm()
        {
            if (!string.IsNullOrEmpty(Flow.MaterialName))
            {
                if (Flow.Number < 0)
                {
                    Flow.InsertDate = DateTime.Now;
                    DialogParameters key = new DialogParameters
                    {
                        { "result", Flow }
                    };
                    RequestClose.Invoke(new DialogResult(ButtonResult.OK, key));
                    Flow = new FlowTable();
                }
                else
                {
                    MessageBox.Show("不符合实际", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);

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
        public string Title => "出库";

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
