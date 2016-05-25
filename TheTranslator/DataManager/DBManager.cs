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

        private static DBManager instance; 

        private static Lazy<ConfigurationOptions> configOptions
                = new Lazy<ConfigurationOptions>(() =>
                {
                var configOptions = new ConfigurationOptions();
                configOptions.EndPoints.Add("localhost");
                configOptions.ClientName = "LeakyRedisConnection";
                    configOptions.AllowAdmin = true;
                configOptions.ConnectTimeout = 1000000;
                configOptions.SyncTimeout = 1000000;
                return configOptions;
                });

        public static DBManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DBManager();
            }
            return instance;
        }
        private DBManager()
        {
            redis = ConnectionMultiplexer.Connect(configOptions.Value);
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
            int co = (int)db.HashLength("k" + key);
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
        public RedisValue GetSet(string table, string key)
        {
            IDatabase db = redis.GetDatabase();
            return db.HashGet("k" + table, "k" + key);
        }
    }
}
