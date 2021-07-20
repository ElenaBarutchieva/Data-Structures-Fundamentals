namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var currentNode = _head;
            while (true)
            {
                if (currentNode.Item.Equals(item))
                {
                    return true;
                }
                if (currentNode.Next == null)
                {
                    break;
                }
                currentNode = currentNode.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var oldHead = this._head;
            this._head = oldHead.Next;
            Count--;
            return oldHead.Item;
        }

        public void Enqueue(T item)
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

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return this._head.Item;
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