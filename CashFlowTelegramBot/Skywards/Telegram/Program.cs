using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards;
using CashFlowTelegramBot.Skywards.Telegram;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

TelegramBotClient bot = new TelegramBotClient(TelegramToken.BotToken);

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
Trace.Listeners.Clear();

string path = null;
if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
        @"logs.txt");
}

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
        @"logs.txt");
}


TextWriterTraceListener twtl = new TextWriterTraceListener(path);
twtl.Name = "TextLogger";
twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

ConsoleTraceListener ctl = new ConsoleTraceListener(false);
ctl.TraceOutputOptions = TraceOptions.DateTime;

Trace.Listeners.Add(twtl);
Trace.Listeners.Add(ctl);
Trace.AutoFlush = true;

Trace.WriteLine("\nThe first line to be in the logfile and on the console.");


/*
if (filestream != null)
{
    var streamwriter = new StreamWriter(filestream);
    streamwriter.AutoFlush = true;
    Console.SetOut(streamwriter);
    Console.SetError(streamwriter);
}
*/

Console.WriteLine($"\nStart listening for @{me.Username}");
Console.ReadLine();


// Send cancellation request to stop bot
cts.Cancel();