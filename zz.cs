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
