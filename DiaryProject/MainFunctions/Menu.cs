using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryProject.MainFunctions
{
    public static class Menu
    {
        public static void start()
        {
            Console.WriteLine("Welcome to the Diary Project!");
            Console.WriteLine("If you have an existing username, press 1");
            Console.WriteLine("If you don't have an existing username, press 0");
            string input = Console.ReadLine().Trim();
            if (input == "0")
            {
                Menu.usernameCreate();
            }
            else if (input == "1")
            {
                Menu.login();
            }
            else
            {
                Console.WriteLine("Type 1 or 0 please.\n");
                Menu.start();
            }
        }
        public static void login()
        {
            MenuFunctions.usernameLogin();
        }

        public static void usernameCreate()
        {
            Console.WriteLine("\nNew username:");
            MenuFunctions.usernameCreate();
        }
        public static void mainMenuLoop(string username)
        {
            bool programContinue = true;
            while (programContinue)
            {
                Console.WriteLine("1.Write a new entry.");
                Console.WriteLine("2.Edit an existing entry.");
                Console.WriteLine("3.Delete an entry.");
                Console.WriteLine("4.Show full list of entries.");
                Console.WriteLine("5.Log out.");
                Console.WriteLine("6.Exit application.\n");
                string input = Console.ReadLine().Trim();
                if (input == "1")
                {
                    Console.WriteLine("What do you want to share today?\n");
                    MenuFunctions.newEntry(username);
                }
                else if (input == "2")
                {
                    MenuFunctions.editEntry(username);
                }
                else if (input == "3")
                {
                    MenuFunctions.deleteEntry(username);
                }
                else if (input == "4")
                {
                    MenuFunctions.viewAllEntries(username);
                }
                else if (input == "5")
                {
                    MenuFunctions.logOut();
                }
                else if (input == "6")
                {
                    programContinue = false;
                }  
            }
        }
    }
}
