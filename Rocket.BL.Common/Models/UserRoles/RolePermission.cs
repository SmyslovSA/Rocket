using System.Collections.Concurrent;

namespace Rocket.BL.Common.Models.UserRoles
{
    public class RolePermission
    {
        /// <summary>
        /// dictionary stores the Key of permission identifier and some Value description
        /// </summary>
        private readonly ConcurrentDictionary<ushort, string> _permission = new ConcurrentDictionary<ushort, string>();

        /// <summary>
        /// we adding new permission
        /// </summary>
        /// <param name="dKey"></param>
        /// <param name="dValue"></param>
        public void Add(ushort dKey, string dValue)
        {
            if (!string.IsNullOrEmpty(dValue) && !_permission.ContainsKey(dKey))
                _permission.TryAdd(dKey, dValue);
        }

        /// <summary>
        /// here is deleting some permission by key
        /// </summary>
        /// <param name="dKey"></param>
        /// <returns></returns>
        public string Remove(ushort dKey)
        {
            if (!_permission.ContainsKey(dKey))
                return string.Empty;

            _permission.TryRemove(dKey, out var value);
            return value;
        }

        /// <summary>
        /// updating description of permission
        /// </summary>
        /// <param name="dKey"></param>
        /// <param name="dValue"></param>
        /// <param name="newDesctiption"></param>
        public void Update(ushort dKey, string dValue, string newDesctiption)
        {
            if (!string.IsNullOrEmpty(newDesctiption) && _permission.ContainsKey(dKey))
                _permission.TryUpdate(dKey, newDesctiption, dValue);
        }
    }
}
