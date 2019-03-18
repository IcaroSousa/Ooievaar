using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Ooievaar.Common.Interfaces;
using StackExchange.Redis;

namespace Ooievaar.Common.Classes
{
    public class RedisQueue : IRedisQueue
    {
        private readonly string _namespace;
        private readonly string _queueName;

        private string _key => string.Format($"{_namespace}:{_queueName}");

        private IDatabase _redis { get; set; }

        protected bool IsEmpty => _redis.ListLength(_key) == 0;
        protected long QueueSize => _redis.ListLength(_key);

        public RedisQueue(IConfiguration pConfig)
        {
            _namespace = pConfig["redis_namespace"];
            _queueName = pConfig["redis_queuename"];

            _redis = ConnectionMultiplexer
            .Connect($"{pConfig["redis_host"]}:{pConfig["redis_port"]}")
            .GetDatabase();

            Console.WriteLine($"Redis server -> {pConfig["redisHost"]}:{pConfig["redisPort"]}");
            Console.WriteLine($"Redis key -> {_key}");
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
