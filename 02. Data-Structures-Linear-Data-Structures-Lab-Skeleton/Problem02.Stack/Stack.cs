namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var currentNode = _top;
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

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return this._top.Item;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var oldTop = this._top;
            this._top = oldTop.Next;
            this.Count--;
            return oldTop.Item;
        }

        public void Push(T item)
        {
            var newNode = new Node<T>
            {
                Item = item,
                Next = this._top
            };

            this._top = newNode;
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = _top;
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