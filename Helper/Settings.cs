using System;

namespace DigitalTransformOffice.Service.Helpers
{
    public static class Settings
    {
        public static T Get<T>(string keyName)
        {
            return (T)(object)Environment.GetEnvironmentVariable(keyName, EnvironmentVariableTarget.Process);
        }

        public static string Get(string keyName)
        {
            return Environment.GetEnvironmentVariable(keyName, EnvironmentVariableTarget.Process);
        }
    }
}
