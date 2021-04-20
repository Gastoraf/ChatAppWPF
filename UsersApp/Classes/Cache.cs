using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace UsersApp
{
    class Cache
    {

        public string  GetValue(string login)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(login) as string;
        }

        public bool Add(string login)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(login.ToString(), login, DateTime.Now.AddDays(1));
        }

        public void Update(string login)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(login.ToString(), login, DateTime.Now.AddDays(1));
        }

        public void Delete(string login)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(login.ToString()))
            {
                memoryCache.Remove(login.ToString());
            }
        }
    }
}
