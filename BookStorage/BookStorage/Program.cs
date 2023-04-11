using System;
using System.Collections.Generic;

namespace BookStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddBook = "1";
            const string CommandDeleteBook = "2";
            const string CommandShowBooks = "3";
            const string CommandExit = "4";

            BookStorage bookStorage = new BookStorage();

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"\n   Хранилище книг\n" +
                    $"\n [{CommandAddBook}] Добавить книгу" +
                    $"\n [{CommandDeleteBook}] Удалить книгу" +
                    $"\n [{CommandShowBooks}] Показать книги" +
                    $"\n [{CommandExit}] Выход");

                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case CommandAddBook:
                        bookStorage.AddBoock();
                        break;

                    case CommandDeleteBook:
                        bookStorage.RemoveBook();
                        break;

                    case CommandShowBooks:
                        bookStorage.ShowBooks();
                        break;

                    case CommandExit:
                        Console.WriteLine("Программа закончила работу");
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Ошибка. Введены не коректные данные");
                        break;
                }
            }
        }

        class Book
        {
            public string TitleBook { get; private set; }
            public string Author { get; private set; }
            public int YearRelease { get; private set; }

            public Book(string title, string autor, int yearRelease)
            {
                TitleBook = title;
                Author = autor;
                YearRelease = yearRelease;
            }
        }

        class BookStorage
        {
            private List<Book> _books = new List<Book>();

            public BookStorage()
            {
                _books.Add(new Book("Unity 2018", "Майкл Гейл", 2018));
                _books.Add(new Book("Бим Чёрное ухо", " Гавриил Троепольский", 1971));
                _books.Add(new Book("Бойцовский клуб", "Чак Паланик", 1996));
            }

            public void AddBoock()
            {
                string title;
                string author;
                string nameAuthor;
                string surnameAuthor;
                int yearRelease;

                Console.Write("Введите название книги: ");
                title = CheckInputText();
                Console.Write("Введите имя автора: ");
                nameAuthor = CheckInputText();
                Console.Write("Введите фамилию автора: ");
                surnameAuthor = CheckInputText();
                author = nameAuthor + " " + surnameAuthor;
                Console.Write("Введите год выпуска книги: ");
                yearRelease = CheckInputNumber();

                if (title == null || author == null || yearRelease == 0)
                {
                    Console.WriteLine("Ошибка. Введены не коректные данные");
                }
                else
                {
                    _books.Add(new Book(title, author, yearRelease));
                    Console.WriteLine("Книга добавлена");
                }
            }

            public void RemoveBook()
            {
                Console.WriteLine("Удалить книгу");

                if (TryFindBook(out Book book))
                {
                    _books.Remove(book);
                }
            }

            public void ShowBooks()
            {
                const string CommandShowAllBooks = "1";
                const string CommandBookSearchParameters = "2";

                Console.WriteLine($"[{CommandShowAllBooks}] Показать все книги" +
                    $"\n[{CommandBookSearchParameters}] Показать книги по параметрам");
                string userInput = Console.ReadLine();

                if (userInput == CommandShowAllBooks)
                {
                    ShowAllBooks();
                }
                else if (userInput == CommandBookSearchParameters)
                {
                    if (TryFindBook(out Book book))
                    {
                        Console.WriteLine($"Книга: {book.TitleBook}. Автор: {book.Author}. Год выпуска: {book.YearRelease}");
                    }
                }
            }

            private void ShowAllBooks()
            {
                foreach (var book in _books)
                {
                    Console.WriteLine($"Книга: {book.TitleBook}. Автор: {book.Author}. Год выпуска: {book.YearRelease}");
                }
            }

            private bool TryFindBook(out Book book)
            {
                const string CommandFindBookName = "1";
                const string CommandFindBookAuthor = "2";
                const string CommandFindYearRelease = "3";

                Console.WriteLine($"[{CommandFindBookName}] Найти книгу по названию\n" +
                                  $"[{CommandFindBookAuthor}] Найти книгу по автору\n" +
                                  $"[{CommandFindYearRelease}] Найти книгу по году выпуска");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandFindBookName:
                        Console.Write("Введите название книги: ");
                        string nameBook = Console.ReadLine();

                        for (int i = 0; i < _books.Count; i++)
                        {
                            if (_books[i].TitleBook.ToLower() == nameBook.ToLower())
                            {
                                book = _books[i];
                                return true;
                            }
                        }

                        Message();
                        break;

                    case CommandFindBookAuthor:
                        Console.Write("Введите имя или фамилию автора: ");
                        string author = Console.ReadLine();

                        for (int i = 0; i < _books.Count; i++)
                        {
                            if (author.ToLower() == _books[i].Author.ToLower())
                            {
                                book = _books[i];
                                return true;
                            }
                        }

                        Message();
                        break;

                    case CommandFindYearRelease:
                        int yearRelease;

                        Console.Write("Введите год выпуска книги: ");
                        int.TryParse(Console.ReadLine(), out yearRelease);

                        for (int i = 0; i < _books.Count; i++)
                        {
                            if (yearRelease == _books[i].YearRelease)
                            {
                                book = _books[i];
                                return true;
                            }
                        }

                        Message();
                        break;

                    default:
                        Console.WriteLine("Такой команды нету. Повторите попытку");
                        break;
                }

                book = null;
                return false;
            }

            private string CheckInputText()
            {
                string text = Console.ReadLine();

                foreach (char symbol in text)
                {
                    if (char.IsLetter(symbol) == false)
                    {
                        Console.WriteLine("Введены некоретные данный поторите попытку.");
                        return null;
                    }
                }

                return text;
            }

            private int CheckInputNumber()
            {
                int maxLength = 4;
                bool isNumber = int.TryParse(Console.ReadLine(), out int yearRelease);

                if (isNumber == true && yearRelease >= maxLength)
                {
                    Console.WriteLine("Дейстие выполнено.");
                    return yearRelease;
                }
                else
                {
                    Console.WriteLine("Введены некоретные данный поторите попытку.");
                    yearRelease = 0;
                    return yearRelease;
                }
            }

            private void Message()
            {
                Console.WriteLine("Такой книги нету");
            }
        }
    }
}
