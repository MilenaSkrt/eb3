using System;
using System.Reflection;

namespace lab3
{
    public class Arr_list<T> : Base_list<T> where T : IComparable<T>
    {
        private T[] buffer; // массив для хранения элементов

        public Arr_list()
        {
            buffer = new T[0]; // инициализация массива
            count = 0; // инициализация счетчика элементов
        }

        private void Expand()
        {
            if (count == buffer.Length)
            {
                T[] newBuffer;
                if (buffer.Length == 0)
                {
                    newBuffer = new T[2]; // создание нового массива с увеличенным размером
                }
                else
                {
                    newBuffer = new T[buffer.Length * 2]; // создание нового массива вдвое большего размера
                }
                for (int i = 0; i < buffer.Length; i++)
                {
                    newBuffer[i] = buffer[i]; // копирование элементов из старого массива в новый
                }
                buffer = newBuffer; // замена старого массива на новый
            }
        }

        public override void Add(T item)
        {
            Expand(); // увеличение размера массива при необходимости
            buffer[count] = item; // добавление элемента
            count++; // увеличение счетчика
            OnItemChanged(); // вызов события изменения элемента
        }

        public override void Insert(int pos, T item)
        {
            if (pos < 0 || pos > count)
            {
                throw new BadIndexException(); // выброс исключения при некорректном индексе
            }

            Expand(); // увеличение размера массива
            for (int i = count; i > pos; i--)
            {
                buffer[i] = buffer[i - 1]; // сдвиг элементов для вставки нового элемента
            }

            buffer[pos] = item; // вставка нового элемента
            count++; // увеличение счетчика
            OnItemChanged(); // вызов события изменения элемента
        }

        public override void Delete(int pos)
        {
            if (pos < 0 || pos >= count)
            {
                throw new BadIndexException(); // выброс исключения при некорректном индексе
            }

            for (int i = pos; i < count - 1; i++)
            {
                buffer[i] = buffer[i + 1]; // сдвиг элементов для удаления элемента
            }
            count--; // уменьшение счетчика
            OnItemChanged(); // вызов события изменения элемента          
        }

        public override void Clear()
        {
            buffer = new T[0]; // очистка массива
            count = 0; // сброс счетчика
            OnItemChanged(); // вызов события изменения элемента
        }

        public override T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new BadIndexException(); // выброс исключения при некорректном индексе
                }
                return buffer[index]; // получение элемента по индексу
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new BadIndexException(); // выброс исключения при некорректном индексе
                }
                buffer[index] = value; // установка нового значения элемента
            }
        }

        protected override Base_list<T> EmptyClone()
        {
            return new Arr_list<T>(); // создание пустой копии
        }
        public override string ToString()
        {
            /*string str = "";
            for (int i = 0; i < count; i++)
            {
                str += buffer[i] + " ";
            }
            return str; */
            return string.Join(" ", buffer); // объединение элементов массива в строку
        }
    }
}
