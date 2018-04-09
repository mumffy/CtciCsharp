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
        private static int fakeTime = 0;
        public static int FakeTime
        {
            get
            {
                fakeTime++;
                return fakeTime;
            }
        }

        private Dictionary<string, Book> books;
        public int CacheSize { get; }

        public Q03IsbnCache(int size)
        {
            books = new Dictionary<string, Book>();
            CacheSize = size;
        }

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
            string lruIsbn = books.OrderBy(x => x.Value.LastUsed).First().Key;
            books.Remove(lruIsbn);
        }

        public void Remove(string isbn)
        {
            books.Remove(isbn);
        }

        private class Book
        {
            public string Isbn { get; }
            public int Price { get; }
            //private DateTime lastUsed;

            //public DateTime LastUsed => lastUsed;
            private int lastUsed;
            public int LastUsed => lastUsed;

            public Book(string isbn, int price)
            {
                Isbn = isbn;
                Price = price;
            }

            internal void UpdateLastUsedTime()
            {
                lastUsed = FakeTime;
            }
        }
    }

    public class C12Q03_Tests
    {
        [Fact]
        public void LruBookIsRemoved()
        {
            Q03IsbnCache cache = new Q03IsbnCache(3);
            cache.Insert("a", 0);
            cache.Insert("b", 0);
            cache.Insert("c", 0);
            cache.Contains("a");
            cache.Insert("d", 0);
            Assert.False(cache.Contains("b"));
            Assert.True(cache.Contains("a"));
            Assert.True(cache.Contains("c"));
            Assert.True(cache.Contains("d"));
        }
    }
}
