using Telegram.Bot.Exceptions;

namespace CashFlowTelegramBot.Skywards;

public class Exceptions
{
    public static bool HandleException(Exception ex)
    {
        if (ex is AggregateException)
        {
            Console.WriteLine("Handling: {0}", ex.Message);
            return true;
        }

        if (ex is ApiRequestException)
        {
            Console.WriteLine("Handling: {0}", ex.Message);
            return true;
        }

        Console.WriteLine("Not handling: {0}", ex.Message);
        return false;
    }
}