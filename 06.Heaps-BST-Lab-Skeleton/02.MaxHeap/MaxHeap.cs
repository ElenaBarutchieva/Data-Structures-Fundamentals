namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> InternalList;

        public MaxHeap()
        {
            this.InternalList = new List<T>();
        }
        public int Size { get { return this.InternalList.Count; } }

        public void Add(T element)
        {
            this.InternalList.Add(element);
            Heapify(this.InternalList.Count - 1);
        }

        public T Peek()
        {
            this.ValidateIfNotempty();
            return this.InternalList[0];
        }

        private void Heapify(int index)
        {
            if (index == 0)
            {
                return;
            }

            int parentIndex = (index - 1) / 2;

            if (this.InternalList[index].CompareTo(this.InternalList[parentIndex]) > 0)
            {
                T temp = this.InternalList[index];
                this.InternalList[index] = this.InternalList[parentIndex];
                this.InternalList[parentIndex] = temp;
                Heapify(parentIndex);
            }
        }

        private void ValidateIfNotempty()
        {
            if (this.InternalList.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
