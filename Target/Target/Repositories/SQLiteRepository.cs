using Akavache;
using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive;
using Newtonsoft.Json;
using Plugin.GoogleAnalytics;

namespace Target.Repositories
{
    public class SQLiteRepository : ISQLiteRepository
    {
        public async Task<Unit> Create<T>(string name, T obj)
        {
            BlobCache.ApplicationName = Constants.AppName;
            Unit returnval;
            try
            {
                returnval = await BlobCache.UserAccount.InsertObject(name, obj);
            }
            catch (KeyNotFoundException ex)
            {
                GoogleAnalytics.Current.Tracker.SendException(ex.Message, false);
                returnval = Unit.Default;
            }
            catch (Exception e)
            {
                GoogleAnalytics.Current.Tracker.SendException(e.Message, false);
                returnval = Unit.Default;
            }
            return returnval;
        }
        public async Task<T> Get<T>(string name)
        {
            BlobCache.ApplicationName = Constants.AppName;
            //T returnval;
            //try
            //{
            //    returnval = (T)await BlobCache.UserAccount.GetObject<T>(name);
            //}
            //catch (KeyNotFoundException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //returnval = (T)await BlobCache.UserAccount.GetObject<T>(name);
            //return returnval;
            return await BlobCache.UserAccount.GetObject<T>(name);
        }
        //public async Task<bool> Delete()
        //{

        //}
        //public async Task<bool> Update()
        //{

        //}
        public async Task<IEnumerable<T>> GetAll<T>()
        {
            BlobCache.ApplicationName = Constants.AppName;
            //IEnumerable<T> returnval;
            //try
            //{
            //    returnval = await BlobCache.UserAccount.GetAllObjects<T>();
            //}
            //catch (KeyNotFoundException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //catch(JsonSerializationException je)
            //{
            //    Console.WriteLine(je.Message);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //returnval = await BlobCache.UserAccount.GetAllObjects<T>();
            //return returnval;
            return await BlobCache.UserAccount.GetAllObjects<T>();
        }
    }
}
