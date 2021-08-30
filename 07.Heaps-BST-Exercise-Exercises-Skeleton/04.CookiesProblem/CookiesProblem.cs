
using System;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            MinHeap<int> heap = new MinHeap<int>();
            for (int i = 0; i < cookies.Length; i++)
            {
                heap.Add(cookies[i]);
            }

            var smallestEl = heap.Peek();
            int steps = 0;
            while (smallestEl < k && heap.Size > 1)
            {
                var smallestCookie = heap.Dequeue();
                var secondSmallestCookie = heap.Dequeue();
                steps++;
                heap.Add(smallestCookie + 2 * secondSmallestCookie);
                smallestEl = heap.Peek();
            }
            return smallestEl >= k ? steps : -1;
        }
    }
}
