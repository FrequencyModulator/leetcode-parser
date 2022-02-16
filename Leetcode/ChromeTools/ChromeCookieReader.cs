using Dapper;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using static ChromeTools.ChromeCookieCrypto;

namespace ChromeTools
{
    public class ChromeCookieReader
    {
        private readonly string _connectionString;
        private readonly byte[] _encryptionKey;

        public ChromeCookieReader(string userDataPath)
        {
            var fullUserDataPath = GetFullUserDataPath(userDataPath);
            _connectionString = GetConnectionString(fullUserDataPath);
            _encryptionKey = GetEncryptionKey(fullUserDataPath);
        }

        public Dictionary<string, string> ReadCookies(string hostName)
        {
            if (hostName == null)
                throw new ArgumentNullException(nameof(hostName));

            using var connection = new SqliteConnection(_connectionString);
            const string query = "SELECT name, encrypted_value FROM cookies WHERE host_key like @hostName and name <> '__cfduid'";
            return connection.Query<(string name, byte[] encrypted)>(query, new { hostName = $"%{hostName}%" })
                .Select(n => (n.name, value: Decrypt(n.encrypted, _encryptionKey)))
                .ToDictionary(k => k.name, v => v.value);
        }

        private static byte[] GetEncryptionKey(string userDataPath)
        {
            var localStatePath = Path.Combine(userDataPath, "Local State");
            if (!File.Exists(localStatePath))
                throw new FileNotFoundException("Can't find local state store.", localStatePath);
            var localStateJson = File.ReadAllText(localStatePath);
            var encKey = GetEncryptedKeyFromLocalStateJson(localStateJson);
            var encryptedData = Convert.FromBase64String(encKey).Skip(5).ToArray();
            return ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.LocalMachine);
        }

        private static string GetEncryptedKeyFromLocalStateJson(string localStateJson)
        {
            var key = JObject.Parse(localStateJson)["os_crypt"]?["encrypted_key"]?.ToString();
            if (key == null)
                throw new InvalidDataException("Local State file doesn't have encryption key.");
            return key;
        }

        private static string GetConnectionString(string userDataPath)
        {
            //var dbPath = Path.Combine(userDataPath, @"Default\Network\Cookies");
            var dbPath = Path.Combine(userDataPath, @"Profile 1\Network\Cookies");
            if (!File.Exists(dbPath))
                throw new FileNotFoundException("Can't find cookie store.", dbPath);

            return $"Data Source={dbPath}";
        }

        private static string GetFullUserDataPath(string userDataPath)
        {
            if (Path.IsPathRooted(userDataPath))
                return userDataPath;

            var localApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localApplicationDataFolder, userDataPath);
        }
    }
}
