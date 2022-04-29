using Material.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Material.ViewModels
{
    public class FIFOViewModel : BindableBase
    {
        private readonly IDialogService dialog;
        private readonly MaterialFlowProvider materialFlowProvider = new MaterialFlowProvider();
        private List<FlowTable> tables = new List<FlowTable>();

        private readonly MaterialEntities db = new MaterialEntities();
        public List<FlowTable> Tables
        {
            get { return tables; }
            set
            {
                SetProperty(ref tables, value);
            }
        }
      
        public DelegateCommand AddMaterialCommand { get; }
        public DelegateCommand SubstarctMaterialCommand { get; }
        public DelegateCommand<FlowTable> RemoveCommand { get; }
        
        public int Sum { get; set; }
        
        public FIFOViewModel( IDialogService service)
        {
            
            dialog = service;
            Tables = materialFlowProvider.SelectAll();
            AddMaterialCommand = new DelegateCommand(OnAddMaterial);
            SubstarctMaterialCommand = new DelegateCommand(OnSubstractMaterial);
            RemoveCommand = new DelegateCommand<FlowTable>(OnRemove);
            Sum = Tables.Sum(r => r.Number);
         }
        private void OnAddMaterial()
        {
            dialog.ShowDialog("AddFlow", arg =>
            {
                if (arg.Result == ButtonResult.OK)
                {
                      var result = arg.Parameters.GetValue<FlowTable>("result");
                      
                      if (materialFlowProvider.Insert(result) > 0)
                        {
                            Tables = materialFlowProvider.SelectAll();
                        }
                        else
                        {
                            MessageBox.Show("新增失败", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    
                }
            });
          
        }
        private void OnSubstractMaterial()
        {

            dialog.Show("SubstractFlow", arg =>
            {
                if (arg.Result == ButtonResult.OK)
                {
                    var result = arg.Parameters.GetValue<FlowTable>("result");
                    if (materialFlowProvider.Insert(result) > 0)
                    {
                        Tables = materialFlowProvider.SelectAll();
                        
                    }
                    else
                    {
                        MessageBox.Show("出库失败", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("出库不符合实际情况", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
            );

        }
        private void OnRemove(FlowTable table)
        {
            if (MessageBox.Show("确定要删除该行数据吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {

                if (materialFlowProvider.Delete(table) > 0)
                {
                    MessageBox.Show("删除成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Tables = materialFlowProvider.SelectAll();
                }
                else
                {
                    MessageBox.Show("删除失败", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
        }

    }
}
