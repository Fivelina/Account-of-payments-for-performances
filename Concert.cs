using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Course_project
{
    internal class Concert
    {
        public int position = 2;
        public string path = @"C:\Users\VReva\source\repos\Course_project\Course_project\info.txt";
        public string tempFilePath = @"C:\Users\VReva\source\repos\Course_project\Course_project\tempFile.txt";

        public ConsoleKey up = ConsoleKey.UpArrow;
        public ConsoleKey down = ConsoleKey.DownArrow;

        public List<Singer> bands = new List<Singer>();
        public List<Singer> singerTemp = new List<Singer>();

        private List<string> bandCountries = new List<string>() { "USA", "Ukraine", "England", "France", "Spain", "Canada" };

        public void SetCursorPosition(ConsoleKey consoleKey, int stringcount)
        {
            if (consoleKey.Equals(up))
            {
                position--;
                if (position < 2)
                {
                    position = stringcount;
                }
            }

            if (consoleKey.Equals(down))
            {
                position++;
                if (position > stringcount)
                {
                    position = 2;
                }
            }
            Console.SetCursorPosition(5, position);
        }

        public void Start()
        {
            StreamReader file = new StreamReader(path);

            string? line;
            while ((line = file.ReadLine()) != null)
            {
                if (line == "")
                {
                    continue;
                }
                string[] parts = line.Split(new char[] { ';' });
                for (int i = 0; i < 6; i++)
                {
                    parts[i].Replace(";", "");
                }
                bands.Add(new Singer(parts[0], parts[1], parts[2], parts[3], int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])));
            }
            file.Close();
        }

        public void Read() //output
        {
            int count = 0;
            Console.WriteLine("   Название\tПопулярость\tЖанр\tСтрана\tЦена\tКоличество концертов\tОбщая стоимость");
            foreach (var item in bands)
            {
                count++;
                Console.WriteLine($"{count}| {item.Name}\t\t{item.Popularity}\t\t{item.Genre}\t{item.Country}\t{item.Price}\t\t{item.ConcertNumber}\t\t{item.PriceAll}\t");
            }
        }

        public string InputPopularity()
        {
            int stringCount = 4;
            Console.WriteLine("выберете уровень популярности из списка");
            Console.WriteLine();
            Console.WriteLine("\tВысокая");
            Console.WriteLine("\tСредняя");
            Console.WriteLine("\tНизкая");

            Console.SetCursorPosition(5, position);
            while (true)
            {
                Console.Write(">");
                ConsoleKey consoleKey = Console.ReadKey().Key;
                Console.SetCursorPosition(5, position);
                Console.Write("  ");

                if (consoleKey.Equals(ConsoleKey.Enter))
                {
                    Console.Clear();
                    switch (position)
                    {
                        case 2:
                            return "Высокая";
                        case 3:
                            return "Средняя";
                        case 4:
                            return "Низкая";
                        default:
                            continue;
                    }
                }
                SetCursorPosition(consoleKey, stringCount);
            }
        }
        public string InputGenre()
        {
            position = 2;
            int strCount = 5;

            Console.WriteLine("\t\tВыберете жанр из списка");
            Console.WriteLine();
            Console.WriteLine("\trock");
            Console.WriteLine("\tgrunge");
            Console.WriteLine("\tpop");
            Console.WriteLine("\tmetal");

            Console.SetCursorPosition(5, position);
            while (true)
            {
                Console.Write(">");
                ConsoleKey consoleKey = Console.ReadKey().Key;
                Console.SetCursorPosition(5, position);
                Console.Write("  ");

                if (consoleKey.Equals(ConsoleKey.Enter))
                {
                    Console.Clear();
                    switch (position)
                    {
                        case 2:
                            return "rock";
                        case 3:
                            return "grunge";
                        case 4:
                            return "pop";
                        case 5:
                            return "metal";
                        default:
                            continue;
                    }
                }
                SetCursorPosition(consoleKey, strCount);
            }
        }

        public bool CheckNameExist(string tempName)
        {
            foreach (Singer item in bands)
            {
                if (tempName.Equals(item.Name))
                {
                    return true;
                }
            }
            return false;
        }

        public int CheckPriceNumber(int number, string popul)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    Console.Clear();

                    if ((popul.Equals("Низкая") && number <= 39000 && number >= 10000) ||
                        (popul.Equals("Средняя") && number <= 100000 && number >= 40000) ||
                        (popul.Equals("Высокая") && number < 100001))
                    {
                        return number;
                    }
                }
                Console.WriteLine("Ошибка! Попробуйте ещё раз: ");
                continue;
            }

        }
        public int CheckConcertNumber(int number)
        {
            while (!int.TryParse(Console.ReadLine(), out number) || !(number <= 20 && number > 0))
            {
                Console.WriteLine("Ошибка! Попробуйте ещё раз: ");
            }
            Console.Clear();
            return number;
        }

        public string InputName()
        {
            string tempName = "";
            while (true)
            {
                Console.Write("Попробуйте ввести название группы/исполнителя: ");
                tempName = Console.ReadLine();
                var regex = new Regex(@"^[A-Z][a-z]{2,10}( [A-Z][a-z]{2,10})*$");
                if (regex.IsMatch(tempName) && !CheckNameExist(tempName))
                {
                    break;
                }
            }
            Console.Clear();
            return tempName;
        }

        public string InputCountry()
        {
            while (true)
            {
                Console.Write("Попробуйте ввести название страны: ");
                string country = Console.ReadLine();

                if (bandCountries.Contains(country))
                {
                    Console.Clear();
                    return country;
                }
            }
        }

        public void Add() //input
        {
            string tempName = InputName();
            string popul = InputPopularity();
            string genre = InputGenre();
            string? country = InputCountry();

            Console.Write("Цена: ");
            int price = 0, concertNum = 0;
            price = CheckPriceNumber(price, popul);
            Console.Write("Количество концертов: ");
            concertNum = CheckConcertNumber(concertNum);

            int priceAll = concertNum * price;

            FileAdd(tempName, popul, genre, country, price, concertNum, priceAll);
        }

        public void FileAdd(string tempName, string popul, string genre, string country, int price, int concertNum, int priceAll)
        {
            Singer singer = new Singer(tempName, popul, genre, country, price, concertNum, priceAll);
            bands.Add(singer);

            using StreamWriter file = new StreamWriter(path, true);
            file.Write(singer.Name + ';');
            file.Write(singer.Popularity + ';');
            file.Write(singer.Genre + ';');
            file.Write(singer.Country + ';');
            file.Write(singer.Price.ToString() + ';');
            file.Write(singer.ConcertNumber.ToString() + ';');
            file.WriteLine(singer.PriceAll.ToString());
        }

        public void Editing() //editing
        {
            Console.WriteLine("Выберете запись, которую хотите редактировать: ");
            Read();

            string newLine = "";
            int elementNumber = 0;
            int count = 0;
            Console.SetCursorPosition(46, 0);
            while (!int.TryParse(Console.ReadLine(), out elementNumber) || !(elementNumber <= bands.Count && elementNumber > 0))
                Console.WriteLine("Ошибка! Попробуйте ещё раз: ");
            Console.Clear();

            foreach (Singer singer in bands)
            {
                count++;
                if (count == elementNumber)
                {
                    Console.WriteLine($"Название: {singer.Name}");
                    string tempName = InputName();
                    Console.Write($"Популярность: {singer.Popularity}\t");
                    string popul = InputPopularity();
                    Console.Write($"Жанр: {singer.Genre}\t");
                    string genre = InputGenre();
                    Console.Write($"Страна: {singer.Country}");
                    string country = InputCountry();

                    int price = 0, concertNum = 0;
                    Console.Write($"Цена: {singer.Price}");
                    price = CheckPriceNumber(price, popul);
                    Console.WriteLine($"Количество концертов: {singer.ConcertNumber}");
                    concertNum = CheckConcertNumber(concertNum);

                    int priceAll = concertNum * price;
                    newLine = tempName + ' ' + popul + ' ' + genre + ' ' + country + ' ' + price + ' ' + concertNum + ' ' + priceAll;
                }
            }
            string temp = "";
            count = 0;
            StreamReader read = new StreamReader(path);
            string? line;
            while ((line = read.ReadLine()) != null)
            {
                count++;
                if (count == elementNumber)
                {
                    temp = line;
                }
            }
            read.Close();
            Delete(temp, newLine);
        }

        public void Delete(string temp, string newLine)
        {
            string strFile = File.ReadAllText(path);
            strFile = strFile.Replace(temp, newLine);
            File.WriteAllText(path, strFile);
            bands.Clear();
            Start();
        }

        public void CaseMessage()
        {
            Console.WriteLine("Нажмите любую клавишу . . .");
            Console.ReadKey();
            Console.Clear();
        }

        public void RequestMenu() //request main
        {
            int strCount = 7;
            Console.Clear();
            Console.WriteLine("\t\tМеню запросов");
            Console.WriteLine();
            Console.WriteLine("\tСамые популярные музыканты");
            Console.WriteLine("\tОбщее количество выступлений по жанру");
            Console.WriteLine("\tСамые затратные музыканты");
            Console.WriteLine("\tПроцентное соотношение популярности музыкантов");
            Console.WriteLine("\tСредняя сума выплат по жанру");
            Console.WriteLine("\tВыход");

            Console.SetCursorPosition(5, position);

            while (true)
            {
                Console.Write(">");
                ConsoleKey consoleKey = Console.ReadKey().Key;
                Console.SetCursorPosition(5, position);
                Console.Write("  ");

                if (consoleKey.Equals(ConsoleKey.Enter))
                {
                    Console.Clear();
                    switch (position)
                    {
                        case 2:
                            MostPopularMusicians();
                            CaseMessage();
                            break;
                        case 3:
                            TotalConcertNumber();
                            CaseMessage();
                            break;
                        case 4:
                            MostExpensiveMusicians();
                            CaseMessage();
                            break;
                        case 5:
                            RequestByPercent();
                            CaseMessage();
                            break;
                        case 6:
                            AveragePrice();
                            CaseMessage();
                            break;
                        case 7:
                            return;
                        default:
                            continue;
                    }
                    RequestMenu();
                }
                SetCursorPosition(consoleKey, strCount);
            }
        }

        public void PrintNameInRequest(Singer[] result)
        {
            Console.WriteLine("Исполнители: ");
            foreach (var item in result)
                Console.WriteLine(item.Name);
        }

        public void MostPopularMusicians() //first request
        {
            int max = 0;
            foreach (Singer item in bands)
                if (max < item.ConcertNumber)
                    max = item.ConcertNumber;

            var popularMusicians = bands.Where(x => x.ConcertNumber == max).ToList();
            PrintNameInRequest(popularMusicians.ToArray());
        }

        public void TotalConcertNumber() //second request
        {
            string requestedGenre = InputGenre();
            var totalNumber = bands.Where(x => x.Genre == requestedGenre).Sum(x => x.ConcertNumber);
            Console.WriteLine($"Количество концертов: {totalNumber}");
        }

        public void MostExpensiveMusicians() //third request
        {
            var max = bands.Max(x => x.Price);
            var maxPrice = bands.Where(price => price.Price == max).ToList();
            PrintNameInRequest(maxPrice.ToArray());
        }

        public void RequestByPercent() //fourth request
        {
            float totalNumber = bands.Sum(x => x.ConcertNumber);

            foreach (Singer item in bands)
            {
                Console.WriteLine($"{item.Name}: {(item.ConcertNumber / totalNumber * 100f).ToString().Substring(0, 4)} %");
            }
        }

        public void AveragePrice() //fifth request
        {
            string requestedGenre = InputGenre();
            var totalNumber = bands.Where(x => x.Genre == requestedGenre)
                                   .Average(x => x.Price);
            Console.WriteLine($"Средняя цена по жанру: {totalNumber}");
        }

        public void SortMenu()
        {
            int strCount = 3;
            Console.Clear();
            Console.WriteLine("\t\tМеню сортировки");
            Console.WriteLine();
            Console.WriteLine("\tПо популярности");
            Console.WriteLine("\tПо общей сумме выплат");
            Console.SetCursorPosition(5, position);

            while (true)
            {
                Console.Write(">");
                ConsoleKey consoleKey = Console.ReadKey().Key;
                Console.SetCursorPosition(5, position);
                Console.Write("  ");

                if (consoleKey.Equals(ConsoleKey.Enter))
                {
                    Console.Clear();
                    switch (position)
                    {
                        case 2:
                            SortByPopularity();
                            CaseMessage();
                            return;
                        case 3:
                            SortByPriceAll();
                            CaseMessage();
                            return;
                    }

                }
                SetCursorPosition(consoleKey, strCount);
            }
        }
        public void SortByPopularity() => bands = bands.OrderBy(x => x.Popularity).ToList();
        public void SortByPriceAll() => bands = bands.OrderBy(x => x.PriceAll).ToList();

        public void DeleteElement()
        {
            Read();

            string newLine = "";
            int elementNumber = 0;
            int count = 0;
            Console.WriteLine("Выберете запись, которую хотите удалить: ");
            while (!int.TryParse(Console.ReadLine(), out elementNumber) || !(elementNumber <= bands.Count && elementNumber > 0))
                Console.Write("Ошибка! Попробуйте ещё раз: ");

            Console.Clear();
            string temp = "";
            StreamReader read = new StreamReader(path);
            string? line;
            while ((line = read.ReadLine()) != null)
            {
                count++;
                if (count == elementNumber)
                {
                    temp = line;
                }
            }
            read.Close();
            Delete(temp, newLine);
            Console.Clear();
        }
    }
}