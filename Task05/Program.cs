using System;
using System.Globalization;

/*
Источник: https://metanit.com/

Класс Dollar представляет сумму в долларах, а Euro - сумму в евро.

Определите операторы преобразования от типа Dollar в Euro и наоборот.
Допустим, 1 евро стоит 1,14 долларов. При этом один оператор должен подразумевать явное,
и один - неявное преобразование. Обработайте ситуации с отрицательными аргументами
(в этом случае должен быть выброшен ArgumentException).

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки - количество долларов и количество евро.
10
100
Программа должна вывести на экран количество евро и долларов, соответственно,
с использованием перегруженных операторов (округлять до 2 знаков после запятой):
8,77
114,00

Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

namespace Task05
{

    class Dollar
    {
        public static decimal currency = 1.14M;

        public decimal Sum { get; set; }

        public override string ToString() => Sum.ToString("f2");

        public static explicit operator Dollar(Euro v) => new Dollar{Sum = v.Sum * currency};

        public static implicit operator Dollar(decimal v) => v >= 0 ? new Dollar { Sum = v } : throw new ArgumentException();
    }
    class Euro
    {   
        public static decimal currency = 1 / Dollar.currency; 

        public decimal Sum { get; set; }

        public override string ToString() => Sum.ToString("f2");

        public static explicit operator Euro(Dollar v) => new Euro { Sum = v.Sum * currency };

        public static implicit operator Euro(decimal v) => v >= 0 ? new Euro { Sum = v } : throw new ArgumentException();
    }

    class MainClass
    {

        public static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            try
            {

                Dollar dollar = decimal.Parse(Console.ReadLine());
                Euro euro = decimal.Parse(Console.ReadLine());

                Console.WriteLine((Euro)dollar);
                Console.WriteLine((Dollar)euro);


            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
            Console.ReadKey();
        }
    }
}
