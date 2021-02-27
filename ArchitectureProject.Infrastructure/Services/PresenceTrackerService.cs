using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureProject.Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class PresenceTrackerService
    {
        private readonly ConcurrentDictionary<string,List<string>> _connectionsByUserId = 
            new ConcurrentDictionary<string, List<string>>();

        public Task AddConnectionAsync(string userId, string connectionId)
        {
            if (this._connectionsByUserId.TryGetValue(userId, out var connections))
            {
                if (!connections.Contains(connectionId))
                {
                    var value = new List<string>(connections) {connectionId};
                    this._connectionsByUserId.TryUpdate(userId, value, connections);
                }
            }
            else
            {
                this._connectionsByUserId.TryAdd(userId, new List<string>() { connectionId });
            }

            return Task.CompletedTask;
        }

        public Task RemoveConnectionAsync(string userId, string connectionId)
        {
            if (!this._connectionsByUserId.ContainsKey(userId)) return Task.CompletedTask;

            this._connectionsByUserId[userId].Remove(connectionId);

            if (!this._connectionsByUserId[userId].Any())
            {
                this._connectionsByUserId.TryRemove(userId, out var _ );
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> GetAllAvailableUsersAsync()
        {
            var result =  this._connectionsByUserId.Select(x => x.Key);

            return Task.FromResult(result);
        }
    }
}
