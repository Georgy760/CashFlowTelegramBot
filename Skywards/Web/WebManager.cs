using System.Net;
using CashFlowTelegramBot.Skywards.Telegram;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;

namespace CashFlowTelegramBot.Skywards.Web;

[Serializable]
public class UserData
{
    public Error error;
    public UserProfile playerData;
    public TableProfile tableData;

    public UserData()
    {
        playerData = new UserProfile();
        tableData = new TableProfile();
        error = new Error();
    }
}

[Serializable]
public class Error
{
    public string errorText;
    public bool isError;

    public Error()
    {
        errorText = "";
        isError = false;
    }
}

[Serializable]
public class RequsetForm
{
    public string Type;
    public UserProfile? User;

    //public TableProfile? Table = null;

    public RequsetForm(UserProfile user, WebManager.RequestType requestType)
    {
        Type = requestType.ToString();
        User = user;
    }

    /*public RequsetForm(TableProfile? table, WebManager.RequestType requestType)
    {
        this.Type = requestType.ToString();
        this.Table = table;
    }
    */
}

public class WebManager
{
    public enum RequestType
    {
        GetUserData,
        Register,
        RegisterWithRef,
        ChangeLang,
        GetTableData,
        RegisterIntoTable,
        LeaveTable,
        RemoveFromTable,
        Confirm
    }

    public static WebManager WM;

    public static UserData UserData = new();

    private static readonly string targetURL = "http://79.174.13.107/logreg.php";

    public static async Task<UserData> SendData(UserProfile usr, RequestType requestType)
    {
        var form = new RequsetForm(usr, requestType);
        var json = JsonConvert.SerializeObject(form, Formatting.Indented);
        Console.WriteLine("\nJSON: " + json);

        var httpWebRequest = (HttpWebRequest) WebRequest.Create(targetURL);
        httpWebRequest.Method = "POST";
        httpWebRequest.Accept = "application/json";
        httpWebRequest.ContentType = "application/json";


        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        UserData = new UserData();
        var responseUser = new UserProfile();
        var error = new Error();
        var tableProfile = new TableProfile();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            Console.WriteLine("\nResponse: " + result);
            if (!result.Contains("userProfile\":null"))
            {
                var res = JObject.Parse(result);
                //userProfileJSON_DESERIALIZE 
                IList<JToken> results = res["userProfile"].Children().ToList();
                Console.WriteLine("--------------\nuserProfile:\n--------------");
                foreach (var obj in results) Console.WriteLine("\n" + obj);

                responseUser.id = (int) results[0];
                responseUser.username = results[1].ToString().Replace("\"username\": \"", "").Replace("\"", "");
                if (!results[2].ToString().Contains("null"))
                    responseUser.refId = (int) results[2];
                else responseUser.refId = null;

                if (!results[3].ToString().Contains("null"))
                    responseUser.invitedBy = results[3].ToString().Replace("\"invitedBy\": ", "").Replace("\"", "");
                else responseUser.invitedBy = null;

                responseUser.lang = results[4].ToString().Replace("\"lang\": \"", "").Replace("\"", "");
                if (!results[5].ToString().Contains("null"))
                    responseUser.table_id = (int) results[5];
                else responseUser.table_id = null;


                var tableType =
                    Enum.Parse<Table.TableType>(
                        results[6].ToString().Replace("\"level_tableType\": \"", "").Replace("\"", ""), true);

                responseUser.level_tableType = tableType.ToString();
                var tableRole =
                    Enum.Parse<Table.TableRole>(
                        results[7].ToString().Replace("\"tableRole\": \"", "").Replace("\"", ""), true);
                responseUser.tableRole = tableRole.ToString();
                responseUser.invited = (int) results[8];
                responseUser.PrintUserProfile();
            }

            if (!result.Contains("tableProfile\":null"))
            {
                var res = JObject.Parse(result);
                IList<JToken> tableProfiles = res["tableProfile"].Children().ToList();
                Console.WriteLine("--------------\ntableProfile:\n--------------");
                foreach (var obj in tableProfiles) Console.WriteLine("\n" + obj);
                tableProfile.tableID = (int) tableProfiles[0];
                var tableType =
                    Enum.Parse<Table.TableType>(
                        tableProfiles[1].ToString().Replace("\"tableType\": \"", "").Replace("\"", ""), true);
                tableProfile.tableType = tableType;
                if (!tableProfiles[2].ToString().Contains("null"))
                    tableProfile.bankerID = (int) tableProfiles[2];
                else tableProfile.bankerID = null;

                if (!tableProfiles[3].ToString().Contains("null"))
                    tableProfile.managerA_ID = (int) tableProfiles[3];
                else tableProfile.managerA_ID = null;

                if (!tableProfiles[4].ToString().Contains("null"))
                    tableProfile.managerB_ID = (int) tableProfiles[4];
                else tableProfile.managerB_ID = null;

                if (!tableProfiles[5].ToString().Contains("null"))
                    tableProfile.giverA_ID = (int) tableProfiles[5];
                else tableProfile.giverA_ID = null;
                tableProfile.verf_A = tableProfiles[6].ToString().Contains("1");
                if (!tableProfiles[7].ToString().Contains("null"))
                    tableProfile.giverB_ID = (int) tableProfiles[7];
                else tableProfile.giverB_ID = null;
                tableProfile.verf_B = tableProfiles[8].ToString().Contains("1");
                if (!tableProfiles[9].ToString().Contains("null"))
                    tableProfile.giverC_ID = (int) tableProfiles[9];
                else tableProfile.giverC_ID = null;
                tableProfile.verf_C = tableProfiles[10].ToString().Contains("1");
                if (!tableProfiles[11].ToString().Contains("null"))
                    tableProfile.giverD_ID = (int) tableProfiles[11];
                else tableProfile.giverD_ID = null;
                tableProfile.verf_D = tableProfiles[12].ToString().Contains("1");
                tableProfile.PrintTableProfile();
            }

            if (!result.Contains("empty"))
            {
                var res = JObject.Parse(result);
                IList<JToken> errors = res["error"].Children().ToList();
                Console.WriteLine("--------------\nerror:\n--------------");
                foreach (var obj in errors) Console.WriteLine("\n" + obj);

                error.errorText = errors[0].ToString().Replace("\"errorText\": \"", "").Replace("\"", "");
                error.isError = true;
            }

            UserData.playerData = responseUser;
            UserData.error = error;
            UserData.tableData = tableProfile;
        }

        Console.WriteLine(httpResponse.StatusCode);
        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
        }

        return UserData;
    }

    public static async Task DownloadImageAsync(string directoryPath, string fileName, Uri uri)
    {
        using var httpClient = new HttpClient();

        // Get the file extension
        var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
        var fileExtension = Path.GetExtension(uriWithoutQuery);

        // Create file path and ensure directory exists
        var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
        Directory.CreateDirectory(directoryPath);

        // Download the image and write to the file
        var imageBytes = await httpClient.GetByteArrayAsync(uri);
        await File.WriteAllBytesAsync(path, imageBytes);
    }
}