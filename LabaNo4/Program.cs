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
            // 1. Ввод двух списков с проверкой корректности
            Console.WriteLine("Введите элементы первого списка через пробел:");
            List<int> L1 = Lab.ReadListFromConsole();

            Console.WriteLine("Введите элементы второго списка через пробел:");
            List<int> L2 = Lab.ReadListFromConsole();

            Console.WriteLine("Результат объединения списков:");
            foreach (int i in Lab.Insert(L1, L2))
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            // 2. Ввод связного списка с проверкой корректности
            Console.WriteLine("Введите элементы связного списка через пробел:");
            LinkedList<int> L = new LinkedList<int>(Lab.ReadListFromConsole());

            Console.WriteLine("Результат подсчета одинаковых соседей:");
            Console.WriteLine(Lab.Sosed(L));



            ////1

            //List<int> L1 = new List<int>(new int[5] { 1, 2, 5, 6, 7 });
            //List<int> L2 = new List<int>(new int[3] { 3, 4, 8 });
            //foreach (int i in Lab.Insert(L1, L2))
            //{
            //    Console.Write(i + " ");
            //}
            //Console.WriteLine();

            ////2

            //LinkedList<int> L = new LinkedList<int>(new int[6] { 1, 2, 1, 5, 6, 5 });
            //Console.WriteLine(Lab.Sosed(L));

            //3


            // Заданный массив блюд
            string[] dishes = { "Рис", "Картошка", "Курица", "Селедка", "Пельмени", "Дранники" };

            // Список заказов 
            List<List<string>> orders = new List<List<string>>
            {
                new List<string> { "Рис", "Картошка" },
                new List<string> { "Картошка", "Курица", "Селедка" },
                new List<string> { "Картошка", "Пельмени" }
            };

            HashSet<string> allOrders;
            HashSet<string> someOrders;
            HashSet<string> noOrders;

            Lab.OrderAnalyze(dishes, orders, out allOrders, out someOrders, out noOrders);

            Console.WriteLine("Все посетители заказали:");
            foreach (string food in allOrders)
            {
                Console.WriteLine(food);
            }

            Console.WriteLine("\nХоть один посетитель заказал:");
            foreach (string food in someOrders)
            {
                Console.WriteLine(food);
            }

            Console.WriteLine("\nНикто не заказал:");
            foreach (string food in noOrders)
            {
                Console.WriteLine(food);
            }


            //4

            string filePath = "text.txt";
            string text = File.ReadAllText(filePath); //читает все содержимое файла в строку text
            Console.WriteLine(Lab.Chars(text));
        }
    }


    class Lab
    {
        public static List<int> Insert(List<int> L1, List<int> L2) //nomer 1
        {
            List<int> l = new List<int>(); //новый список в котором будут содержаться все элементы 2-х спискаов по возростанию

            int index1 = 0, index2 = 0; //начинаем просмотр с 0 элементов
            while (index1 < L1.Count && index2 < L2.Count) //пока индекс 1-го и 2-го списка мельше длинны этих списков
            {
                if (L1[index1] <= L2[index2]) //если эелемент 1-го списка не привышает соответствующий элемент 2-го списка
                {
                    l.Add(L1[index1]); //добавляем в новый список меньший элемент (элемент 1-го списка)
                    index1++; //увиличиваем читаемый индекс первого списка на 1
                }
                else
                {
                    l.Add(L2[index2]); //в ином случае добавляем соответствующий элемент 2-го списка
                    index2++; //увиличиваем читаемый индекс 2 списка на 1
                }
            }
            //когда этот цикл заканчивается начинается следующий 

            while (index1 < L1.Count) //пока индекс 1 списка менльше длинны списка
            {
                l.Add(L1[index1]); //добавляем элемент 1 списка
                index1++; //увиличиваем читаемый индекс первого списка на 1
            }
            //когда этот цикл заканчивается начинается следующий 

            while (index2 < L2.Count)//пока индекс 2 списка менльше длинны списка
            {
                l.Add(L2[index2]); //добавляем элемент 2 списка
                index2++; //увиличиваем читаемый индекс 2 списка на 1
            }
            return l;
        }

        public static int Sosed(LinkedList<int> L) //nomer 2
        {
            LinkedListNode<int> node = L.First.Next;
            int count = 0;
            while (node.Next != null)
            {
                if (node.Previous.Value == node.Next.Value)
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
        public static List<int> ReadListFromConsole()
        {
            List<int> result = new List<int>();

            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string currentNumber = "";
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (input[i] == ' ') //если текущий символ - пробел 
                        {
                            if (!string.IsNullOrEmpty(currentNumber)) //проверяем есть ли currentNumber какое-либо число 
                            {
                                result.Add(int.Parse(currentNumber)); //преобразуем строку в число
                                currentNumber = ""; //очищаем временную переменную
                            }
                        }
                        else
                        {
                            currentNumber += input[i]; //если символ не пробел, добавляем к текущемму числу
                        }
                    }

                    //добавление последнего числа (если есть)
                    if (!string.IsNullOrEmpty(currentNumber)) //проверяем есть ли ещё числа
                    {
                        result.Add(int.Parse(currentNumber)); //добавляем, если есть
                    }

                    return result;
                }
                catch (Exception)
                {
                    Console.WriteLine("Ошибка ввода! Убедитесь, что вводите только целые числа, разделенные пробелами. Повторите попытку:");
                    result.Clear(); //Очистка списка для повторного ввода
                }
            }
        }
    }
}





