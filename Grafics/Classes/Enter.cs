using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grafics.Classes
{
    public class Enter
    {
        public List<User> ListUsers = JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText("listUsers.json"));
    }
}
