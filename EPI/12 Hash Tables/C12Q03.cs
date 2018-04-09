using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EPI.C12_HashTables
{

    public interface IBookNumberCache
    {
        bool Contains(string isbn);
        int GetPrice(string isbn);
        void Insert(string isbn, int price);
        void Remove(string isbn);
    }

    class Q03IsbnCache : IBookNumberCache
    {
        private Dictionary<string, Book> books = new Dictionary<string, Book>();
        public int CacheSize { get; }
        private Book lruBook;

        public bool Contains(string isbn)
        {
            bool found = books.ContainsKey(isbn);
            if (found)
                books[isbn].UpdateLastUsedTime();
            return found;
        }

        public int GetPrice(string isbn)
        {
            Contains(isbn);
            return books[isbn].Price;
        }

        public void Insert(string isbn, int price)
        {
            if (!books.ContainsKey(isbn))
            {
                if (books.Count == CacheSize)
                    evictLruBook(books);
                books[isbn] = new Book(isbn, price);
            }
            books[isbn].UpdateLastUsedTime();
        }

        private void evictLruBook(Dictionary<string, Book> books)
        {
            throw new NotImplementedException();
        }

        public void Remove(string isbn)
        {
            books.Remove(isbn);
        }

        private class Book
        {
            private DateTime lastUsed;
            public string Isbn { get; }
            public int Price { get; }
            public DateTime LastUsed => lastUsed;

            public Book(string isbn, int price)
            {
                Isbn = isbn;
                Price = price;
                //UpdateLastUsedTime();
            }

            internal void UpdateLastUsedTime()
            {
                lastUsed = DateTime.UtcNow;
            }
        }
    }

    public class C12Q03_Tests
    {

    }
}
