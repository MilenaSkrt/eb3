

namespace lab3 
{
    class Program 
    {
        static void Main(string[] args) //объявление метода Main
        {
            int arr_count_except = 0; //объявление переменной arr_count_except и присвоение ей значения 0
            int chain_count_except = 0; //объявление переменной chain_count_except и присвоение ей значения 0
            Base_list<char> array = new Arr_list<char>(); //создание объекта array типа Base_list<char> и инициализация его новым объектом Arr_list<char>()
            Base_list<char> chain = new Arr_chain_list<char>(); //создание объекта chain типа Base_list<char> и инициализация его новым объектом Arr_chain_list<char>()
            Random rnd = new Random(); //создание объекта rnd типа Random
            for (int i = 0; i < 1000; i++) //цикл с переменной i от 0 до 999
            {
                int operation = rnd.Next(5); //присвоение переменной operation случайного значения от 0 до 4
                char item = (char)('a' + rnd.Next(0, 26)); //присвоение переменной item случайного символа в диапазоне от 'a' до 'z'
                int pos = rnd.Next(50); //присвоение переменной pos случайного значения от 0 до 49
                switch (operation) //оператор выбора в зависимости от значения operation
                {
                    case 0: 
                        array.Add(item); //добавление элемента в array
                        chain.Add(item); //добавление элемента в chain
                        break; //выход из switch
                    case 1: 
                        try //попытка выполнить следующий блок кода
                        {
                            array.Delete(pos); //удаление элемента из array
                        }
                        catch (BadIndexException) //перехват исключения типа BadIndexException
                        {
                            arr_count_except++; //увеличение значения переменной arr_count_except
                            Console.WriteLine("Delete array exception caught"); //вывод сообщения об исключении при удалении из array
                        }
                        try //попытка выполнить следующий блок кода
                        {
                            chain.Delete(pos); //удаление элемента из chain
                        }
                        catch (BadIndexException) //перехват исключения типа BadIndexException
                        {
                            chain_count_except++; //увеличение значения переменной chain_count_except
                            Console.WriteLine("Delete chain exception caught"); //вывод сообщения об исключении при удалении из chain
                        }
                        break; //выход из switch
                    case 2: 
                        try //попытка выполнить следующий блок кода
                        {
                            array.Insert(pos, item); //вставка элемента в array
                        }
                        catch (BadIndexException) //перехват исключения типа BadIndexException
                        {
                            arr_count_except++; //увеличение значения переменной arr_count_except
                            Console.WriteLine("Insert array exception caught"); //вывод сообщения об исключении при вставке в array
                        }
                        try //попытка выполнить следующий блок кода
                        {
                            chain.Insert(pos, item); //вставка элемента в chain
                        }
                        catch (BadIndexException) //перехват исключения типа BadIndexException
                        {
                            chain_count_except++; //увеличение значения переменной chain_count_except
                            Console.WriteLine("Insert chain exception caught"); //вывод сообщения об исключении при вставке в chain
                        }
                        break; //выход из switch
                    case 3: 
                        array.Clear(); //очистка array
                        chain.Clear(); //очистка chain
                        break; //выход из switch
                    case 4: 
                        try //попытка выполнить следующий блок кода
                        {
                            array[pos] = item; //присвоение элементу по индексу в array нового значения
                        }
                        catch (BadIndexException) //перехват исключения типа BadIndexException
                        {
                            arr_count_except++; //увеличение значения переменной arr_count_except
                            Console.WriteLine("Set array exception caught"); //вывод сообщения об исключении при установке значения в array
                        }
                        try //попытка выполнить следующий блок кода
                        {
                            chain[pos] = item; //присвоение элементу по индексу в chain нового значения
                        }
                        catch (BadIndexException) //перехват исключения типа BadIndexException
                        {
                            chain_count_except++; //увеличение значения переменной chain_count_except
                            Console.WriteLine("Set chain exception caught"); //вывод сообщения об исключении при установке значения в chain
                        }
                        break; //выход из switch
                }
            }

           
            /*
            bool flag = true;
            if (array.Count == chain.Count)
            {
                for (int i = 0; i < chain.Count; i++)
                {
                    if (array[i] != chain[i])
                    {
                        Console.WriteLine("Test error");
                        flag = false;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Test error");
                flag = false;
            }
            if (flag == true)
            {
                Console.WriteLine("Test successfull");
            }
            Console.WriteLine(arr_count_except);
            Console.WriteLine(chain_count_except);
            */

            
            /*if (array.IsEqual(chain))
            {
                Console.WriteLine("Test successfull lists are equal
");
            }
            else
            {
                Console.WriteLine("Test error lists are not equal
");
            }*/

            //File test
            array.Clear(); //очистка array
            array.Add('1'); //добавление элемента в array
            array.Add('2'); //добавление элемента в array
            array.Add('6'); //добавление элемента в array
            array.Add('4'); //добавление элемента в array
            array.Add('5'); //добавление элемента в array
            array.Sort(); //сортировка array
            Console.WriteLine("Элементы ArrayList: "); //вывод сообщения
            array.Print(); //вывод элементов array
            string filename = "test_lab3.txt"; //присвоение переменной значения "test_lab3.txt"
            array.SaveToFile(filename); //сохранение данных из array в файл с именем filename
            Base_list<char> clone = array.Clone(); //создание объекта clone и копирование данных из array
            Console.WriteLine($"From file {filename}: "); //вывод сообщения
            clone.Print(); //вывод данных из clone
            clone.LoadFromFile(filename); //загрузка данных из файла в clone

          
            /*
            // == != + test
            Console.WriteLine("== != + test");
            if (array == chain){Console.WriteLine("ArrayList == ChainList
");}
            else{Console.WriteLine("ArrayList != ChainList
");}
            
            if (array != chain){Console.WriteLine("ArrayList != ChainList
");}
            else{Console.WriteLine("ArrayList == ChainList
");}
            */

            clone.Clear(); //очистка clone
            array.Print(); //вывод элементов array
            chain.Print(); //вывод элементов chain
            clone = array + chain; //сложение array и chain, результат в clone
            clone.Print(); //вывод элементов из clone

            Console.WriteLine($"Exception Array: {arr_count_except}"); //вывод сообщения с количеством исключений в array
            Console.WriteLine($"Exception Chain: {chain_count_except}"); //вывод сообщения с количеством исключений в chain
        }
    }
}
