```csharp
// Использование пространства имен System.Collections
using System.Collections;

namespace lab3{
    // Объявление абстрактного класса Base_list<T> с ограничением на тип T
    public abstract class Base_list<T> : IEnumerable<T> where T: IComparable<T>
    {  
        // Объявление делегата события ListChangedEventHandler
        public delegate void ListChangedEventHandler(object sender, ActionEventArgs e);

        // Защищенное поле счетчика
        protected int count;

        // Событие ItemChanged
        public event ListChangedEventHandler ItemChanged;

        // Свойство для получения значения счетчика
        public int Count 
        {
            get { return count; }
        }

        // Защищенный виртуальный метод вызова события ItemChanged
        protected virtual void OnItemChanged()
        {
            ItemChanged?.Invoke(this, new ActionEventArgs("Item Changed"));
        }

        // Абстрактные методы для добавления, вставки, удаления, очистки и получения элемента по индексу
        public abstract void Add(T item);
        public abstract void Insert(int pos, T item);
        public abstract void Delete(int pos);
        public abstract void Clear();
        public abstract T this[int i] { get; set; }

        // Метод для печати элементов
        public void Print()
        {
            foreach (var item in this)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        // Метод для присваивания значений из одного списка другому
        public void Assign(Base_list<T> source)
        {
            Clear();
            for (int i = 0; i < source.count; i++)
            {
                Add(source[i]);
            }
        }

        // Метод для присваивания значений этого списка другому
        public void AssignTo(Base_list<T> dest)
        {
            dest.Assign(this);
        }

        // Метод клонирования списка
        public Base_list<T> Clone()
        {
            Base_list<T> clone_list = EmptyClone();
            clone_list.Assign(this);
            return clone_list;
        }
        
        // Защищенный абстрактный метод для создания пустой копии списка
        protected abstract Base_list<T> EmptyClone();

        // Виртуальный метод сортировки списка
        public virtual void Sort()
        {
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (this[i].CompareTo(this[j]) > 0)
                    {
                        T temp = this[i];
                        this[i] = this[j];
                        this[j] = temp;
                    }
                }
            }
        }

        // Метод для сравнения двух списков
        public bool IsEqual(Base_list<T> AnotherOne)
        {
            if (AnotherOne == null)
                return false;

            if (this.Count != AnotherOne.Count)
                return false;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].CompareTo(AnotherOne[i]) != 0)
                    return false;
            }
            return true;
        }

        // Метод для записи списка в файл
        public void SaveToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine(this[i].ToString());
                }
            }
        }

        // Метод для загрузки списка из файла
        public void LoadFromFile(string fileName)
        {
            Clear();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Trim() == "")
                    {
                        T item = (T)Convert.ChangeType(line, typeof(T));
                        Add(item);
                    }
                }
            }
        }

        // Перегруженные операторы для сравнения двух списков и объединения списков
        public static bool operator ==(Base_list<T> list1, Base_list<T> list2)
        {
            return list1.IsEqual(list2);
        }

        public static bool operator !=(Base_list<T> list1, Base_list<T> list2)
        {
            return !list1.IsEqual(list2);
        }
        
        public static Base_list<T> operator +(Base_list<T> list1, Base_list<T> list2)
        {
            Base_list<T> list3 = list1.Clone();
            for (int i = 0; i < list2.Count; i++)
            {
                list3.Add(list2[i]);
            }
            return list3;
        }

        // Делегат счетчика исключений и вспомогательные классы исключений
        /*
        public class ExceptionCounter
        {
            protected static int ChainExceptionCount = 0;
            protected static int ArrayExceptionCount = 0;
           
            public static int ChainExceptionCounter
            {
                get { return ChainExceptionCount; }
            }
            public static int ArrayExceptionCounter
            {
                get { return ArrayExceptionCount; }
            }
            public static void ArrayExceptionCounterIncrement()
            {
                ArrayExceptionCount++;
            }
            public static void ChainExceptionCounterIncrement()
            {
                ChainExceptionCount++;
            }
        }*/

        // Методы для добавления обработчиков событий для разных типов списков
        public void AddArrayListEventHandler()
        {
            ItemChanged += (sender, e) => Console.WriteLine("Array list changed");
        }
        public void AddChainListEventHandler()
        {
            ItemChanged += (sender, e) => Console.WriteLine("Chain list changed");
        }

        // Реализация интерфейса IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return new Base_listEnumerator(this);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Вложенный класс Base_listEnumerator для перечисления элементов списка
        private class Base_listEnumerator : IEnumerator<T>
        {
            private Base_list<T> list;
            private int currentIndex = -1;

            public Base_listEnumerator(Base_list<T> list)
            {
                this.list = list;
            }

            public T Current => list[currentIndex];

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                currentIndex++;
                return currentIndex < list.Count;
            }

            public void Reset()
            {
                currentIndex = -1;
            }

            public void Dispose()
            {
            }
        }
    }

    // Класс исключения для недопустимого индекса
    public class BadIndexException : Exception
    {
        public BadIndexException() : base("exception")
        {
        }
    }       

    // Класс исключения для некорректного файла
    public class BadFileException : Exception
    {
        public BadFileException() : base("exception")
        {
        }
    }

    // Класс аргументов события
    public class ActionEventArgs : EventArgs
    {
        public string Action { get; private set; }

        public ActionEventArgs(string action)
        {
            Action = action;
        }
    }
}
```
