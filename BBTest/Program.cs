using System;
using System.Collections.Generic;
using System.Linq;

namespace BBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int count;

            Console.WriteLine(function0(12346789));
            Console.WriteLine(function1("69541 584 689 1123 123 6547 1529 35487"));
            function2("69541 584 689 1123 123 6547 1529 35487");

            Console.ReadKey();
        }

        /*Вычисляет произведение цифр числа и повторяет процедуру с полученными произведениями
        до тех пор, пока в результате не получится 0. Результат - количество проходов*/
        static int function0(int n)
        {
            string number = n.ToString();
            int[] digits;           //для хранения цифр числа
            int temp, count = 0;
            if (number.Length > 1)
            {
                while (number.Length > 1)
                {
                    digits = new int[number.Length];

                    for (int i = 0; i < number.Length; i++)
                    {
                        digits[i] = n % 10;
                        n /= 10;
                    }

                    temp = 1;       //для хранения промежуточного результата умножения

                    for (int i = 0; i < digits.Count(); i++)
                        temp *= digits[i];


                    n = temp;
                    number = n.ToString();
                    count++;
                }
                count++;            //это заменяет шаг, где в примере число умножается на единицу
                return count;
            }
            else                    //если число состоит из одной цифры
                return 0;
        }

        //без использования классов
        static string function1(string s)
        {
            string result = "";                          //итоговая строка
            List<string> values = new List<string>();    //значения
            List<int> weights = new List<int>();         //веса

            values = s.Split(' ').ToList();              //строка делится на отдельные элементы по пробелу
            for (int i = 0; i < values.Count; i++)
            {
                int temp = int.Parse(values[i]);         //переменная для промежуточных вычислений

                List<int> digits = new List<int>();      //цифры, из которых состоит число
                while (temp > 0)
                {
                    digits.Add(temp % 10);
                    temp /= 10;
                }
                for (int j = 0; j < digits.Count; j++)
                    temp += digits[j];
                weights.Add(temp);
            }

            //использование кортежа для объединения данных в одну сущность
            List<Tuple<string, int>> numbers = new List<Tuple<string, int>>();
            for (int i = 0; i < values.Count; i++)
                numbers.Add(new Tuple<string, int>(values[i], weights[i]));

            numbers = numbers.OrderBy(n => n.Item1).ToList();   //конструкция OrderBy().ThenBy() отказалась работать
            numbers = numbers.OrderBy(n => n.Item2).ToList();   //поэтому проще было сделать так

            for (int i = 0; i < numbers.Count; i++)
                result += numbers[i].Item1 + " ";
            return result;
        }

        //с использованием классов
        static string function2(string s)
        {
            string result = "";
            try
            {
                List<string> stringNumbers = new List<string>();    //строковые представления
                List<Number> numbers = new List<Number>();          //лист чисел, каждое из которых хранит строковое, числовое представление и вес
                stringNumbers = s.Split(' ').ToList();
                for (int i = 0; i < stringNumbers.Count; i++)
                {
                    int temp = int.Parse(stringNumbers[i]);         //переменная для промежуточных вычислений
                    if (temp < 0)
                    {
                        Console.WriteLine("Строка должна состоять из положительных чисел.");
                        return "Ошибка!";
                    }

                    List<int> digits = new List<int>();             //цифры, из которых состоит число
                    while (temp > 0)
                    {
                        digits.Add(temp % 10);
                        temp /= 10;
                    }
                    for (int j = 0; j < digits.Count; j++)
                        temp += digits[j];
                    numbers.Add(new Number(stringNumbers[i], temp));
                }

                numbers = numbers.OrderBy(n => n.StringValue).ToList(); //конструкция OrderBy().ThenBy() отказалась работать
                numbers = numbers.OrderBy(n => n.Power).ToList();       //поэтому проще было сделать так

                for (int i = 0; i < numbers.Count(); i++)
                {
                    result += numbers[i].StringValue + " ";
                }
                Console.WriteLine(result);
            }
            catch
            {
                result = "Ошибка!";
                Console.WriteLine("Строка должна состоять из целых положительных чисел, записанных через пробел.");
            }
            return result;
        }
    }
}
