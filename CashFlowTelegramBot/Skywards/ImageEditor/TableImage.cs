using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards.Telegram;
using CashFlowTelegramBot.Skywards.Web;
using SkiaSharp;

namespace CashFlowTelegramBot.Skywards.ImageEditor;

public static class TableImage
{
    public static string TruncateLongString(this string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str)) return str;
        var end = "";
        if (str.Length > maxLength) end = "...";
        return str.Substring(0, Math.Min(str.Length, maxLength)) + end;
    }

    public static async Task<Stream> CreateTableImage(TableProfile TableProfile, UserProfile userData)
    {
        string? imageFilePath = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            imageFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/Tables/");
            imageFilePath += TableProfile.tableType + ".png";
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            imageFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\Tables\");
            imageFilePath += TableProfile.tableType + ".png";
        }

        /*using (Stream
               stream = System.IO.File.OpenRead(imageFilePath))*/
        SKBitmap bitmap = SKBitmap.FromImage(SKImage.FromEncodedData(imageFilePath));
        var canvas = new SKCanvas(bitmap);
        var bankerPoint = new SKPoint(x: 960, y: 110);
        var managerAPoint = new SKPoint(x: 640, y: 610);
        var managerBPoint = new SKPoint(x: 1295, y: 610);
        var giverAPoint = new SKPoint(x: 195, y: 245);
        var giverBPoint = new SKPoint(x: 195, y: 610);
        var giverCPoint = new SKPoint(x: 1740, y: 610);
        var giverDPoint = new SKPoint(x: 1740, y: 245);
        var paint = new SKPaint
        {
            TextSize = 30,
            IsAntialias = true,
            FakeBoldText = true,
            Color = SKColors.Black,
            IsStroke = false
        };
        var paintForUser = new SKPaint
        {
            TextSize = 30,
            IsAntialias = true,
            FakeBoldText = true,
            Color = SKColors.Red,
            IsStroke = false
        };
        paint.TextAlign = SKTextAlign.Center;
        paintForUser.TextAlign = SKTextAlign.Center;
        if (TableProfile.bankerID != null)
        {
            var bankerName = await WebManager.SendData(new UserProfile(TableProfile.bankerID),
                WebManager.RequestType.GetUserData, true);

            if (TableProfile.bankerID == userData.id)
                canvas.DrawText("@" + bankerName.playerData.username.TruncateLongString(8), bankerPoint, paintForUser);
            else canvas.DrawText("@" + bankerName.playerData.username.TruncateLongString(8), bankerPoint, paint);
        }

        if (TableProfile.giverA_ID != null)
        {
            var giverAUsername = await WebManager.SendData(new UserProfile(TableProfile.giverA_ID),
                WebManager.RequestType.GetUserData, true);
            if (TableProfile.giverA_ID == userData.id)
                canvas.DrawText("@" + giverAUsername.playerData.username.TruncateLongString(8), giverAPoint,
                    paintForUser);
            else canvas.DrawText("@" + giverAUsername.playerData.username.TruncateLongString(8), giverAPoint, paint);
        }

        if (TableProfile.giverB_ID != null)
        {
            var giverBUsername = await WebManager.SendData(new UserProfile(TableProfile.giverB_ID),
                WebManager.RequestType.GetUserData, true);
            if (TableProfile.giverB_ID == userData.id)
                canvas.DrawText("@" + giverBUsername.playerData.username.TruncateLongString(8), giverBPoint,
                    paintForUser);
            else canvas.DrawText("@" + giverBUsername.playerData.username.TruncateLongString(8), giverBPoint, paint);
        }

        if (TableProfile.giverC_ID != null)
        {
            var giverCUsername = await WebManager.SendData(new UserProfile(TableProfile.giverC_ID),
                WebManager.RequestType.GetUserData, true);
            if (TableProfile.giverC_ID == userData.id)
                canvas.DrawText("@" + giverCUsername.playerData.username.TruncateLongString(8), giverCPoint,
                    paintForUser);
            else canvas.DrawText("@" + giverCUsername.playerData.username.TruncateLongString(8), giverCPoint, paint);
        }

        if (TableProfile.giverD_ID != null)
        {
            var giverDUsername = await WebManager.SendData(new UserProfile(TableProfile.giverD_ID),
                WebManager.RequestType.GetUserData, true);
            if (TableProfile.giverD_ID == userData.id)
                canvas.DrawText("@" + giverDUsername.playerData.username.TruncateLongString(8), giverDPoint,
                    paintForUser);
            else canvas.DrawText("@" + giverDUsername.playerData.username.TruncateLongString(8), giverDPoint, paint);
        }

        canvas.Flush();
        var resultImage = SKImage.FromBitmap(bitmap);
        var data = resultImage.Encode(SKEncodedImageFormat.Png, 100);
        return data.AsStream();

        //return Convert.ToBase64String(data.ToArray());
        //Console.WriteLine("\nInfo: " + bitmap.Info.BytesPerPixel + "\nHeight: " + bitmap.Height + "\nWidth: " + bitmap.Width);
        //return image;
    }
}