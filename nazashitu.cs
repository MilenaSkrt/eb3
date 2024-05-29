using System;

public class EvenNumberException : Exception
{
    public EvenNumberException(string message) : base(message)
    {
    }
}

public class OddNumberException : Exception
{
    public OddNumberException(string message) : base(message)
    {
    }
}

public class NumberProcessor
{
    public void ProcessNumber(int number)
    {
        if (number % 2 == 0)
        {
            throw new EvenNumberException("Even numbers are not allowed.");
        }
        else
        {
            throw new OddNumberException("Odd numbers are not allowed.");
        }
    }
}

class Program
{
    static void Main()
    {
        NumberProcessor numberProcessor = new NumberProcessor();

        try
        {
            numberProcessor.ProcessNumber(2);
        }
        catch (EvenNumberException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (OddNumberException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        try
        {
            numberProcessor.ProcessNumber(3);
        }
        catch (EvenNumberException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (OddNumberException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
