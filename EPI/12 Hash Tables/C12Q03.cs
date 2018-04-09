using System.Linq;
using Xunit;
using EPI.DataStructures.LinkedList;
using System.Collections.Generic;

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

        private DoublyLinkedList<Book> lruBooks;
        private Dictionary<string, DoublyLinkedListNode<Book>> books;
        public int CacheSize { get; }
        public int Count => books.Count;

        public Q03IsbnCache(int size)
        {
            books = new Dictionary<string, DoublyLinkedListNode<Book>>();
            lruBooks = new DoublyLinkedList<Book>();
            CacheSize = size;
        }

        public bool Contains(string isbn)
        {
            bool found = books.ContainsKey(isbn);
            if (found)
                UpdateBookUsage(isbn);
            return found;
        }

        public int GetPrice(string isbn)
        {
            Contains(isbn);
            return books[isbn].Value.Price;
        }

        public void Insert(string isbn, int price)
        {
            if (!books.ContainsKey(isbn))
            {
                if (books.Count == CacheSize)
                    evictLruBook(books);

                var newBookNode = new DoublyLinkedListNode<Book>(new Book(isbn, price));
                books[isbn] = newBookNode;
                lruBooks.AddToTail(newBookNode);
            }
            UpdateBookUsage(isbn);
        }

        private void evictLruBook(Dictionary<string, DoublyLinkedListNode<Book>> books)
        {
            //string lruIsbn = books.OrderBy(x => x.Value.Value.LastUsed).First().Key;
            var lru = lruBooks.RemoveHead();
            books.Remove(lru.Value.Isbn);
        }

        public void Remove(string isbn)
        {
            var removalNode = books[isbn];
            lruBooks.Remove(removalNode);
            books.Remove(isbn);
        }

        private void UpdateBookUsage(string isbn)
        {
            books[isbn].Value.UpdateLastUsedTime();
            lruBooks.MoveToTail(books[isbn]);
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
            cache.Insert("a", 123);
            cache.Insert("b", 0);
            cache.Insert("c", 0);
            cache.Contains("a");
            cache.Insert("d", 0);
            Assert.False(cache.Contains("b"));
            Assert.True(cache.Contains("a"));
            Assert.True(cache.Contains("c"));
            Assert.True(cache.Contains("d"));

            cache.Insert("a", 999);
            cache.Insert("x", 0);
            Assert.False(cache.Contains("c"));
            Assert.True(cache.Contains("d"));
            Assert.True(cache.Contains("a"));
            Assert.True(cache.Contains("x"));
            Assert.Equal(123, cache.GetPrice("a"));

            Assert.Equal(3, cache.Count);
            cache.Remove("a");
            Assert.Equal(2, cache.Count);
        }
    }
}
