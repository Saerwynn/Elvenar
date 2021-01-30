using System.Security.Cryptography;
using System.Text;

namespace Elvenar.Misc
{
    class Encryption
    {
        public static string Hash(UserTokens x, string json)
        {
            const string get_key = "MAW#YB*y06wqz$kTOE";    // < de_innogames_shared_networking_protection_SaltGenerator

            string md5 = x.gateway_id + get_key + json;

            MD5 md5Hash = MD5.Create();

            string hash = GetMd5Hash(md5Hash, md5);
            string hashed = hash.Substring(0, 10);

            string query = hashed + json;

            return query;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);

            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
