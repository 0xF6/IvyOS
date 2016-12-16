namespace IvyOS.IO
{
    using System.Collections.Generic;
    public class UserSystem
    {
        public enum Rank
        {
            User = 3,
            Admin = 2,
            System = 0
        }

        public class User
        {
            public string Name { get; private set; }
            public string Password { get; private set; }
            public string UserPath { get; private set; }

            public Rank Rank { get; private set; }

            public User(string Name, string Password, string UserPath, Rank Rank = Rank.User)
            {
                this.Name = Name;
                this.Password = Password;
                this.UserPath = UserPath;
                this.Rank = Rank;
            }
        }

        public static List<User> Users { get; private set; }
        public static User ActiveUser;
        public static bool Login(string Name, string Password)
        {
            if (Users == null)
                Load();
            bool it = false;
            foreach (User Item in Users)
            if (Item.Name == Name)
            if (Item.Password == Password)
            {
                ActiveUser = Item;
                it = true;
            }
            return it;
        }
        static void Load()
        {
            Users = new List<User>();
            Users.Add(new User("System", "010101", "System", Rank.System));
            Users.Add(new User("La", "123", "La", Rank.Admin));
        }

        public static void Logout() => ActiveUser = null;
        public static void Init() => Load();
    }
}