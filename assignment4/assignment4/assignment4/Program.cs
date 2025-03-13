using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment4_1
{
    public class LinkedListNode<T>
    {
        public LinkedListNode<T> Next { get; set; }
        public T Value { get; set; }

        public LinkedListNode(T value)
        {
            Next = null;
            Value = value;
        }
    }

    public class LinkedList<T>
    {
        private LinkedListNode<T> _head;
        private LinkedListNode<T> _tail;

        public LinkedList()
        {
            _tail = _head = null;
        }

        public LinkedListNode<T> Head
        {
            get => _head;
        }

        public void Add(T item)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(item);
            if (_tail == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                _tail = newNode;
            }
        }

        public void ForEach(Action<T> action)
        {
            LinkedListNode<T> currentNode = _head;
            while (currentNode != null)
            {
                action(currentNode.Value);
                currentNode = currentNode.Next;
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            LinkedList<int> numberList = new LinkedList<int>();

            for (int currentNumber = 0; currentNumber < 10; currentNumber++)
            {numberList.Add(currentNumber); }

            Console.WriteLine("链表元素：");
            numberList.ForEach(item => Console.WriteLine(item));

            int maximum = int.MinValue;
            numberList.ForEach(item =>
            {if (item > maximum) maximum = item; });
            Console.WriteLine("最大值: " + maximum);

            int minimum = int.MaxValue;
            numberList.ForEach(item =>
            { if (item < minimum) minimum = item;});
            Console.WriteLine("最小值: " + minimum);
             int total = 0;
            numberList.ForEach(item => total += item);
            Console.WriteLine("总和: " + total);
        }
    }
}