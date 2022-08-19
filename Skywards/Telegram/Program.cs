using CashFlowTelegramBot.Skywards.Telegram;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

var bot = new TelegramBotClient(TelegramToken.BotToken);

var me = await bot.GetMeAsync();
Console.Title = me.Username ?? "CashFlow";

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>(),
    ThrowPendingUpdates = true
};


bot.StartReceiving(UpdateHandlers.HandleUpdateAsync,
    UpdateHandlers.PollingErrorHandler,
    receiverOptions,
    cts.Token);
/*
FileStream filestream = null;
if (File.Exists("logs.txt"))
{
    filestream = new FileStream("logs.txt", FileMode.Open);
}
else
{
    filestream = new FileStream("logs.txt", FileMode.Create);
}

if (filestream != null)
{
    var streamwriter = new StreamWriter(filestream);
    streamwriter.AutoFlush = true;
    Console.SetOut(streamwriter);
    Console.SetError(streamwriter);
}
*/
Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();


// Send cancellation request to stop bot
cts.Cancel();