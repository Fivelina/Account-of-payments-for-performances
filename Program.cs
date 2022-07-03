
using Course_project;

int position = 2;
ConsoleKey up = ConsoleKey.UpArrow;
ConsoleKey down = ConsoleKey.DownArrow;
Concert os = new Concert();

os.Start();

int stringCount = 8;

void PrintMenu()
{
    Console.WriteLine("\t\tMenu");
    Console.WriteLine();
    Console.WriteLine("\tПросмотр файла");
    Console.WriteLine("\tДобавить записи");
    Console.WriteLine("\tРедактировать записи");
    Console.WriteLine("\tЗапросы");
    Console.WriteLine("\tCортировать записи");
    Console.WriteLine("\tУдалить записи");
    Console.WriteLine("\tВыход из программы");
    Console.SetCursorPosition(5, position);
}

PrintMenu();

while (true)
{
    Console.Write(">");
    ConsoleKey consoleKey = Console.ReadKey().Key;
    Console.SetCursorPosition(4, position);
    Console.Write("   ");

    if (consoleKey.Equals(ConsoleKey.Enter))
    {
        Console.Clear();
        switch (position)
        {
            case 2:
                os.Read();
                os.CaseMessage();
                break;
            case 3:
                os.Add();
                os.CaseMessage();
                break;
            case 4:
                os.Editing();
                os.CaseMessage();
                break;
            case 5:
                os.RequestMenu();
                break;
            case 6:
                os.SortMenu();
                break;
            case 7:
                os.DeleteElement();
                break;
            case 8:
                return 0;
            default:
                Console.WriteLine("Неправильний пункт меню");
                continue;
        }
        PrintMenu();
    }
    if (consoleKey.Equals(up))
    {
        position--;
        if (position < 2)
        {
            position = stringCount;
        }
    }

    if (consoleKey.Equals(down))
    {
        position++;
        if (position > stringCount)
        {
            position = 2;
        }
    }
    Console.SetCursorPosition(5, position);
}