using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace CodeHuntOne
{
    class Program
    {
        static void Main(string[] args)
        {
            while (Restart());        
        }
        public static bool Restart()
        {
            Console.WriteLine("Hello! There is a secret message in here for you. Please enter the password to see the secret message: ");
            var userData = Console.ReadLine();
            var secret = "Boop!";

            if (userData == secret)
            {
                string secretMessage = HiddenSecret();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The encoded secret message: \n" + secretMessage);
                Console.ForegroundColor = ConsoleColor.White;

                return false;
            }
            else if (userData == "Help")
            {
                string secretMessage = HiddenSecret();
                byte[] data = Convert.FromBase64String(secretMessage);
                string decodedMessage = System.Text.Encoding.UTF8.GetString(data);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("The decoded secret message XD \n" + decodedMessage);
                Console.ForegroundColor = ConsoleColor.White;

                return false;

            }
            else if (userData != secret)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That was not the password. Try again!");
                Console.ForegroundColor = ConsoleColor.White;

                return true;
            }
            return true;
        }
        public static string HiddenSecret()
        {
            char[] remove = { '\"'};

            JObject secret = JObject.Parse(File.ReadAllText(@".\obfuscation.json"));

            using (StreamReader file = File.OpenText(@".\obfuscation.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject readSecret = (JObject)JToken.ReadFrom(reader);
            }

            var newSecret = secret.ToString();
            var adjustJson = JsonConvert.DeserializeObject(newSecret).ToString();
            string final = adjustJson.Split('\"')[3];
            //https://www.base64decode.org/

            return final;
        }
    }
}
