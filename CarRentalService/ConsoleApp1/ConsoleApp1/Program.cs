using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

    class Program
    {
        static List<Car> carFleet = new List<Car>
        {
            new Car { Id = 1, Brand = "Toyota", Model = "Camry", Year = 2022, PricePerDay = 50.00m },
            new Car { Id = 2, Brand = "Honda", Model = "Accord", Year = 2023, PricePerDay = 55.00m },
            new Car { Id = 3, Brand = "Ford", Model = "Mustang", Year = 2021, PricePerDay = 75.50m },
            new Car { Id = 4, Brand = "BMW", Model = "X5", Year = 2023, PricePerDay = 120.00m },
            new Car { Id = 5, Brand = "Volkswagen", Model = "Golf", Year = 2022, PricePerDay = 45.00m }
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("--- Сервіс прокату автомобілів ---");

            DisplayAvailableCars();

            int carId = GetPositiveInt("\nБудь ласка, введіть ID автомобіля, який бажаєте орендувати: ");
            Car selectedCar = carFleet.FirstOrDefault(c => c.Id == carId && c.IsAvailable);

            if (selectedCar != null)
            {
                int rentalDays = GetPositiveInt($"Введіть кількість днів для оренди '{selectedCar.Brand} {selectedCar.Model}': ");
                decimal totalCost = rentalDays * selectedCar.PricePerDay;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n--- Деталі вашого замовлення ---");
                Console.WriteLine($"Автомобіль: {selectedCar.Year} {selectedCar.Brand} {selectedCar.Model}");
                Console.WriteLine($"Термін оренди: {rentalDays} днів");
                Console.WriteLine($"Загальна вартість: {totalCost:C}");
                Console.ResetColor();

                selectedCar.IsAvailable = false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Помилка! Автомобіль з таким ID не знайдено або він вже орендований.");
                Console.ResetColor();
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        static void DisplayAvailableCars()
        {
            Console.WriteLine("\n--- Доступні автомобілі для оренди ---");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("| ID | Марка        | Модель      | Рік  | Ціна за добу  |");
            Console.WriteLine("----------------------------------------------------------");

            foreach (var car in carFleet.Where(c => c.IsAvailable))
            {
                Console.WriteLine($"| {car.Id,-2} | {car.Brand,-12} | {car.Model,-11} | {car.Year} | {car.PricePerDay,12:C} |");
            }
            Console.WriteLine("----------------------------------------------------------");
        }

        static int GetPositiveInt(string prompt)
        {
            int parsedValue;
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out parsedValue) && parsedValue > 0)
                {
                    return parsedValue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Помилка! Будь ласка, введіть додатне ціле число (наприклад, 1, 5, 10).");
                    Console.ResetColor();
                }
            }
        }
    }
