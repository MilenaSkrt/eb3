// Импорт пространства имен System
// Импорт пространства имен System.Reflection
using System;
using System.Reflection;

// Объявление пространства имен lab3
namespace lab3
{
    // Объявление обобщенного класса Arr_chain_list, который наследует класс Base_list и где T должно реализовать интерфейс IComparable
    public class Arr_chain_list<T> : Base_list<T> where T : IComparable<T>
    {
        // Приватное поле head, инициализированное значением null
        private Node head = null;

        // Объявление внутреннего класса Node
        public class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
            public Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }

        // Метод для поиска элемента по индексу
        private Node NodeFinder(int pos)
        {
            if (pos >= count) { return null; }
            int i = 0;
            Node Checker = head;
            while (Checker != null && i < pos)
            {
                Checker = Checker.Next;
                i++;
            }
            if (i == pos) { return Checker; }
            else { return null; }
        }

        // Метод для добавления элемента в конец списка
        public override void Add(T data)
        {
            if (head == null)
            {
                head = new Node(data, null);
            }
            else
            {
                Node lastNode = NodeFinder(count - 1);
                lastNode.Next = new Node(data, null);
            }
            count++;
            OnItemChanged();
        }

        // Метод для вставки элемента по индексу
        public override void Insert(int pos, T data)
        {
            if (pos < 0 || pos > count)
            {
                throw new BadIndexException();
            }
            if (pos == 0)
            {
                head = new Node(data, head);
            }
            else
            {
                Node prevNode = NodeFinder(pos - 1);
                prevNode.Next = new Node(data, prevNode.Next);
            }
            count++;
            OnItemChanged();
        }

        // Метод для удаления элемента по индексу
        public override void Delete(int pos)
        {
            if (pos < 0 || pos >= count)
            {
                throw new BadIndexException();
            }
            if (pos == 0)
            {
                head = head.Next;
            }
            else
            {
                Node prevNode = NodeFinder(pos - 1);
                prevNode.Next = prevNode.Next.Next;
            }
            count--;
            OnItemChanged();
        }

        // Метод очистки списка
        public override void Clear()
        {
            head = null;
            count = 0;
            OnItemChanged();
        }

        // Индексатор для получения и задания элемента по индексу
        public override T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new BadIndexException();
                }
                Node node = NodeFinder(index);
                return node.Data;
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new BadIndexException();
                }
                Node current = NodeFinder(index);
                current.Data = value;
            }
        }

        // Метод для создания клона списка
        protected override Base_list<T> EmptyClone()
        {
            return new Arr_chain_list<T>();
        }

        // Метод для сортировки списка
        public override void Sort()
        {
            if (count <= 1)
            {
                return;
            }

            T temp;
            // Сортировка пузырьком
            while (true)
            {
                bool t = true;
                Node current = head;

                while (current != null && current.Next != null)
                {
                    if (current.Data.CompareTo(current.Next.Data) > 0)
                    {
                        temp = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = temp;
                        t = false;
                    }
                    current = current.Next;
                }
                if (t)
                {
                    break;
                }
            }
        }

        // Метод для вывода списка в виде строки
        public override string ToString()
        {
            string str = "";
            Node current = head;
            while (current != null)
            {
                str += current.Data.ToString() + " ";
                current = current.Next;
            }
            return str.Trim();
        }
    }
}
