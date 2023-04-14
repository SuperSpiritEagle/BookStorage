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
            const string CommandSearchParameters = "4";
            const string CommandExit = "5";

            BookStorage bookStorage = new BookStorage();

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"\n   Хранилище книг\n" +
                    $"\n [{CommandAddBook}] Добавить книгу" +
                    $"\n [{CommandDeleteBook}] Удалить книгу" +
                    $"\n [{CommandShowBooks}] Показать книги" +
                    $"\n [{CommandSearchParameters}] Поиск по параметрам" +
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

                    case CommandSearchParameters:
                        bookStorage.Search();
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
            public Book(string title, string autor, int yearRelease)
            {
                TitleBook = title;
                Author = autor;
                YearRelease = yearRelease;
            }

            public string TitleBook { get; private set; }
            public string Author { get; private set; }
            public int YearRelease { get; private set; }
        }

        class BookStorage
        {
            private List<Book> _books = new List<Book>();

            public BookStorage()
            {
                _books.Add(new Book("Unity 2018", "Майкл Гейл", 2018));
                _books.Add(new Book("Бим Чёрное ухо", " Гавриил Троепольский", 1971));
                _books.Add(new Book("Бойцовский клуб", "Чак Паланик", 1996));
                _books.Add(new Book("Unity 2018", "Майкл Гейл", 2018));
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
                title = Console.ReadLine();

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
                Console.WriteLine("Удаление ");
                Console.Write("Введите автора: ");
                string author = Console.ReadLine();

                bool isDeletedAuthor = false;

                for (int i = 0; i < _books.Count; i++)
                {
                    if (_books[i].Author == author)
                    {
                        Console.WriteLine($"Удалить Книгу номер : {i + 1} {_books[i].TitleBook}. Автор: {_books[i].Author}. Год выпуска: {_books[i].YearRelease}");
                        isDeletedAuthor = true;
                    }
                }

                if (isDeletedAuthor == true)
                {
                    Console.WriteLine("Введите номер кники для удаления");
                    int.TryParse(Console.ReadLine(), out int deleteAuthor);

                    if (_books[deleteAuthor - 1].Author == author)
                    {
                        _books.RemoveAt(deleteAuthor - 1);
                        Console.WriteLine("Книга удалена!");
                    }
                    else
                    {
                        Console.WriteLine("Удаление не возможно");
                    }
                }
                else
                {
                    Console.WriteLine("Автор не найден");
                }

            }

            public void ShowBooks()
            {
                ShowAllBooks();
            }

            private void ShowAllBooks()
            {
                foreach (var book in _books)
                {
                    Console.WriteLine($"Книга: {book.TitleBook}. Автор: {book.Author}. Год выпуска: {book.YearRelease}");
                }
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

                return text.ToString();
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

            public void Search()
            {
                const string CommandSearchAuthor = "1";
                const string CommandSearchYearRelease = "2";

                Console.WriteLine($"{CommandSearchAuthor} Поиск по автору {CommandSearchYearRelease} поиск по году выпуска");

                switch (Console.ReadLine())
                {
                    case CommandSearchAuthor:
                        SearchAuthor();
                        break;

                    case CommandSearchYearRelease:
                        SearchYearRelease();
                        break;
                }
            }

            private void SearchYearRelease()
            {
                bool isFound = false;
                Console.WriteLine("\nВведите год выпуска\n");
                int.TryParse(Console.ReadLine(), out int input);

                foreach (Book book in _books)
                {
                    if (book.YearRelease == input)
                    {
                        if (isFound == false)
                        {
                            Console.WriteLine("\nРезультаты поиска\n");
                        }

                        ShowBooksAuthor(book);

                        isFound = true;
                    }
                }

                if (isFound == false)
                {
                    Console.WriteLine("\nКнига от данного автора не найдена\n");
                }
            }

            private void SearchAuthor()
            {
                bool isFound = false;
                Console.WriteLine("\nВведите автора\n");
                string input = Console.ReadLine();

                foreach (Book book in _books)
                {
                    if (book.Author == input)
                    {
                        if (isFound == false)
                        {
                            Console.WriteLine("\nРезультаты поиска\n");
                        }

                        ShowBooksAuthor(book);

                        isFound = true;
                    }
                }

                if (isFound == false)
                {
                    Console.WriteLine("\nКнига от данного автора не найдена\n");
                }
            }

            private void ShowBooksAuthor(Book booksAuthor)
            {
                Console.WriteLine($"Книга: {booksAuthor.TitleBook}. Автор: {booksAuthor.Author}. Год выпуска: {booksAuthor.YearRelease}");
            }
        }
    }
}
