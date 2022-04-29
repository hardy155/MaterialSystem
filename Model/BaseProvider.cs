using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Material.Model
{
   public class BaseProvider<T> : IProvider<T> where T : class
    {
        public MaterialEntities DB=new MaterialEntities();
        public int Delete(T t)
        {
            DB.Set<T>().Remove(t);
            return DB.SaveChanges();
        }

        public int Insert(T t)
        {
            DB.Set<T>().Add(t);
            return DB.SaveChanges();
        }

        public List<T> SelectAll()
        {
            var list=DB.Set<T>().ToList();
            return list;
        }

        public List<T> SelectAll(Expression<Func<T,bool>> experssion)
        {
            var list = DB.Set<T>().Where(experssion).ToList();
            return list;
        }

        public int Update(T t)
        {
            DB.Entry<T>(t).State = System.Data.Entity.EntityState.Modified;
            return DB.SaveChanges();
        }
    }
}
