using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IInMemoryCache
    {
        List<T> GetCache<T>(string key);
        void SetCache<T>(string key, List<T> value);
    }
}
