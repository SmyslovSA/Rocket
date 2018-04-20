using System;
using System.Collections.Generic;

namespace Rocket.BL.Common.Models.UserRoles
{
    public class UserPermission
    {
        private readonly Dictionary<int, string> _permission = new Dictionary<int, string>();

        public bool Add(int dKey, string dValue)
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

        public bool Remove(int dKey)
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
                // ReSharper disable once InvertIf
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