namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>
            {
                Item = item,
                Previous = null
            };

            if (Count == 0)
            {
                this.head = newNode;
                this.tail = newNode;
                Count++;
            }
            else
            {
                this.head.Previous = newNode;
                newNode.Next = this.head;
                this.head = newNode;
                this.Count++;
            }
        }

        public void AddLast(T item)
        {
            var newNode = new Node<T>
            {
                Item = item
            };

            if (this.head is null)
                this.head = this.tail = newNode;
            else
            {
                var current = this.tail;

                newNode.Previous = current;
                current.Next = newNode;
                this.tail = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();

            return this.head.Item;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();

            var current = this.tail;

            return current.Item;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();

            var headItem = this.head.Item;
            var newHead = this.head.Next;
            this.head.Next = null;
            this.head = newHead;
            this.Count--;

            return headItem;
        }

        public T RemoveLast()
        {
            this.EnsureNotEmpty();

            if (this.head.Next is null)
                return this.RemoveFirst();

            var current = this.tail;
            var prev = current.Previous;
            prev.Next = null;
            this.tail = prev;

            this.Count--;

            return current.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();
        }
    }
}