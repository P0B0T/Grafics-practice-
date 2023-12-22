using Newtonsoft.Json;

namespace Grafics.Classes
{
    static public class DataUser
    {
        static public string UserRole;

        static public string Rights { get; set; } = JsonConvert.DeserializeObject<string>(System.IO.File.ReadAllText("rights.json"));
    }
}
