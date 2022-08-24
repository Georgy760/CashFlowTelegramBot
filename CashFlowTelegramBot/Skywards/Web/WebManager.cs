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
    public TableProfile? Table = null;
    public UserTableList? UserTableList = null;

    public RequsetForm(UserData user, WebManager.RequestType requestType)
    {
        Type = requestType.ToString();
        User = user.playerData;
        Table = user.tableData;
    }
    public RequsetForm(UserProfile user, WebManager.RequestType requestType)
    {
        Type = requestType.ToString();
        User = user;
        Table = null;
        UserTableList = null;
    }

    public RequsetForm(TableProfile table, WebManager.RequestType requestType)
    {
        Type = requestType.ToString();
        User = null;
        Table = table;
        UserTableList = null;
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
        //Register,
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

    //private static readonly string targetURL = "http://79.174.13.107/logreg.php";
    private static readonly string targetURL = "http://79.174.13.107/logregV2.php";

    public static async Task<UserData> SendData(UserProfile data, RequestType requestType)
    {
        var form = new RequsetForm(data, requestType);
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
            
            UserData = await SetResponseData(result);
        }

        Console.WriteLine(httpResponse.StatusCode);
        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
        }

        return UserData;
    }
    public static async Task<UserData> SendData(TableProfile data, RequestType requestType)
    {
        var form = new RequsetForm(data, requestType);
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
            
            UserData = await SetResponseData(result);
        }

        Console.WriteLine(httpResponse.StatusCode);
        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
        }

        return UserData;
    }
    public static async Task<UserData> SetResponseData(string response)
    {
        var userData = new UserData();
        var responseUser = new UserProfile();
        if (!response.Contains("userProfile\":null"))
        {
            
            var res = JObject.Parse(response);
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
            //Console.WriteLine("TEST: " + results[5] + "\nEND");
            if (!results[5].ToString().Contains("userTableList\":null"))
            {
                Console.WriteLine("DEBUG0");
                Console.WriteLine(results[5]);
                Console.WriteLine("DEBUG1");
                var resp = JObject.Parse("{" + results[5] + "}");
                Console.WriteLine("DEBUG2");
                IList<JToken> results_UserTableList = resp["userTableList"].Children().ToList();
                Console.WriteLine("DEBUG3");
                Console.WriteLine("--------------\nuserProfile->userTableList:\n--------------");
                foreach (var obj in results_UserTableList) Console.WriteLine("\n" + obj);
                //ID
                Console.WriteLine("ID");
                responseUser.UserTableList.id = (int) results_UserTableList[0];
                //USER_ID
                Console.WriteLine("USER_ID");
                responseUser.UserTableList.userID = (int) results_UserTableList[1];
                //COPPER_INFO
                Console.WriteLine("COPPER_INFO");
                if (!results_UserTableList[2].ToString().Contains("null"))
                {
                    responseUser.UserTableList.table_ID_copper = (int) results_UserTableList[2];
                    if (!results_UserTableList[3].ToString().Contains("null"))
                        responseUser.UserTableList.copperTableRole = Enum.Parse<Table.TableRole>(
                            results_UserTableList[3].ToString().Replace("\"copperTableRole\": \"", "")
                                .Replace("\"", ""), true);
                    else responseUser.UserTableList.copperTableRole = null;
                }
                else responseUser.UserTableList.table_ID_copper = null;
                //BRONZE_INFO
                Console.WriteLine("BRONZE_INFO");
                if (!results_UserTableList[4].ToString().Contains("null"))
                {
                    responseUser.UserTableList.table_ID_bronze = (int) results_UserTableList[4];
                    if (!results_UserTableList[5].ToString().Contains("null"))
                        responseUser.UserTableList.bronzeTableRole = Enum.Parse<Table.TableRole>(
                            results_UserTableList[5].ToString().Replace("\"bronzeTableRole\": \"", "")
                                .Replace("\"", ""), true);
                    else responseUser.UserTableList.bronzeTableRole = null;
                }
                else responseUser.UserTableList.table_ID_bronze = null;
                //SILVER_INFO
                Console.WriteLine("SILVER_INFO");
                if (!results_UserTableList[6].ToString().Contains("null"))
                {
                    responseUser.UserTableList.table_ID_silver = (int) results_UserTableList[6];
                    if (!results_UserTableList[7].ToString().Contains("null"))
                        responseUser.UserTableList.silverTableRole = Enum.Parse<Table.TableRole>(
                            results_UserTableList[7].ToString().Replace("\"silverTableRole\": \"", "")
                                .Replace("\"", ""), true);
                    else responseUser.UserTableList.silverTableRole = null;
                }
                else responseUser.UserTableList.table_ID_silver = null;
                //GOLD_INFO
                Console.WriteLine("GOLD_INFO");
                if (!results_UserTableList[8].ToString().Contains("null"))
                {
                    responseUser.UserTableList.table_ID_gold = (int) results_UserTableList[8];
                    if (!results_UserTableList[9].ToString().Contains("null"))
                        responseUser.UserTableList.goldTableRole = Enum.Parse<Table.TableRole>(
                            results_UserTableList[9].ToString().Replace("\"goldTableRole\": \"", "")
                                .Replace("\"", ""), true);
                    else responseUser.UserTableList.goldTableRole = null;
                }
                else responseUser.UserTableList.table_ID_gold = null;
                //PLATINUM_INFO
                Console.WriteLine("PLATINUM_INFO");
                if (!results_UserTableList[10].ToString().Contains("null"))
                {
                    responseUser.UserTableList.table_ID_platinum = (int) results_UserTableList[10];
                    if (!results_UserTableList[11].ToString().Contains("null"))
                        responseUser.UserTableList.platinumTableRole = Enum.Parse<Table.TableRole>(
                            results_UserTableList[11].ToString().Replace("\"platinumTableRole\": \"", "")
                                .Replace("\"", ""), true);
                    else responseUser.UserTableList.platinumTableRole = null;
                }
                else responseUser.UserTableList.table_ID_platinum = null;
                //DIAMOND_INFO
                Console.WriteLine("DIAMOND_INFO");
                if (!results_UserTableList[12].ToString().Contains("null"))
                {
                    responseUser.UserTableList.table_ID_diamond = (int) results_UserTableList[12];
                    if (!results_UserTableList[13].ToString().Contains("null"))
                        responseUser.UserTableList.diamondTableRole = Enum.Parse<Table.TableRole>(
                            results_UserTableList[13].ToString().Replace("\"diamondTableRole\": \"", "")
                                .Replace("\"", ""), true);
                    else responseUser.UserTableList.diamondTableRole = null;
                }
                else responseUser.UserTableList.table_ID_diamond = null;
            }
            else responseUser.UserTableList = null;

            responseUser.verf = results[6].ToString().Contains("1");
            var levelTableType =
                Enum.Parse<Table.TableType>(
                    results[7].ToString().Replace("\"level_tableType\": \"", "").Replace("\"", ""), true);
            responseUser.level_tableType = levelTableType;
            responseUser.invited = (int) results[8];
            responseUser.team = (int) results[9];
            responseUser.giftsReceived = (int) results[10];
            responseUser.PrintUserProfile();
        }
        var tableProfile = new TableProfile();
        if (!response.Contains("tableProfile\":null"))
        {
            
            var res = JObject.Parse(response);
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
        var error = new Error();
        if (!response.Contains("empty"))
        {
            
            var res = JObject.Parse(response);
            IList<JToken> errors = res["error"].Children().ToList();
            Console.WriteLine("--------------\nerror:\n--------------");
            foreach (var obj in errors) Console.WriteLine("\n" + obj);

            error.errorText = errors[0].ToString().Replace("\"errorText\": \"", "").Replace("\"", "");
            error.isError = true;
        }

        userData.playerData = responseUser;
        userData.error = error;
        userData.tableData = tableProfile;
        return userData;
    }
}