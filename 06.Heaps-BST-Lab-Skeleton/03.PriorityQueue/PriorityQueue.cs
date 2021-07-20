namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> InternalList;

        public PriorityQueue()
        {
            this.InternalList = new List<T>();
        }

        public int Size { get { return InternalList.Count; } }

        public void Add(T element)
        {
            this.InternalList.Add(element);
            this.HeapifyUp(this.InternalList.Count - 1);
        }

        public T Dequeue()
        {
            this.ValidateIfNotempty();
            var oldRoot = this.InternalList[0];
            this.Swap(0, this.InternalList.Count - 1);
            this.InternalList.RemoveAt(this.InternalList.Count - 1);
            this.HeapifyDown(0);
            return oldRoot;
        }
        public T Peek()
        {
            this.ValidateIfNotempty();
            return this.InternalList[0];
        }

        private void ValidateIfNotempty()
        {
            if (this.InternalList.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }

        private void HeapifyDown(int index)
        {
            var leftChild = 2 * index + 1;
            var rigthChild = 2 * index + 2;

            if (index == this.InternalList.Count - 1 || leftChild > this.InternalList.Count - 1)
            {
                return;
            }

            if (this.InternalList.Count == 2 && IsBigger(leftChild, index))
            {
                this.Swap(index, leftChild);
            }

            if (rigthChild <= this.InternalList.Count - 1 && IsBigger(leftChild, rigthChild) && IsBigger(leftChild, index))
            {
                this.Swap(index, leftChild);
                this.HeapifyDown(leftChild);
            }
            else if (rigthChild <= this.InternalList.Count - 1 && IsBigger(rigthChild, leftChild) && IsBigger(rigthChild, index))
            {
                this.Swap(index, rigthChild);
                this.HeapifyDown(rigthChild);
            }
        }

        private bool IsBigger(int firstIndex, int secondIndex)
        {
            if (this.InternalList[firstIndex].CompareTo(this.InternalList[secondIndex]) > 0)
            {
                return true;
            }
            return false;
        }

        private void Swap(int firstindex, int secondIndex)
        {
            var temp = this.InternalList[firstindex];
            this.InternalList[firstindex] = this.InternalList[secondIndex];
            this.InternalList[secondIndex] = temp;
        }
        private void HeapifyUp(int index)
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
                HeapifyUp(parentIndex);
            }
        }

    }
}
