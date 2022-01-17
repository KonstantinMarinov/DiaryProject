using DiaryProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryProject.MainFunctions
{
    public static class MenuFunctions
    {
        internal static bool usernameLogin()
        {
            var db = new Database();
            Console.WriteLine("Username:");
            string username = Console.ReadLine().Trim();
            bool loggedIn = false;
            foreach(var user in db.Users)
            {
                if(user.Name == username)
                {
                 Console.WriteLine("Password:");
                    string password;
                    bool success = false;
                    while(!success)
                    {
                        password = Console.ReadLine().Trim();
                        if(password == user.Password)
                        {
                            success = true;
                            Console.WriteLine("Success!\n");
                            loggedIn = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password! Try again.\n");
                        }
                    }
                }
                if (loggedIn)
                {
                    break;
                }
            }
            if (!loggedIn)
            {
                Console.WriteLine("Username not found. Try again. \n");
                MenuFunctions.usernameLogin();
            }
            Menu.mainMenuLoop(username);
            return true;
        }
        public static bool usernameCreate()
        {
            var db = new Database();
            string username = Console.ReadLine().Trim();
            if (username == "" || username == "0")
            {
                Console.WriteLine("Username blank or 0, please try again.\n");
                MenuFunctions.usernameCreate();
            }
            else
            {
                var allUsers = db.Users.ToList();
                foreach (var user in allUsers)
                {
                    if (user.Name == username)
                    {
                        Console.WriteLine("Username already exists! Try another.\n");
                        MenuFunctions.usernameCreate();
                    }
                }
                MenuFunctions.passwordCreate(username);
               
            }
            return true;
        }
        public static bool passwordCreate(string username)
        {
            var db = new Database();
            Console.WriteLine("Password:");
            string password = Console.ReadLine().Trim();
            if (password == "")
            {
                Console.WriteLine("A blank password is not a password - Albert Einstein. Try again.\n");
                MenuFunctions.passwordCreate(username);
            }
            else
            {
               Console.WriteLine("Repeat password:");
               var check = MenuFunctions.passwordVerify(password);
                if (check)
                {
                    var newUser = new User { Name = username, Password = password };
                    db.Add(newUser);
                    db.SaveChanges();
                    Console.WriteLine("The new user has been added!\n");
                    Menu.login();
                }
                else
                {
                    Console.WriteLine("Passwords don't match! Try again.\n");
                    MenuFunctions.passwordCreate(username);
                }
            }
            return true;
        }
        public static bool passwordVerify(string password)
        {
            string verifyPassword = Console.ReadLine().Trim();
            return verifyPassword == password;
        }

        internal static void newEntry(string username)
        {
            var db = new Database();
            string entry = Console.ReadLine().Trim();
            var user = db.Users.FirstOrDefault(x => x.Name == username);
            var dbEntry = new Diary { Date = DateTime.Today, Text = entry,User = user};
            db.Diaries.Add(dbEntry);
            db.SaveChanges();
            Console.WriteLine("New entry added!\n");
        }

        internal static void logOut()
        {
            MenuFunctions.usernameLogin();
        }

        internal static void editEntry(string username)
        {
            var db = new Database();
            Console.WriteLine("Write the number of the entry that you want to edit.");
            string input = Console.ReadLine().Trim();  
            int idNumber = Int32.Parse(input);
            var entry = db.Diaries.FirstOrDefault(x => x.DiaryId == idNumber);
            if (entry == null)
            {
                Console.WriteLine("No such entry!\n");
                Menu.mainMenuLoop(username);
            }  
            Console.WriteLine("Content of this entry:\n");
            Console.WriteLine(entry.Text.ToString());
            Console.WriteLine("\nWrite the new entry:");
            string newText = Console.ReadLine().Trim();
            entry.Text = newText;
            db.SaveChanges();
            Console.WriteLine("Entry edited successfully!\n");

        }

        internal static void deleteEntry(string username)
        {
            var db = new Database();
            Console.WriteLine("Write the number of the entry that you want to delete.");
            string input = Console.ReadLine().Trim();
            int idNumber = Int32.Parse(input);
            var entry = db.Diaries.FirstOrDefault(x => x.DiaryId == idNumber);
            Console.WriteLine("Content of this entry:\n");
            Console.WriteLine(entry.Text.ToString());
            Console.WriteLine("Press 1 to delete entry, press 0 to go back to the main menu.");
            string number = Console.ReadLine().Trim(); 
            if (number == "1")
            {
                db.Diaries.Remove(entry);
                db.SaveChanges();
                Console.WriteLine("Entry deleted successfully!");
            }
            else 
            {
                Menu.mainMenuLoop(username);
            }
        }

        internal static void viewAllEntries(string username)
        {
            var db = new Database();
            Console.WriteLine("This is a list of all the current entries:\n");
            var user = db.Users.FirstOrDefault(x => x.Name == username);
            var allEntries = db.Diaries.Where(x => x.User == user).ToList();
            foreach(var entry in allEntries)
            {
                Console.WriteLine(entry.Text);
            }
            Console.WriteLine();
        }
    }
}
