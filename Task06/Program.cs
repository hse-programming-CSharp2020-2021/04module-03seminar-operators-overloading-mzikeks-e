using System;

/*
Источник: https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/operator-overloading

Fraction - упрощенная структура, представляющая рациональное число.
Необходимо перегрузить операции:
+ (бинарный)
- (бинарный)
*
/ (в случае деления на 0, выбрасывать DivideByZeroException)

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки, содержацие числители и знаменатели двух дробей, разделенные /, соответственно.
1/3
1/6
Программа должна вывести на экран сумму, разность, произведение и частное двух дробей, соответственно,
с использованием перегруженных операторов (при необходимости, сокращать дроби):
1/2
1/6
1/18
2

Обратите внимание, если дробь имеет знаменатель 1, то он уничтожается (2/1 => 2). Если дробь в числителе имеет 0, то 
знаменатель также уничтожается (0/3 => 0).
Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

public readonly struct Fraction
{
    private readonly int num;
    private readonly int den;


    public Fraction(int numerator, int denomenator)
    {
        int gcdThis = GCD(Math.Abs(numerator), Math.Abs(denomenator));
        numerator /= gcdThis;
        denomenator /= gcdThis;
        if (denomenator < 0)
        {
            denomenator = -denomenator;
            numerator = -numerator;
        }

        this.num = numerator;
        this.den = denomenator;

    }

    private static int GCD(int one, int two)
    {
        while (one != 0 && two != 0)
        {
            if (one > two) one %= two;
            else two %= one;
        }
        return Math.Max(1, one + two);
    }

    public override string ToString() => den == 1 ? num.ToString(): $"{num}/{den}";

    public static Fraction operator +(Fraction one, Fraction two)
    {
        return new Fraction
        (
            one.num * two.den + two.num * one.den,
            one.den * two.den
        );
    }

    public static Fraction operator -(Fraction one, Fraction two)
    {
        return new Fraction
        (
            one.num * two.den - two.num * one.den,
            one.den * two.den
        );
    }

    public static Fraction operator *(Fraction one, Fraction two)
    {
        return new Fraction
        (
            one.num * two.num,
            one.den * two.den
        );
    }

    public static Fraction operator /(Fraction one, Fraction two)
    {
        if (two.num == 0) throw new DivideByZeroException();
        if (one.num == 0 && two.num == 0) return new Fraction(1, 1);
        return new Fraction
        (
            one.num * two.den,
            one.den * two.num
        );
    }
}

public static class OperatorOverloading
{
    public static void Main()
    {
        try
        {
            var inp1 = Console.ReadLine().Split('/');
            var inp2 = Console.ReadLine().Split('/');

            Fraction r1 = new Fraction(int.Parse(inp1[0]), int.Parse(inp1[1]));
            Fraction r2 = new Fraction(int.Parse(inp2[0]), int.Parse(inp2[1]));
            Console.WriteLine(r1 + r2);
            Console.WriteLine(r1 - r2);
            Console.WriteLine(r1 * r2);
            Console.WriteLine(r1 / r2);

        }
        catch (ArgumentException)
        {
            Console.WriteLine("error");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("zero");
        }
        Console.ReadKey();
    }
}
