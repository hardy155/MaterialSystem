
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
    public class ManagementlViewModel:BindableBase
    {
        private readonly IDialogService dialog1;
        private readonly MaterialEntities db=new MaterialEntities();
        private readonly MaterialProvider materialProvider = new MaterialProvider();
        private List<MaterialTable> tables= new List<MaterialTable>();  
        public List<MaterialTable> Tables
        {
            get { return tables; }
            set { SetProperty(ref tables, value); }
        }
       
        public DelegateCommand AddCommand { get; }
        public DelegateCommand<MaterialTable> RemoveCommand { get;  }
        public DelegateCommand<MaterialTable> EditCommand { get;  }
        public DelegateCommand SearchCommand { get; }
        public ManagementlViewModel( IDialogService service)
        {
            
            Tables = materialProvider.SelectAll();
            dialog1=service;
            AddCommand = new DelegateCommand(OnAdd);
            EditCommand = new DelegateCommand<MaterialTable>(OnEdit);
            RemoveCommand=new DelegateCommand<MaterialTable>(OnRemove);
        
        }
        private void OnAdd()
        {
            dialog1.ShowDialog("AddMaterial", arg =>
            {
                if(arg.Result==ButtonResult.OK)
                {
                  var result= arg.Parameters.GetValue<MaterialTable>("result");     
                  var model= db.MaterialTable.FirstOrDefault(r => r.Name == result.Name);
                    if (model == null)
                    {
                        if (materialProvider.Insert(result) > 0)
                        {
                            Tables = materialProvider.SelectAll();
                        }
                    }
                    else
                    {
                        MessageBox.Show("新增失败", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                }
            });
        }
        private void OnRemove(MaterialTable model)
        {
            
            if (MessageBox.Show("确定要删除该行数据吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
               
                    if (materialProvider.Delete(model) > 0)
                    {
                        MessageBox.Show("删除成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        Tables = materialProvider.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("删除失败", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                

            }
        }
        private void OnEdit(MaterialTable model)
        {
            DialogParameters key = new DialogParameters();
            key.Add("editmodel", model);
            dialog1.ShowDialog("EditMaterial", key, arg => {
                if (arg.Result == ButtonResult.OK)
                {
                    var result = arg.Parameters.GetValue<MaterialTable>("result");
                    var model1 = db.MaterialTable.FirstOrDefault(r => r.Name == result.Name);
                    if(model1!=null)
                    {
                        if (materialProvider.Update(result) > 0)
                        {
                            Tables = materialProvider.SelectAll();
                        }

                    }

                }


            });
        }
    }
}
