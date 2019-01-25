using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OOPMöbler.Models
{
    // Våran klass userdata som används flitigt, vi definerar variabler som används för en användare
    public class UserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Buy> ShoppingCart { get; set; }
        public static List<UserData> UserList = GetUsers();
        // Userdata för användarens email som sparas i selected
        public static UserData GetUserData(string Email)
        {
            var selected = UserList.Where(x => x.Email == Email).FirstOrDefault();
            return selected;
        }
        // Här hämtar vi anvädarens data i json filen genom id't och returnerar userdatan
        public static UserData GetUserData(int id)
        {
            UserData userdata;
            string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/user" + id + ".json");

            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                userdata = JsonConvert.DeserializeObject<UserData>(json, settings);
            }
            else
            {
                userdata = UserList.Where(x => x.Id == id).FirstOrDefault();
            }
            return userdata;
        }
        // Här sparar vi datan med user id't i json filet
        public static void SaveUserData(UserData user)
        {
            string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/user" + user.Id + ".json");
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(user, settings);
            System.IO.File.WriteAllText(filepath, json);
        }
        // Här kan vi lägga till användare, här skulle man kunna lägga till en funktion som gör att man kan registrera användare genom en annan metod
        public static List<UserData> GetUsers()
        {
            List<UserData> UserList = new List<UserData>();
            UserList.Add(new UserData { Id = 1, Email = "alex@primat.se", Password = "123", Name = "Alexander Johansson" });
            UserList.Add(new UserData { Id = 2, Email = "nisse@abc.se", Password = "hejsan", Name = "Nisse Hult" });
            return UserList;
        }
        // Deklarerar id och pris för varor
        public class Buy
        {
            public int Id;
            public int pris;
        }
    }
}