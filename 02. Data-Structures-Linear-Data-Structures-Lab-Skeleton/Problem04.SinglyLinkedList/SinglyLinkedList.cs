namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>
            {
                Item = item,
                Next = this._head
            };

            this._head = newNode;
            Count++;
        }

        public void AddLast(T item)
        {
            if (Count == 0)
            {
                this._head = new Node<T>();
                this._head.Item = item;
                this._head.Next = null;
                Count++;
                return;
            }
            var currentElement = this._head;
            while (true)
            {
                if (currentElement.Next == null)
                {
                    currentElement.Next = new Node<T>();
                    currentElement.Next.Item = item;
                    Count++;
                    break;
                }

                currentElement = currentElement.Next;
            }
        }

        public T GetFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            return this._head.Item;
        }

        public T GetLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            var currentElement = this._head;
            while (true)
            {
                if (currentElement.Next == null)
                {
                    return currentElement.Item;
                }
                currentElement = currentElement.Next;
            }
        }

        public T RemoveFirst()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            var oldHead = this._head;
            this._head = oldHead.Next;
            Count--;
            return oldHead.Item;
        }

        public T RemoveLast()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            if (Count == 1)
            {
                var item = this._head.Item;
                this._head = null;
                Count--;
                return item;
            }
            var currentElement = this._head;

            while (true)
            {
                if (currentElement.Next.Next == null)
                {
                    var item = currentElement.Next.Item;
                    currentElement.Next = null;
                    Count--;
                    return item;
                }
                currentElement = currentElement.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = _head;
            while (currentNode.Next != null)
            {
                yield return currentNode.Item;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}