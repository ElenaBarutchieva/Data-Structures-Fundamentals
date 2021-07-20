namespace Problem02.Stack
{
    public class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }
    }
}