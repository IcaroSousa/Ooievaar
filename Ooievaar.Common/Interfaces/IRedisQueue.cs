using System.Collections.Generic;

namespace Ooievaar.Common.Interfaces
{
    public interface IRedisQueue
    {
        string Dequeue();
        void Enqueue(string pValue);
        void DeleteItem(string pValue);
        IList<string> GetListFromQueue();
    }
}