using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.DataAccessLayer
{
    public interface IDataRepository<T>
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(params T[] items);
        void Update(params T[] items);
        void Remove(params T[] items);
        void CallStoredProc(string name, params Tuple<string, string>[] parameters);
    }
}
