using System.Security.Cryptography;
using System.Text;

namespace ThAmCo.User_Profiles.Utility
{
    public interface IGuidUtility
    {
        public string GenerateShortGuid(Guid guid);
    }

    public class GuidUtility : IGuidUtility
    {
        public string GenerateShortGuid(Guid guid)
        {
            // Convert the GUID to bytes
            byte[] guidBytes = guid.ToByteArray();

            // Compute the MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(guidBytes);

                // Convert the hash to a hexadecimal string
                StringBuilder hashStringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashStringBuilder.Append(b.ToString("x2"));
                }

                // Take the first 8 characters for a shorter representation
                string shortHash = hashStringBuilder.ToString().Substring(0, 12);

                return shortHash;
            }
        }
    }
}
