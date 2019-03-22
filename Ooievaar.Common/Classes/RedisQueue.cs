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
        private readonly string _queueName;
        private IDatabase _redis { get; set; }

        protected bool IsEmpty => _redis.ListLength(_queueName) == 0;
        protected long QueueSize => _redis.ListLength(_queueName);

        public RedisQueue(IConfiguration pConfig)
        {
            _queueName = pConfig["redis_queuename"];

            _redis = ConnectionMultiplexer
            .Connect($"{pConfig["redis_host"]}:{pConfig["redis_port"]}")
            .GetDatabase();

            Console.WriteLine($"Redis server -> {pConfig["redis_host"]}:{pConfig["redis_port"]}");
            Console.WriteLine($"Redis key -> {_queueName}");
        }

        public void Enqueue(string pValue)
        {
            _redis.ListRightPush(_queueName, pValue);
        }

        public string Dequeue()
        {
            if (IsEmpty)
                return string.Empty;

            return _redis.ListLeftPop(_queueName);
        }

        public IList<string> GetListFromQueue()
        {
            return _redis.ListRange(_queueName, 0, QueueSize).ToStringArray().ToList();
        }

        public void DeleteItem(string pValue)
        {
            _redis.ListRemove(_queueName, pValue);
        }
    }
}
