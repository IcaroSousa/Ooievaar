using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Ooievaar.Common.Interfaces;
using StackExchange.Redis;

namespace Ooievaar.Common.Classes
{
    public class RedisQueue : IRedisQueue
    {
        private readonly string _nameSpace;
        private readonly string _queueName;

        private string _key => string.Format($"{_nameSpace}:{_queueName}");

        private IDatabase _redis { get; set; }

        protected bool IsEmpty => _redis.ListLength(_key) == 0;
        protected long QueueSize => _redis.ListLength(_key);

        public RedisQueue(IConfiguration pConfig)
        {        
            _nameSpace = pConfig["redisNameSpace"];
            _queueName = pConfig["redisQueueName"];

            _redis = ConnectionMultiplexer
            .Connect($"{pConfig["redisHost"]}:{pConfig["redisPort"]}")
            .GetDatabase();
        }

        public void Enqueue(string pValue)
        {
            _redis.ListRightPush(_key, pValue);
        }

        public string Dequeue()
        {
            if (IsEmpty)
                return string.Empty;

            return _redis.ListLeftPop(_key);
        }

        public IList<string> GetListFromQueue()
        {
            return _redis.ListRange(_key, 0, QueueSize).ToStringArray().ToList();
        }

        public void DeleteItem(string pValue)
        {
            _redis.ListRemove(_key, pValue);
        }
    }
}
