// <copyright file="AssemblyFileLoader.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Reflection;

namespace Drastic.Social.Mocks
{
    public static class AssemblyFileLoader
    {
        public static string GetResourceFileContentAsString(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Drastic.Social.Mocks." + fileName;

            string? resource = null;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName)!)
            {
                using StreamReader reader = new StreamReader(stream);
                resource = reader.ReadToEnd();
            }

            return resource!;
        }

        public static T DeserializeViaResourceFile<T>(string filename)
        {
            var file = GetResourceFileContentAsString(filename);
            return System.Text.Json.JsonSerializer.Deserialize<T>(file)!;
        }
    }
}
