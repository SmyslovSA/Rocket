using System;
using System.Collections.Generic;

namespace Rocket.BL.Common.Models.UserRoles
{
    public class RolePermission
    {
        /// <summary>
        /// dictionary stores the Key of permission identifier and some Value description
        /// </summary>
        private readonly Dictionary<ushort, string> _permission = new Dictionary<ushort, string>();

        public bool Add(ushort dKey, string dValue)
        {
            if (KeyExists(dKey))
            {
                Console.WriteLine($"This key allready exists: {dKey}");
                return false;
            }

            if (string.IsNullOrEmpty(dValue))
            {
                Console.WriteLine($"Value string is empty for key: {dKey}");
                return false;
            }

            if (RoleExists(dValue))
                return false;

            _permission.Add(dKey, dValue);
            return true;
        }

        public bool Remove(ushort dKey)
        {
            if (!KeyExists(dKey))
            {
                Console.WriteLine($"This key not exists: {dKey}");
                return false;
            }

            _permission.Remove(dKey);
            return true; 
        }

        public bool RoleExists(string dValue)
        {
            foreach (var pair in _permission)
            {
                if (string.Equals(pair.Value, dValue, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Role: {dValue} exist in id: {pair.Key}");
                    return true;
                }
            }
            return false;
        }

        private bool KeyExists(int dKey)
        {
            foreach (var pair in _permission)
            {
                if (pair.Key == dKey)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
