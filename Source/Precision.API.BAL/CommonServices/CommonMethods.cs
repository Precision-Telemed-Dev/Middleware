using Amazon;
using Amazon.Runtime.CredentialManagement;
using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.Model.Common;
using Precision.API.Model.Enums;
using Newtonsoft.Json;
using System.Net;

namespace Precision.API.BAL.CommonServices
{    
    public class CommonMethods : ICommonMethods
    {
        public CommonMethods()
        {
        }
        public Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
        public async Task CreateOrAppendFile(string path, string content)
        {
            try
            {
                string? directoryName = Path.GetDirectoryName(path);

                if (!string.IsNullOrEmpty(directoryName))
                {
                    Directory.CreateDirectory(directoryName);

                    using (StreamWriter fs = File.AppendText(path))
                    {
                        await fs.WriteLineAsync(content);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void WriteProfile(string profileName, string keyId, string secret)
        {
            Console.WriteLine($"Create the [{profileName}] profile...");
            var options = new CredentialProfileOptions
            {
                AccessKey = keyId,
                SecretKey = secret
            };
            var profile = new CredentialProfile(profileName, options);
            var sharedFile = new SharedCredentialsFile();
            sharedFile.RegisterProfile(profile);
        }
        public void AddRegion(string profileName, RegionEndpoint region)
        {
            var sharedFile = new SharedCredentialsFile();
            CredentialProfile profile;
            if (sharedFile.TryGetProfile(profileName, out profile))
            {
                profile.Region = region;
                sharedFile.RegisterProfile(profile);
            }
        }
    }
}