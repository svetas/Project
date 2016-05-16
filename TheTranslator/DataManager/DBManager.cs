using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.DataManager
{
    public class DBManager
    {
        private ConnectionMultiplexer redis;


        public DBManager()
        {
            
            redis = ConnectionMultiplexer.Connect("localhost,allowAdmin=true");

        }
        public void Reset()
        {
            var endpoints = redis.GetEndPoints();            
            var server = redis.GetServer(endpoints.First());
            server.FlushDatabase();
        }

        public bool WriteData(string key)
        {
            IDatabase db = redis.GetDatabase();
            string keyStr = key;
            db.StringIncrement(keyStr, 1);
            return true;            
        }

        public bool WriteSet(string key, string value)
        {
            IDatabase db = redis.GetDatabase();
            db.HashIncrement("k"+key, "k"+value, 1);
            return true;
        }

        public IEnumerable<HashEntry> GetAllValues(string key)
        {
            IDatabase db = redis.GetDatabase();
            return db.HashGetAll("k"+key);
        }

        public HashEntry[] GetSet(string key)
        {
            IDatabase db = redis.GetDatabase();
            HashEntry[] values = db.HashGetAll("k" + key);
            return values;
        }
        public bool CheckGet(string table, string key)
        {
            IDatabase db = redis.GetDatabase();
            return db.HashExists("k" + table, "k" + key);
        }
    }
}
