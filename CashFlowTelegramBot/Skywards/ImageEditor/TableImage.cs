using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards.Telegram;
using CashFlowTelegramBot.Skywards.Web;
using SkiaSharp;

namespace CashFlowTelegramBot.Skywards.ImageEditor;

public class TableImage
{
    
    public static async Task<Stream> CreateTableImage(TableProfile TableProfile)
    {
        string? imageFilePath  = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            imageFilePath  = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/Tables/");
            imageFilePath  += TableProfile.tableType + ".png";
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            imageFilePath  = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\Tables\");
            imageFilePath  += TableProfile.tableType + ".png";
        }

        /*using (Stream
               stream = System.IO.File.OpenRead(imageFilePath))*/
            SKBitmap bitmap = SKBitmap.FromImage(SKImage.FromEncodedData(imageFilePath));
            var canvas = new SKCanvas(bitmap);
            var bankerPoint = new SKPoint(x:960, y:110);
            var managerAPoint = new SKPoint(x:640, y:610);
            var managerBPoint = new SKPoint(x:1295, y:610);
            var giverAPoint = new SKPoint(x:195, y:245);
            var giverBPoint = new SKPoint(x:195, y:610);
            var giverCPoint = new SKPoint(x:1740, y:610);
            var giverDPoint = new SKPoint(x:1740, y:245);
            var paint = new SKPaint
            {
                TextSize = 40,
                IsAntialias = true,
                Color = SKColors.Black,
                IsStroke = false
            };
            paint.TextAlign = SKTextAlign.Center;
            if (TableProfile.bankerID != null)
            {
                var bankerName = await WebManager.SendData(new UserProfile((int) TableProfile.bankerID),
                    WebManager.RequestType.GetUserData, true);
                canvas.DrawText("@" + bankerName.playerData.username, bankerPoint, paint);
            }
            if (TableProfile.managerA_ID != null)
            {
                var managerAUsername = await WebManager.SendData(new UserProfile((int) TableProfile.managerA_ID),
                    WebManager.RequestType.GetUserData, true);
                canvas.DrawText("@" + managerAUsername.playerData.username, managerAPoint, paint);
            }
            if (TableProfile.managerB_ID != null)
            {
                var managerBUsername = await WebManager.SendData(new UserProfile((int) TableProfile.managerB_ID),
                    WebManager.RequestType.GetUserData, true);
                canvas.DrawText("@" + managerBUsername.playerData.username, managerBPoint, paint);
            }
            if (TableProfile.giverA_ID != null)
            {
                var giverAUsername = await WebManager.SendData(new UserProfile((int) TableProfile.giverA_ID),
                    WebManager.RequestType.GetUserData, true);
                canvas.DrawText("@" + giverAUsername.playerData.username, giverAPoint, paint);
            }
            if (TableProfile.giverB_ID != null)
            {
                var giverBUsername = await WebManager.SendData(new UserProfile((int) TableProfile.giverB_ID),
                    WebManager.RequestType.GetUserData, true);
                canvas.DrawText("@" + giverBUsername.playerData.username, giverBPoint, paint);
            }
            if (TableProfile.giverC_ID != null)
            {
                var giverCUsername = await WebManager.SendData(new UserProfile((int) TableProfile.giverC_ID),
                    WebManager.RequestType.GetUserData, true);
                canvas.DrawText("@" + giverCUsername.playerData.username, giverCPoint, paint);
            }
            if (TableProfile.giverD_ID != null)
            {
                var giverDUsername = await WebManager.SendData(new UserProfile((int) TableProfile.giverD_ID),
                    WebManager.RequestType.GetUserData, true);
                canvas.DrawText("@" + giverDUsername.playerData.username, giverDPoint, paint);
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