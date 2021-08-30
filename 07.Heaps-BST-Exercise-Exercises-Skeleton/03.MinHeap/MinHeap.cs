namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            this.ValidateIfNotempty();
            var oldRoot = this._elements[0];
            this.Swap(0, this._elements.Count - 1);
            this._elements.RemoveAt(this._elements.Count - 1);
            this.HeapifyDown(0);
            return oldRoot;
        }

        public void Add(T element)
        {
            this._elements.Add(element);
            HeapifyUp(this._elements.Count - 1);
        }

        public T Peek()
        {
            this.ValidateIfNotempty();
            return this._elements[0];
        }

        private void HeapifyDown(int index)
        {
            var leftChildIndex = 2 * index + 1;
            var rightChildIndex = 2 * index + 2;
            var minElementIndex = leftChildIndex;

            if (leftChildIndex >= this._elements.Count) return;

            if (rightChildIndex <= this._elements.Count - 1 && IsSmaller(rightChildIndex, leftChildIndex))
            {
                minElementIndex = rightChildIndex;
            }

            if (IsSmaller(minElementIndex, index))
            {
                this.Swap(index, minElementIndex);
                this.HeapifyDown(minElementIndex);
            }
        }

        private bool IsSmaller(int firstIndex, int secondIndex)
        {
            if (this._elements[firstIndex].CompareTo(this._elements[secondIndex]) < 0)
            {
                return true;
            }
            return false;
        }
        private void HeapifyUp(int index)
        {
            if (index == 0)
            {
                return;
            }

            int parentIndex = (index - 1) / 2;

            if (this._elements[index].CompareTo(this._elements[parentIndex]) < 0)
            {
                Swap(index, parentIndex);
                HeapifyUp(parentIndex);
            }
        }

        private void ValidateIfNotempty()
        {
            if (this._elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            T temp = this._elements[firstIndex];
            this._elements[firstIndex] = this._elements[secondIndex];
            this._elements[secondIndex] = temp;
        }
    }
}
