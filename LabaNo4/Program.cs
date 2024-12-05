using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LabaNo4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// 1. Ввод двух списков с проверкой корректности

            Console.WriteLine("Введите элементы первого списка через пробел:");
            List<string> L1 = Lab.ReadListFromConsole<string>();

            Console.WriteLine("Введите элементы второго списка через пробел:");
            List<string> L2 = Lab.ReadListFromConsole<string>();

            Console.WriteLine("Результат объединения списков:");
            foreach (string item in Lab.Insert(L1, L2))
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Введите элементы списка через пробел:");
            string input = Console.ReadLine(); // Читаем строку из ввода
            string[] elements = input.Split(' '); // Разделяем строку на элементы

            // Создаём связный список на основе введённых элементов
            var list = new LinkedList<string>(elements);

            // Вызываем метод Sosed
            int result = Lab.Sosed(list);

            // Выводим результат
            Console.WriteLine($"Количество узлов, удовлетворяющих условию: {result}");


        }
    }


    class Lab
    {
        public static List<T> Insert<T>(List<T> L1, List<T> L2) where T : IComparable<T>
        {
            List<T> result = new List<T>();

            int index1 = 0, index2 = 0;

            while (index1 < L1.Count && index2 < L2.Count)
            {
                if (L1[index1].CompareTo(L2[index2]) <= 0) // Сравнение для обобщенного типа
                {
                    result.Add(L1[index1]);
                    index1++;
                }
                else
                {
                    result.Add(L2[index2]);
                    index2++;
                }
            }

            while (index1 < L1.Count)
            {
                result.Add(L1[index1]);
                index1++;
            }

            while (index2 < L2.Count)
            {
                result.Add(L2[index2]);
                index2++;
            }

            return result;
        }

        public static int Sosed<T>(LinkedList<T> list) // Метод работает с любыми типами данных
        {
            if (list == null || list.Count < 3) // Проверяем, что список не null и содержит минимум 3 элемента
            {
                return 0;
            }

            LinkedListNode<T> node = list.First.Next;
            int count = 0;

            while (node.Next != null)
            {
                if (EqualityComparer<T>.Default.Equals(node.Previous.Value, node.Next.Value))
                {
                    count++;
                }
                node = node.Next;
            }

            return count;
        }

        //nomer 3 

        public static void OrderAnalyze(string[] food, List<List<string>> orders,
                                   //out возвращает значения через эти параметры
                                   out HashSet<string> allOrders, 
                                   out HashSet<string> someOrders,
                                   out HashSet<string> noOrders)          
        {
            HashSet<string> allFood = new HashSet<string>(food); //allFood станет { "Рис", "Картошка", "Курица", "Селедка", "Пельмени", "Дранники" }

            allOrders = new HashSet<string>(orders[0]);
            someOrders = new HashSet<string>();

            foreach (List<string> order in orders) //проходимся по каждому заказу
            {
                HashSet<string> currOrder = new HashSet<string>(order);

                someOrders.UnionWith(currOrder); //объединение
                                                 //объединяем этот хешсет с текущим просматриваемым,
                                                 //чтобы записать новые виды еды,
                                                 //чтобы в итоге получился хешсет со всей едой,
                                                 //которую заказали хоть раз

                allOrders.IntersectWith(currOrder); //пересечение
                                                    //изменяем текйщий хешсет так,
                                                    //чтобы в нем оставались только те виды еды,
                                                    //которые есть в этом хешсете
                                                    //(в начале он содержит только 1-й заказ) 
                                                    //и в текущем заказе
            }

            noOrders = new HashSet<string>(allFood); //создаем хешсет со всеми видами еды
            noOrders.ExceptWith(someOrders); //удаляем все виды еды, которые заказали хоть раз
        }





        public static string Chars(string text)//nomer 4
        {
            HashSet<char> soglChars = new HashSet<char>("бвгджзйклмнпрстфхцчшщ");  //создаем коллекцию согл. букв
                                                                              //благодаря хешсету гарантируем хранение каждой буквы лиш 1 раз
                                                                              //вне зависимости от того, сколько она встречалась 

            // Хранение всех согласных и их встречаемости в словах
            Dictionary<char, int> soglCount = new Dictionary<char, int>(); //создаем словарь, в котором будем хранить кол-во встречь согл. букв в тексте
                                                                            //согл. буква - ключ а кол-во её встреч - значение
            HashSet<char> tempHeshSet = new HashSet<char>(); //множество уникальных согл. букв

            // Разделяем текст на слова
            string[] words = text
                .ToLower() //все буквы теперь маленькие
                .Split(new char[] { ' ', '.', ',', '!', '?', ':', ';', '\n', '\r', '-' }, StringSplitOptions.RemoveEmptyEntries);
                //разделяет текст на отдельные слова, используя пробелы и знаки препинания как разделители
                //StringSplitOptions.RemoveEmptyEntries - удаляет пустые строки если есть

            foreach (string word in words) //проходимся по всем словам
            {
                tempHeshSet.Clear(); //очищаем текущее мноржество, чтобы оно в каждом новом слове содержало уникальные согласные 

                foreach (char ch in word) //проходимся по всем символам
                {
                    if (soglChars.Contains(ch)) //если в слове есть согл. буква
                    {
                        tempHeshSet.Add(ch); //добавляем в текущее множество
                    }
                }

                //обновляем общий подсчет согласных
                foreach (char soglChar in tempHeshSet) //проходимся по всем согласным слова в текущекм множестве
                {
                    if (soglCount.ContainsKey(soglChar)) //проверяем есть ли уже в словаре такая согласная
                    {
                        soglCount[soglChar]++;  //если есть, то добавляем 1 к кол-ву её встреч
                    }
                    else
                    {
                        soglCount[soglChar] = 1; //если нет, то присваиваем её кол-ву встречь 1
                    }
                }
            }

            //получаем согласные, которые входят ровно в одно слово
            var result = soglCount
                //KeyValuePair - пара ключа-числа
                .Where(KeyValuePair => KeyValuePair.Value == 1) //если согл. буква встретилась ровно 1 раз
                .Select(KeyValuePair => KeyValuePair.Key) //извлекаем только ключ (согл. букву)
                .OrderBy(c => c);  //сортируем в алфавитном проядке

            string resultStr = "";

            // Выводим результат
            Console.WriteLine("Согласные буквы, входящие ровно в одно слово, в алфавитном порядке:");
            foreach (char soglChar in result)
            {
                resultStr += soglChar + " ";
            }
            return resultStr;
        }


        //метод для ввода списков для 1 и 2 с клавиатуры
        public static List<T> ReadListFromConsole<T>()
        {
            List<T> result = new List<T>();

            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string[] elements = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string element in elements)
                    {
                        result.Add((T)Convert.ChangeType(element, typeof(T))); // Преобразование строки в нужный тип
                    }

                    return result;
                }
                catch (Exception)
                {
                    Console.WriteLine($"Ошибка ввода! Убедитесь, что вводите данные корректного типа ({typeof(T).Name}). Повторите попытку:");
                    result.Clear(); // Очистка списка для повторного ввода
                }
            }
        }
    }
}





