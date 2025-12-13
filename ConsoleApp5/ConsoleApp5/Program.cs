using System;
using System.IO;

class Program
{
    static string filePath = "users.csv";

    static void Main()
    {
        int choice;
        do
        {
            Console.WriteLine("\n1. Реєстрація");
            Console.WriteLine("2. Авторизація");
            Console.WriteLine("3. Вихід");
            Console.Write("Ваш вибір: ");
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    Register();
                    break;
                case 2:
                    Login();
                    break;
            }
        } while (choice != 3);
    }

    static void Register()
    {
        Console.Write("Логін: ");
        string login = Console.ReadLine();
        Console.Write("Пароль: ");
        string password = Console.ReadLine();

        File.AppendAllText(filePath, login + ";" + password + "\n");
        Console.WriteLine("Користувача зареєстровано!");
    }

    static void Login()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл користувачів не знайдено.");
            return;
        }

        Console.Write("Логін: ");
        string login = Console.ReadLine();
        Console.Write("Пароль: ");
        string password = Console.ReadLine();

        string[] users = File.ReadAllLines(filePath);
        foreach (string user in users)
        {
            string[] data = user.Split(';');
            if (data[0] == login && data[1] == password)
            {
                Console.WriteLine("Авторизація успішна!");
                return;
            }
        }
        Console.WriteLine("Невірний логін або пароль.");
    }
}