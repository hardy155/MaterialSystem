using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Material.Model
{
    public interface IProvider<T> where T:class
    {
        List<T> SelectAll();
        int Insert(T t);
        int Update(T t);
        int Delete(T t);
    }
}
