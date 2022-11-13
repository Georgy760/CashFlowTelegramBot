<?php
//include("db.php");

$dt = $input = json_decode(file_get_contents('php://input'), true); //$_POST;

$user = "root";
$pass = "0x2f7d8186f142EB170349D4Fe1b842fEF1fC95275";
$dbname = "BotV3";

$db = new PDO('mysql:host=localhost;dbname=' . $dbname, $user, $pass);
/*
{
  "Type": "RemoveFromTable",
  "User": {
    "giftsReceived": 0,
    "invited": 0,
    "team": 0,
    "id": 237487193,
    "username": null,
    "refId": null,
    "invitedBy": null,
    "lang": null,
    "verf": false,
    "UserTableList": null,
    "level_tableType": 0
  },
  "Table": {
    "tableID": 295,
    "tableType": 0,
    "bankerID": 5680187538,
    "managerA_ID": null,
    "giverA_ID": 237487193,
    "verf_A": false,
    "giverB_ID": null,
    "verf_B": false,
    "managerB_ID": null,
    "giverC_ID": null,
    "verf_C": false,
    "giverD_ID": null,
    "verf_D": false
  },
  "UserTableList": null
}
 */
$userTableList = array(
    "id" => 0,
    "userID" => 0,
    "table_ID_copper" => null,
    "copperTableRole" => null,
    "table_ID_bronze" => null,
    "bronzeTableRole" => null,
    "table_ID_silver" => null,
    "silverTableRole" => null,
    "table_ID_gold" => null,
    "goldTableRole" => null,
    "table_ID_platinum" => null,
    "platinumTableRole" => null,
    "table_ID_diamond" => null,
    "diamondTableRole" => null,
);
$userProfile = array(
    "id" => -1,
    "username" => "username",
    "refId" => 0,
    "invitedBy" => "username",
    "lang" => "eng",
    "userTableList" => $userTableList,
    "verf" => 0,
    "level_tableType" => "copper",
    "invited" => 0,
    "team" => 0,
    "giftsReceived" => 0
);
$tableProfile = array(
    "tableID" => 0,
    "tableType" => "copper",
    "bankerID" => null,
    //"managerA_ID" => null,
    "giverA_ID" => null,
    "verf_A" => null,
    "giverB_ID" => null,
    "verf_B" => null,
    //"managerB_ID" => null,
    "giverC_ID" => null,
    "verf_C" => null,
    "giverD_ID" => null,
    "verf_D" => null
);
$notification = array(
    "notificationText" => "NotifyEmpty",
    "tableID" => null,
    "bankerID" => null,
    //"managerA_ID" => null,
    //"managerB_ID" => null,
    "giverA_ID" => null,
    "giverB_ID" => null,
    "giverC_ID" => null,
    "giverD_ID" => null,
    "isNotify" => false,
    "tableType" => "copper"
);
$error = array(
    "errorText" => "empty",
    "isError" => false
);

$userData = array(
    "userProfile" => $userProfile,
    "tableProfile" => null,//$tableProfile,
    "notification" => null,//$notification,
    "error" => $error,
    "updateData" => null,

);
$updateData = array(

);

switch ($dt['Type']) {
    case "GetAllUsersID":
    {
        $sql = "SELECT `userID` FROM `users`";
        $q = $db->prepare($sql, array(PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL));
        $q->execute();
        if ($q->rowCount() > 0) {
            while ($row = $q->fetch(PDO::FETCH_NUM, PDO::FETCH_ORI_NEXT)) {
                $updateData[] = $row[0];
            }
            $userData["updateData"] = $updateData;
        }
        break;
    }
    case "GetUserData":
    {
        if (isset($dt['User']['id'])) {
            //$userData["userProfile"] = GetUserData($dt['User']['id']);
            //$userID = $dt['User']['id'];
            $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$dt['User']['id']}'");
            if ($users->rowCount() == 1) {
                $user = $users->fetch(PDO::FETCH_ASSOC);
                $invitedCount = $db->query("SELECT * FROM `users` WHERE `refId` = '{$dt['User']['id']}' AND `verf` = '1'");
                $invitedBy = $db->query("SELECT * FROM `users` WHERE `userID` = '{$user['refId']}'")->fetch(PDO::FETCH_ASSOC);
                $usersTableList = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$dt['User']['id']}'")->fetch(PDO::FETCH_ASSOC);

                $team = $db->query("WITH RECURSIVE descendants AS (
                SELECT userID
                FROM users
                WHERE userID= '{$dt['User']['id']}'
                UNION ALL
                SELECT t.userID
                FROM descendants d, users t
                WHERE t.refId=d.userID
                )
                SELECT * FROM descendants;");

                $userData["userProfile"]["id"] = $user['userID'];
                $userData["userProfile"]["username"] = $user['username'];
                $userData["userProfile"]["refId"] = $user['refId'];
                $userData["userProfile"]["invitedBy"] = $invitedBy['username'];
                $userData["userProfile"]["lang"] = $user['lang'];
                $userData["userProfile"]["userTableList"] = SetUserTableList($usersTableList); //TODO ОШИБКА ЗДЕСЬ
                $userData["userProfile"]["level_tableType"] = $user['level_tableType'];
                $userData["userProfile"]["invited"] = $invitedCount->rowCount();
                $userData["userProfile"]["team"] = $team->rowCount() - 1;
                $userData["userProfile"]["giftsReceived"] = $user['giftsReceived'];
            } else SetError("User is not recognized - GetUserData - ERROR");
        } else SetError("Incorrect id - GetUserData - ERROR");
        break;
    }
    case "Register":
    {
        if (isset($dt['User']['id']) && isset($dt['User']['username'])) {
            $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$dt['User']['id']}'");
            if ($users->rowCount() == 0) {
                //Создание пользователя
                $db->query("INSERT INTO `users`(`userID`,`username`) VALUES ('{$dt['User']['id']}', '{$dt['User']['username']}')");
            } else {
                $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$dt['User']['id']}' AND `username` != '{$dt['User']['username']}'");
                if ($users->rowCount() == 1) {
                    $db->query("UPDATE `users` SET `username` = '{$dt['User']['username']}' WHERE `userID` = '{$dt['User']['id']}'");
                } else SetError("User Exists - Register - ERROR");
            }
        } else SetError("Data incorrect - Register - ERROR");
        break;
    }
    case "ChangeLang":
    {
        if (isset($dt['User']['id']) && isset($dt['User']['lang']) && $dt['User']['lang'] == ('ru' || 'eng' || 'fr' || 'de')) {
            $db->query("UPDATE `users` SET `lang` = '{$dt['User']['lang']}' WHERE `userID` = '{$dt['User']['id']}'");
        } else SetError("Data incorrect - ChangeLang - ERROR");
        break;
    }
    case "RegisterWithRef":
    {
        if (isset($dt['User']['id']) && isset($dt['User']['username']) && isset($dt['User']['refId'])) {
            $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$dt['User']['id']}'");
            if ($users->rowCount() == 0) {
                $refUser = $db->query("SELECT * FROM `users` WHERE `userID` = '{$dt['User']['refId']}'");
                if ($refUser->rowCount() == 1) {
                    //Создание пользователя с ref ссылкой
                    $db->query("INSERT INTO `users`(`userID` , `username`, `refId`)
                        VALUES ('{$dt['User']['id']}', '{$dt['User']['username']}', '{$dt['User']['refId']}')");
                } else SetError("RefLink invalid");
            } else SetError("User Exists - RegisterWithRef - ERROR");
        } else SetError("Data incorrect - RegisterWithRef - ERROR");

        break;
    }
    case "RegisterIntoTable":
    {
        if (isset($dt['User']['id']) && isset($dt['User']['level_tableType'])) {
            //print "test\n";
            $tableType = $dt['User']['level_tableType'];
            switch ($tableType) {
                case 0:
                    $tableType = "copper";
                    break;
                case 1:
                    $tableType = "bronze";
                    break;
                case 2:
                    $tableType = "silver";
                    break;
                case 3:
                    $tableType = "gold";
                    break;
                case 4:
                    $tableType = "platinum";
                    break;
                case 5:
                    $tableType = "diamond";
                    break;
            }
            $user = $db->query("SELECT * FROM `users` WHERE `userID` = '{$dt['User']['id']}'")->fetch(PDO::FETCH_ASSOC);
            $userTables = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$dt['User']['id']}'")->fetch(PDO::FETCH_ASSOC);
            $ThisUserHaveTable = false;

            switch ($tableType) {
                case "copper":
                {
                    if ($userTables['table_ID_copper'] != null) {
                        //У пользователя уже есть стол
                        $ThisUserHaveTable = true;
                        $table = $userTables;
                        $userData["tableProfile"] = SetTableProfileData($userTables['table_ID_copper']);
                        //return false;
                    } else {
                        if ($userTables['CompletedCopper'] == 0) {
                            $RefID = $user['refId'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveCopperTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTable($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        } else {
                            $RefID = $user['userID'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveCopperTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTableReinvest($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        }
                    }
                    break;
                }
                case "bronze":
                {
                    if ($userTables['table_ID_bronze'] != null) {
                        $ThisUserHaveTable = true;
                        $table = $userTables;
                        $userData["tableProfile"] = SetTableProfileData($userTables['table_ID_bronze']);
                        //return false;
                    } else {
                        if ($userTables['CompletedBronze'] == 0) {
                            $RefID = $user['refId'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveBronzeTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTable($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        } else {
                            $RefID = $user['userID'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveBronzeTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTableReinvest($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        }
                    }
                    break;
                }
                case "silver":
                {
                    if ($userTables['table_ID_silver'] != null) {
                        $ThisUserHaveTable = true;
                        $table = $userTables;
                        $userData["tableProfile"] = SetTableProfileData($userTables['table_ID_silver']);
                        //return false;
                    } else {
                        if ($userTables['CompletedSilver'] == 0) {
                            $RefID = $user['refId'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveSilverTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTable($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        } else {
                            $RefID = $user['userID'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveSilverTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTableReinvest($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        }
                    }
                    break;
                }
                case "gold":
                {
                    if ($userTables['table_ID_gold'] != null) {
                        $ThisUserHaveTable = true;
                        $table = $userTables;
                        $userData["tableProfile"] = SetTableProfileData($userTables['table_ID_gold']);
                        //return false;
                    } else {
                        if ($userTables['CompletedCopper'] == 0) {
                            $RefID = $user['refId'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveGoldTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTable($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        } else {
                            $RefID = $user['userID'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveGoldTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTableReinvest($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        }

                    }
                    break;
                }
                case "platinum":
                {
                    if ($userTables['table_ID_platinum'] != null) {
                        $ThisUserHaveTable = true;
                        $table = $userTables;
                        $userData["tableProfile"] = SetTableProfileData($userTables['table_ID_platinum']);
                        //return false;
                    } else {
                        if ($userTables['CompletedCopper'] == 0) {
                            $RefID = $user['refId'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leavePlatinumTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTable($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        } else {
                            $RefID = $user['userID'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leavePlatinumTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTableReinvest($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        }
                    }
                    break;
                }
                case "diamond":
                {
                    if ($userTables['table_ID_diamond'] != null) {
                        $ThisUserHaveTable = true;
                        $table = $userTables;
                        $userData["tableProfile"] = SetTableProfileData($userTables['table_ID_diamond']);
                        //return false;
                    } else {
                        if ($userTables['CompletedCopper'] == 0) {
                            $RefID = $user['refId'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveDiamondTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTable($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        } else {
                            $RefID = $user['userID'];
                            $IsBlocked = $db->query("SELECT * FROM userTableList WHERE `userID` = '{$dt['User']['id']}' AND `leaveDiamondTable` >= now() - INTERVAL 1 DAY");
                            if ($IsBlocked->rowCount() == 0) EnterTableReinvest($tableType, $dt['User']['id'], $RefID);
                            else SetError("RegisterIntoTable - ThisTableIsBlocked - ERROR");
                        }
                    }
                    break;
                }
            }
        } else SetError("Data incorrect - RegisterIntoTable - ERROR");
        break;
    }
    case "GetTableData":
    {
        if (isset($dt['Table']['tableID'])) {
            $tables = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
            if ($tables->rowCount() == 1) {
                $table = $tables->fetch(PDO::FETCH_ASSOC);
                $tableType = $table['tableType'];
                $giverA_Ban = false;
                $giverB_Ban = false;
                $giverC_Ban = false;
                $giverD_Ban = false;
                $EnterDateGiver = $db->query("SELECT * FROM `tables` WHERE `giverA_ID` IS NOT NULL AND `tableID` = '{$table['tableID']}' AND `verf_A` = 0 AND `EnterDateGiverA` <= NOW() - INTERVAL 1 DAY");
                if($EnterDateGiver->rowCount() == 1){
                    $giverA_Ban = true;
                    $EnterDateGiver = $EnterDateGiver->fetch(PDO::FETCH_ASSOC);
                    $giverData = $EnterDateGiver['giverA_ID'];
                    $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = 0 WHERE `tableID` = '{$table['tableID']}'");
                    switch ($tableType){
                        case "copper":{
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "bronze":{
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `leaveBronzeTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "silver":{
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "gold":{
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "platinum":{
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "diamond":{
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                    }
                }
                $EnterDateGiver = $db->query("SELECT * FROM `tables` WHERE `giverB_ID` IS NOT NULL AND `tableID` = '{$table['tableID']}' AND `verf_B` = 0 AND `EnterDateGiverB` <= NOW() - INTERVAL 1 DAY");
                if($EnterDateGiver->rowCount() == 1){
                    $giverB_Ban = true;
                    $EnterDateGiver = $EnterDateGiver->fetch(PDO::FETCH_ASSOC);
                    $giverData = $EnterDateGiver['giverB_ID'];
                    $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `EnterDateGiverB` = NULL, `verf_B` = 0 WHERE `tableID` = '{$table['tableID']}'");
                    switch ($tableType){
                        case "copper":{
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "bronze":{
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `leaveBronzeTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "silver":{
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "gold":{
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "platinum":{
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "diamond":{
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                    }
                }
                $EnterDateGiver = $db->query("SELECT * FROM `tables` WHERE `giverC_ID` IS NOT NULL AND `tableID` = '{$table['tableID']}' AND `verf_C` = 0 AND `EnterDateGiverC` <= NOW() - INTERVAL 1 DAY");
                if($EnterDateGiver->rowCount() == 1){
                    $giverC_Ban = true;
                    $EnterDateGiver = $EnterDateGiver->fetch(PDO::FETCH_ASSOC);
                    $giverData = $EnterDateGiver['giverC_ID'];
                    $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `EnterDateGiverC` = NULL, `verf_C` = 0 WHERE `tableID` = '{$table['tableID']}'");
                    switch ($tableType){
                        case "copper":{
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "bronze":{
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `leaveBronzeTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "silver":{
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "gold":{
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "platinum":{
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "diamond":{
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                    }
                }
                $EnterDateGiver = $db->query("SELECT * FROM `tables` WHERE `giverD_ID` IS NOT NULL AND `tableID` = '{$table['tableID']}' AND `verf_D` = 0 AND `EnterDateGiverD` <= NOW() - INTERVAL 1 DAY");
                if($EnterDateGiver->rowCount() == 1){
                    $giverD_Ban = true;
                    $EnterDateGiver = $EnterDateGiver->fetch(PDO::FETCH_ASSOC);
                    $giverData = $EnterDateGiver['giverD_ID'];
                    $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `EnterDateGiverD` = NULL, `verf_D` = 0 WHERE `tableID` = '{$table['tableID']}'");
                    switch ($tableType){
                        case "copper":{
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "bronze":{
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `leaveBronzeTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "silver":{
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "gold":{
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "platinum":{
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                        case "diamond":{
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$giverData}'");
                            break;
                        }
                    }
                }
                if($giverA_Ban || $giverB_Ban || $giverC_Ban || $giverD_Ban){
                    $userData["notification"]["notificationText"] = "BannedAfterDayUnVerified";
                    $userData["notification"]["tableID"] = $table['tableID'];
                    $userData["notification"]["bankerID"] = null;
                    //$userData["notification"]["managerA_ID"] = null;
                    //$userData["notification"]["managerB_ID"] = null;
                    if($giverA_Ban) $userData["notification"]["giverA_ID"] = $table['giverA_ID'];
                    else $userData["notification"]["giverA_ID"] = null;
                    if($giverB_Ban) $userData["notification"]["giverB_ID"] = $table['giverB_ID'];
                    else $userData["notification"]["giverB_ID"] = null;
                    if($giverC_Ban) $userData["notification"]["giverC_ID"] = $table['giverC_ID'];
                    else $userData["notification"]["giverC_ID"] = null;
                    if($giverD_Ban) $userData["notification"]["giverD_ID"] = $table['giverD_ID'];
                    else $userData["notification"]["giverD_ID"] = null;
                    $userData["notification"]["isNotify"] = true;
                    $userData["notification"]["tableType"] = $table['tableType'];
                    $tables = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                    $table = $tables->fetch(PDO::FETCH_ASSOC);
                }
                $userData["tableProfile"] = SetTableProfileData($table['tableID']);
            } else SetError("GetTableData - TableIsNotFound - ERROR");
        }
        break;
    }
    case "GetUserTableListData":
    {
        if (isset($dt['User']['id'])) {
            $usersTableList = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$dt['User']['id']}'");
            if ($usersTableList->rowCount() == 1) {
                $userTableList = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$dt['User']['id']}' AND (`table_ID_copper` IS NOT NULL OR `table_ID_bronze` IS NOT NULL OR 
                                                                                            `table_ID_silver` IS NOT NULL OR `table_ID_gold` IS NOT NULL OR 
                                                                                            `table_ID_platinum` IS NOT NULL OR `table_ID_diamond` IS NOT NULL)");
                if ($userTableList->rowCount() == 1) {
                    $userTables = $userTableList->fetch(PDO::FETCH_ASSOC);
                    SetUserTableList($userTables);
                } else SetError("GetTableData - UserDontHaveAnyTables - ERROR");
            } else SetError("GetTableData - UserIsNotFound - ERROR");
        } else SetError("GetTableData - DataIsWrong - ERROR");
        break;
    }
    case "LeaveTable":
    {
        if (isset($dt['User']['id']) && isset($dt['User']['level_tableType'])) {
            $tableType = $dt['User']['level_tableType'];
            switch ($tableType) {
                case 0:
                    $tableType = "copper";
                    break;
                case 1:
                    $tableType = "bronze";
                    break;
                case 2:
                    $tableType = "silver";
                    break;
                case 3:
                    $tableType = "gold";
                    break;
                case 4:
                    $tableType = "platinum";
                    break;
                case 5:
                    $tableType = "diamond";
                    break;
            }
            $tables = $db->query("SELECT * FROM `tables` WHERE `tableType` = '{$tableType}' AND (`giverA_ID` = '{$dt['User']['id']}' OR `giverB_ID` = '{$dt['User']['id']}'
                                                            OR `giverC_ID` = '{$dt['User']['id']}' OR `giverD_ID` = '{$dt['User']['id']}')");

            if ($tables->rowCount() == 1) {
                $table = $tables->fetch(PDO::FETCH_ASSOC);
                switch ($tableType) {
                    case "copper":
                    {
                        if ($table['giverA_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverB_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `EnterDateGiverB` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverC_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `EnterDateGiverC` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverD_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `EnterDateGiverD` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        break;
                    }
                    case "bronze":
                    {
                        if ($table['giverA_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `leaveBronzeTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverB_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `EnterDateGiverB` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `leaveBronzeTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverC_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `EnterDateGiverC` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `leaveBronzeTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverD_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `EnterDateGiverD` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `leaveBronzeTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        break;
                    }
                    case "silver":
                    {
                        if ($table['giverA_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverB_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `EnterDateGiverB` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverC_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `EnterDateGiverC` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverD_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `EnterDateGiverD` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        break;
                    }
                    case "gold":
                    {
                        if ($table['giverA_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverB_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `EnterDateGiverB` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverC_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `EnterDateGiverC` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverD_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `EnterDateGiverD` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        break;
                    }
                    case "platinum":
                    {
                        if ($table['giverA_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverB_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `EnterDateGiverB` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverC_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `EnterDateGiverC` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverD_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `EnterDateGiverD` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        break;
                    }
                    case "diamond":
                    {
                        if ($table['giverA_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverB_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `EnterDateGiverB` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverC_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `EnterDateGiverC` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverD_ID'] == $dt['User']['id']) {
                            $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `EnterDateGiverD` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        break;
                    }
                }
            } else SetError("LeaveTable - TableIsNotFound - ERROR");

        } else SetError("LeaveTable - ERROR");

        break;
    }
    case "RemoveFromTable":
    {
        if (isset($dt['User']['id']) && isset($dt['Table']['tableID'])) { //userID - is id of user that should be removed
            $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$dt['User']['id']}'");
            if ($users->rowCount() == 1) {
                $user = $users->fetch(PDO::FETCH_ASSOC);
                $tables = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                if ($tables->rowCount() == 1) {
                    $table = $tables->fetch(PDO::FETCH_ASSOC);
                    $tableType = $table['tableType']; //`table_ID_copper` = NULL
                    switch ($tableType) {
                        case "copper":
                        {
                            if ($table['giverA_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, $table['giverA_ID'], null, null, null);
                            }
                            if ($table['giverB_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, $table['giverB_ID'], null, null);
                            }
                            if ($table['giverC_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, $table['giverC_ID'], null);
                            }
                            if ($table['giverD_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `leaveCopperTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, null, $table['giverD_ID']);
                            }
                            break;
                        }
                        case "bronze":
                        {
                            if ($table['giverA_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze`  = NULL, `leaveBronzeTable` = NOW() WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, $table['giverA_ID'], null, null, null);
                            }
                            if ($table['giverB_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze`  = NULL, `leaveBronzeTable` = NOW() WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, $table['giverB_ID'], null, null);
                            }
                            if ($table['giverC_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze`  = NULL, `leaveBronzeTable` = NOW() WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, $table['giverC_ID'], null);
                            }
                            if ($table['giverD_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze`  = NULL, `leaveBronzeTable` = NOW() WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, null, $table['giverD_ID']);
                            }
                            break;
                        }
                        case "silver":
                        {
                            if ($table['giverA_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW() WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, $table['giverA_ID'], null, null, null);
                            }
                            if ($table['giverB_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW() WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, $table['giverB_ID'], null, null);
                            }
                            if ($table['giverC_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW() WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, $table['giverC_ID'], null);
                            }
                            if ($table['giverD_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `leaveSilverTable` = NOW() WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, null, $table['giverD_ID']);
                            }
                            break;
                        }
                        case "gold":
                        {
                            if ($table['giverA_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, $table['giverA_ID'], null, null, null);
                            }
                            if ($table['giverB_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, $table['giverB_ID'], null, null);
                            }
                            if ($table['giverC_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, $table['giverC_ID'], null);
                            }
                            if ($table['giverD_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `leaveGoldTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, null, $table['giverD_ID']);
                            }
                            break;
                        }
                        case "platinum":
                        {
                            if ($table['giverA_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, $table['giverA_ID'], null, null, null);
                            }
                            if ($table['giverB_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, $table['giverB_ID'], null, null);
                            }
                            if ($table['giverC_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, $table['giverC_ID'], null);
                            }
                            if ($table['giverD_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `leavePlatinumTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, null, $table['giverD_ID']);
                            }
                            break;
                        }
                        case "diamond":
                        {
                            if ($table['giverA_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverA_ID` = NULL, `EnterDateGiverA` = NULL, `verf_A` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, $table['giverA_ID'], null, null, null);
                            }
                            if ($table['giverB_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverB_ID` = NULL, `verf_B` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, $table['giverB_ID'], null, null);
                            }
                            if ($table['giverC_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverC_ID` = NULL, `verf_C` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, $table['giverC_ID'], null);
                            }
                            if ($table['giverD_ID'] == $dt['User']['id']) {
                                $db->query("UPDATE `tables` SET `giverD_ID` = NULL, `verf_D` = '0' WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `leaveDiamondTable` = NOW()  WHERE `userID` = '{$dt['User']['id']}'");
                                SetNotification("GiverIsDeleted", $table['tableID'], $table['bankerID'], null, null, null, null, null, $table['giverD_ID']);
                            }
                            break;
                        }
                    }
                }
            }
        } else SetError("RemoveFromTable - ERROR");

        break;
    }
    case "Confirm":
    {
        {
            if (isset($dt['User']['id']) && isset($dt['Table']['tableID'])) {
                $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$dt['User']['id']}'");
                if ($users->rowCount() == 1) {
                    $user = $users->fetch(PDO::FETCH_ASSOC);
                    $tables = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                    if ($tables->rowCount() == 1) {
                        $table = $tables->fetch(PDO::FETCH_ASSOC);

                        $TypeOfTable = "";
                        switch ($table['tableType']) {
                            case 0:
                                $TypeOfTable = "copper";
                                break;
                            case 1:
                                $TypeOfTable = "bronze";
                                break;
                            case 2:
                                $TypeOfTable = "silver";
                                break;
                            case 3:
                                $TypeOfTable = "gold";
                                break;
                            case 4:
                                $TypeOfTable = "platinum";
                                break;
                            case 5:
                                $TypeOfTable = "diamond";
                                break;
                        }
                        switch ($table['tableType']) {
                            case "copper":
                                $TypeOfTable = "copper";
                                break;
                            case "bronze":
                                $TypeOfTable = "bronze";
                                break;
                            case "silver":
                                $TypeOfTable = "silver";
                                break;
                            case "gold":
                                $TypeOfTable = "gold";
                                break;
                            case "platinum":
                                $TypeOfTable = "platinum";
                                break;
                            case "diamond":
                                $TypeOfTable = "diamond";
                                break;
                        }
                        switch ($TypeOfTable) {
                            case "copper":
                            {
                                $db->query("UPDATE `users` SET `giftsReceived` = `giftsReceived` + 100 WHERE `userID` = '{$table['bankerID']}'");
                                break;
                            }
                            case "bronze":
                            {
                                $db->query("UPDATE `users` SET `giftsReceived` = `giftsReceived` + 400 WHERE `userID` = '{$table['bankerID']}'");
                                break;
                            }
                            case "silver":
                            {
                                $db->query("UPDATE `users` SET `giftsReceived` = `giftsReceived` + 1000 WHERE `userID` = '{$table['bankerID']}'");
                                break;
                            }
                            case "gold":
                            {
                                $db->query("UPDATE `users` SET `giftsReceived` = `giftsReceived` + 2500 WHERE `userID` = '{$table['bankerID']}'");
                                break;
                            }
                            case "platinum":
                            {
                                $db->query("UPDATE `users` SET `giftsReceived` = `giftsReceived` + 5000 WHERE `userID` = '{$table['bankerID']}'");
                                break;
                            }
                            case "diamond":
                            {
                                $db->query("UPDATE `users` SET `giftsReceived` = `giftsReceived` + 10000 WHERE `userID` = '{$table['bankerID']}'");
                                break;
                            }
                        }
                        if ($table['giverA_ID'] == $dt['User']['id'] && $table['verf_A'] == 0) {
                            $db->query("UPDATE `tables` SET `verf_A` = '1' WHERE `tableID` = '{$dt['Table']['tableID']}'");
                            SetNotification("GiverIsVerified", $table['tableID'], $table['bankerID'], null, null, $table['giverA_ID'], null, null, null);
                            if ($TypeOfTable == "bronze" || $TypeOfTable == "silver" ||
                                $TypeOfTable == "gold" || $TypeOfTable == "platinum" ||
                                $TypeOfTable == "diamond") {
                                $db->query("UPDATE `users` SET `verf` = '1' WHERE `userID` = '{$dt['User']['id']}'");
                            }
                            //$db->query("UPDATE `users` SET `table_id` = NULL WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverB_ID'] == $dt['User']['id'] && $table['verf_B'] == 0) {
                            $db->query("UPDATE `tables` SET `verf_B` = '1' WHERE `tableID` = '{$dt['Table']['tableID']}'");
                            SetNotification("GiverIsVerified", $table['tableID'], $table['bankerID'], null, null, null, $table['giverB_ID'], null, null);
                            if ($TypeOfTable == "bronze" || $TypeOfTable == "silver" ||
                                $TypeOfTable == "gold" || $TypeOfTable == "platinum" ||
                                $TypeOfTable == "diamond") {
                                $db->query("UPDATE `users` SET `verf` = '1' WHERE `userID` = '{$dt['User']['id']}'");
                            }
                            //$db->query("UPDATE `users` SET `table_id` = NULL WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverC_ID'] == $dt['User']['id'] && $table['verf_C'] == 0) {
                            $db->query("UPDATE `tables` SET `verf_C` = '1' WHERE `tableID` = '{$dt['Table']['tableID']}'");
                            SetNotification("GiverIsVerified", $table['tableID'], $table['bankerID'], null, null, null, null, $table['giverC_ID'], null);
                            if ($TypeOfTable == "bronze" || $TypeOfTable == "silver" ||
                                $TypeOfTable == "gold" || $TypeOfTable == "platinum" ||
                                $TypeOfTable == "diamond") {
                                $db->query("UPDATE `users` SET `verf` = '1' WHERE `userID` = '{$dt['User']['id']}'");
                            }
                            //$db->query("UPDATE `users` SET `table_id` = NULL WHERE `userID` = '{$dt['User']['id']}'");
                        }
                        if ($table['giverD_ID'] == $dt['User']['id'] && $table['verf_D'] == 0) {
                            $db->query("UPDATE `tables` SET `verf_D` = '1' WHERE `tableID` = '{$dt['Table']['tableID']}'");
                            SetNotification("GiverIsVerified", $table['tableID'], $table['bankerID'], null, null, null, null, null, $table['giverD_ID']);
                            if ($TypeOfTable == "bronze" || $TypeOfTable == "silver" ||
                                $TypeOfTable == "gold" || $TypeOfTable == "platinum" ||
                                $TypeOfTable == "diamond") {
                                $db->query("UPDATE `users` SET `verf` = '1' WHERE `userID` = '{$dt['User']['id']}'");
                            }
                            //$db->query("UPDATE `users` SET `table_id` = NULL WHERE `userID` = '{$dt['User']['id']}'");
                        }

                        $tables = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                        $table = $tables->fetch(PDO::FETCH_ASSOC);

                        /*
                        echo "\n".$userBanker['userID'];
                        echo "\n".$userManagerA['userID'];
                        echo "\n".$userManagerB['userID'];
                        echo "\n".$userGiverA['userID'];
                        echo "\n".$userGiverB['userID'];
                        echo "\n".$userGiverC['userID'];
                        echo "\n".$userGiverD['userID'];
                        */
                        if ($table['verf_A'] == 1 && $table['verf_B'] == 1 && $table['verf_C'] == 1 && $table['verf_D'] == 1) {
                            $userBanker['userID'] = $table['bankerID'];
                            //$userManagerA['userID'] = $table['managerA_ID'];
                            //$userManagerB['userID'] = $table['managerB_ID'];
                            $userGiverA['userID'] = $table['giverA_ID'];
                            $userGiverB['userID'] = $table['giverB_ID'];
                            $userGiverC['userID'] = $table['giverC_ID'];
                            $userGiverD['userID'] = $table['giverD_ID'];
                            SetNotificationWithTableType("TableCompleted", $table['tableType'], $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverC_ID']);

                            switch ($table['tableType']) {
                                case "copper":
                                    $db->query("DELETE FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                                    $bankerData = $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userBanker['userID']}'");
                                    $banker = $bankerData->fetch(PDO::FETCH_ASSOC);
                                    if ($banker['level_tableType'] == 'copper') {

                                        $db->query("UPDATE `users` SET `level_tableType` = 'bronze' WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                        $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `CompletedCopper` = `CompletedCopper` + 1 WHERE `userID` = '{$userBanker['userID']}'");
                                    } else {
                                        $db->query("UPDATE `userTableList` SET `table_ID_copper` = NULL, `CompletedCopper` = `CompletedCopper` + 1 WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                    }
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('copper', '{$userManagerA['userID']}', '{$userGiverA['userID']}', '{$userGiverB['userID']}')");
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('copper', '{$userManagerB['userID']}', '{$userGiverC['userID']}', '{$userGiverD['userID']}')");
                                    $tableA = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerA['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableA['tableID']}' WHERE `userID` = '{$userManagerA['userID']}' OR `userID` = '{$userGiverA['userID']}' OR `userID` = '{$userGiverB['userID']}'");
                                    $tableB = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerB['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableB['tableID']}' WHERE `userID` = '{$userManagerB['userID']}' OR `userID` = '{$userGiverC['userID']}' OR `userID` = '{$userGiverD['userID']}'");
                                    break;
                                case "bronze":
                                    $db->query("DELETE FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                                    $bankerData = $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userBanker['userID']}'");
                                    $banker = $bankerData->fetch(PDO::FETCH_ASSOC);
                                    if ($banker['level_tableType'] == 'copper' || $banker['level_tableType'] == 'bronze') {

                                        $db->query("UPDATE `users` SET `level_tableType` = 'silver' WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                        $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `CompletedBronze` = `CompletedBronze` + 1 WHERE `userID` = '{$userBanker['userID']}'");
                                    } else {
                                        $db->query("UPDATE `userTableList` SET `table_ID_bronze` = NULL, `CompletedBronze` = `CompletedBronze` + 1 WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                    }
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('bronze', '{$userManagerA['userID']}', '{$userGiverA['userID']}', '{$userGiverB['userID']}')");
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('bronze', '{$userManagerB['userID']}', '{$userGiverC['userID']}', '{$userGiverD['userID']}')");
                                    $tableA = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerA['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableA['tableID']}' WHERE `userID` = '{$userManagerA['userID']}' OR `userID` = '{$userGiverA['userID']}' OR `userID` = '{$userGiverB['userID']}'");
                                    $tableB = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerB['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableB['tableID']}' WHERE `userID` = '{$userManagerB['userID']}' OR `userID` = '{$userGiverC['userID']}' OR `userID` = '{$userGiverD['userID']}'");
                                    break;
                                case "silver":
                                    $db->query("DELETE FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                                    $bankerData = $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userBanker['userID']}'");
                                    $banker = $bankerData->fetch(PDO::FETCH_ASSOC);
                                    if ($banker['level_tableType'] == 'copper' || $banker['level_tableType'] == 'bronze' || $banker['level_tableType'] == 'silver') {

                                        $db->query("UPDATE `users` SET `level_tableType` = 'gold' WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `CompletedSilver` = `CompletedSilver` + 1 WHERE `userID` = '{$userBanker['userID']}'");
                                    } else {
                                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = NULL, `CompletedSilver` = `CompletedSilver` + 1 WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                    }
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('silver', '{$userManagerA['userID']}', '{$userGiverA['userID']}', '{$userGiverB['userID']}')");
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('silver', '{$userManagerB['userID']}', '{$userGiverC['userID']}', '{$userGiverD['userID']}')");
                                    $tableA = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerA['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableA['tableID']}' WHERE `userID` = '{$userManagerA['userID']}' OR `userID` = '{$userGiverA['userID']}' OR `userID` = '{$userGiverB['userID']}'");
                                    $tableB = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerB['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableB['tableID']}' WHERE `userID` = '{$userManagerB['userID']}' OR `userID` = '{$userGiverC['userID']}' OR `userID` = '{$userGiverD['userID']}'");
                                    break;
                                case "gold":
                                    $db->query("DELETE FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                                    $bankerData = $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userBanker['userID']}'");
                                    $banker = $bankerData->fetch(PDO::FETCH_ASSOC);
                                    if ($banker['level_tableType'] == 'copper' || $banker['level_tableType'] == 'bronze' || $banker['level_tableType'] == 'silver' || $banker['level_tableType'] == 'gold') {

                                        $db->query("UPDATE `users` SET `level_tableType` = 'platinum' WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                        $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `CompletedGold` = `CompletedGold` + 1 WHERE `userID` = '{$userBanker['userID']}'");
                                    } else {
                                        $db->query("UPDATE `userTableList` SET `table_ID_gold` = NULL, `CompletedGold` = `CompletedGold` + 1 WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                    }
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('gold', '{$userManagerA['userID']}', '{$userGiverA['userID']}', '{$userGiverB['userID']}')");
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('gold', '{$userManagerB['userID']}', '{$userGiverC['userID']}', '{$userGiverD['userID']}')");
                                    $tableA = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerA['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableA['tableID']}' WHERE `userID` = '{$userManagerA['userID']}' OR `userID` = '{$userGiverA['userID']}' OR `userID` = '{$userGiverB['userID']}'");
                                    $tableB = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerB['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableB['tableID']}' WHERE `userID` = '{$userManagerB['userID']}' OR `userID` = '{$userGiverC['userID']}' OR `userID` = '{$userGiverD['userID']}'");
                                    break;
                                case "platinum":
                                    $db->query("DELETE FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                                    $bankerData = $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userBanker['userID']}'");
                                    $banker = $bankerData->fetch(PDO::FETCH_ASSOC);
                                    if ($banker['level_tableType'] == 'copper' || $banker['level_tableType'] == 'bronze' || $banker['level_tableType'] == 'silver' || $banker['level_tableType'] == 'gold' || $banker['level_tableType'] == 'platinum') {

                                        $db->query("UPDATE `users` SET `level_tableType` = 'diamond' WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                        $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `CompletedPlatinum` = `CompletedPlatinum` + 1 WHERE `userID` = '{$userBanker['userID']}'");
                                    } else {
                                        $db->query("UPDATE `userTableList` SET `table_ID_platinum` = NULL, `CompletedPlatinum` = `CompletedPlatinum` + 1 WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                    }
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('platinum', '{$userManagerA['userID']}', '{$userGiverA['userID']}', '{$userGiverB['userID']}')");
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('platinum', '{$userManagerB['userID']}', '{$userGiverC['userID']}', '{$userGiverD['userID']}')");
                                    $tableA = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerA['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableA['tableID']}' WHERE `userID` = '{$userManagerA['userID']}' OR `userID` = '{$userGiverA['userID']}' OR `userID` = '{$userGiverB['userID']}'");
                                    $tableB = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerB['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableB['tableID']}' WHERE `userID` = '{$userManagerB['userID']}' OR `userID` = '{$userGiverC['userID']}' OR `userID` = '{$userGiverD['userID']}'");
                                    break;
                                case "diamond":
                                    $db->query("DELETE FROM `tables` WHERE `tableID` = '{$dt['Table']['tableID']}'");
                                    $db->query("UPDATE `userTableList` SET `table_ID_diamond` = NULL, `CompletedDiamond` = `CompletedDiamond` + 1 WHERE `userID` = '{$userBanker['userID']}'"); //Banker -> giver
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('diamond', '{$userManagerA['userID']}', '{$userGiverA['userID']}', '{$userGiverB['userID']}')");
                                    $db->query("INSERT INTO `tables`(`tableType`, `bankerID`, `managerA_ID`, `managerB_ID`) VALUES ('diamond', '{$userManagerB['userID']}', '{$userGiverC['userID']}', '{$userGiverD['userID']}')");
                                    $tableA = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerA['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableA['tableID']}' WHERE `userID` = '{$userManagerA['userID']}' OR `userID` = '{$userGiverA['userID']}' OR `userID` = '{$userGiverB['userID']}'");
                                    $tableB = $db->query("SELECT * FROM `tables` WHERE `bankerID` = '{$userManagerB['userID']}'")->fetch(PDO::FETCH_ASSOC);
                                    $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableB['tableID']}' WHERE `userID` = '{$userManagerB['userID']}' OR `userID` = '{$userGiverC['userID']}' OR `userID` = '{$userGiverD['userID']}'");
                                    break;
                            }
                            //$db->query("DELETE FROM `tables` WHERE `tableID` = '{$userBanker['table_id']}'");
                            SetError("TableCompleted");
                        }
                    } else SetError("Cannot find table");
                } else SetError("Confirm - ERROR");
                break;
            }

        }
    }
    default:
        SetError("Something wrong with data");
}
function SetUserTableList($tableList){
    global $userData;
    global $db;
    global $userTableList;
    $userTableList["id"] = $tableList['id'];
    $userTableList["userID"] = $tableList['userID'];
    $userTableList["copperTableRole"] = "giver";
    if($tableList['table_ID_copper'] != null) {
        $userTableList["table_ID_copper"] = $tableList['table_ID_copper'];
        $userTableList["copperTableRole"] = GetTableRole($tableList['userID'], $tableList['table_ID_copper']);
    }
    else {
        $userTableList["table_ID_copper"] = null;
        $userTableList["copperTableRole"] = null;
    }


    if($tableList['table_ID_bronze'] != null) {
        $userTableList["table_ID_bronze"] = $tableList['table_ID_bronze'];
        $userTableList["bronzeTableRole"] = GetTableRole($tableList['userID'], $tableList['table_ID_bronze']);
    }
    else {
        $userTableList["table_ID_bronze"] = null;
        $userTableList["bronzeTableRole"] = null;
    }


    if($tableList['table_ID_silver'] != null) {
        $userTableList["table_ID_silver"] = $tableList['table_ID_silver'];
        $userTableList["silverTableRole"] = GetTableRole($tableList['userID'], $tableList['table_ID_silver']);
    }
    else {
        $userTableList["table_ID_silver"] = null;
        $userTableList["silverTableRole"] = null;
    }


    if($tableList['table_ID_gold'] != null) {
        $userTableList["table_ID_gold"] = $tableList['table_ID_gold'];
        $userTableList["goldTableRole"] = GetTableRole($tableList['userID'], $tableList['table_ID_gold']);
    }
    else {
        $userTableList["table_ID_gold"] = null;
        $userTableList["goldTableRole"] = null;
    }


    if($tableList['table_ID_platinum'] != null) {
        $userTableList["table_ID_platinum"] = $tableList['table_ID_platinum'];
        $userTableList["platinumTableRole"] = GetTableRole($tableList['userID'], $tableList['table_ID_platinum']);
    }
    else {
        $userTableList["table_ID_platinum"] = null;
        $userTableList["platinumTableRole"] = null;
    }

    if($tableList['table_ID_diamond'] != null) {
        $userTableList["table_ID_diamond"] = $tableList['table_ID_diamond'];
        $userTableList["diamondTableRole"] = GetTableRole($tableList['userID'], $tableList['table_ID_diamond']);
    }
    else {
        $userTableList["table_ID_diamond"] = null;
        $userTableList["diamondTableRole"] = null;
    }
    return $userTableList;
}
function SetTableProfileData($tableID)
{
    global $db;
    global $tableProfile;
    $tablesData = $db->query("SELECT * FROM `tables` WHERE `tableID` = '$tableID'");
    if($tablesData->rowCount() == 1) {
        $tableData = $tablesData->fetch(PDO::FETCH_ASSOC);
        $tableProfileData["tableID"] = $tableData['tableID'];
        $tableProfileData["tableType"] = $tableData['tableType'];
        $tableProfileData["bankerID"] = $tableData['bankerID'];
        //$tableProfileData["managerA_ID"] = $tableData['managerA_ID'];
        $tableProfileData["giverA_ID"] = $tableData['giverA_ID'];
        $tableProfileData["verf_A"] = $tableData['verf_A'];
        $tableProfileData["giverB_ID"] = $tableData['giverB_ID'];
        $tableProfileData["verf_B"] = $tableData['verf_B'];
        //$tableProfileData["managerB_ID"] = $tableData['managerB_ID'];
        $tableProfileData["giverC_ID"] = $tableData['giverC_ID'];
        $tableProfileData["verf_C"] = $tableData['verf_C'];
        $tableProfileData["giverD_ID"] = $tableData['giverD_ID'];
        $tableProfileData["verf_D"] = $tableData['verf_D'];
        return $tableProfileData;
    } else {
        SetError("TableIsNotFound");
        return $tableProfile = null;
    }
}
function GetTableRole($userID, $tableID){
    global $db;
    $tableRole = '';
    //print "userID: [" . $userID . "]\tTableID: [" . $tableID . "]\n";
    if($tableID != null) {
        $tables = $db->query("SELECT * FROM `tables` WHERE `tableID` = '$tableID'");

        if ($tables->rowCount() == 1) {
            $table = $tables->fetch(PDO::FETCH_ASSOC);
            //print "giverA: [" . $table['giverA_ID'] . "]\tgiverB: [" . $table['giverB_ID'] . "]" . "\tgiverC: [" . $table['giverC_ID'] . "]" . "\tgiverD: [" . $table['giverD_ID'] . "]" ."\tTableID: [" . $tableID . "]\n";
            switch (true){
                case ($table['giverA_ID'] == $userID):
                {
                    $tableRole = 'giver';
                    break;
                }
                case ($table['giverB_ID'] == $userID):
                {
                    $tableRole = 'giver';
                    break;
                }
                case ($table['giverC_ID'] == $userID):
                {
                    $tableRole = 'giver';
                    break;
                }
                case ($table['giverD_ID'] == $userID):
                {
                    $tableRole = 'giver';
                    break;
                }
                case ($table['bankerID'] == $userID):{
                    $tableRole =  'banker';
                    break;
                }
                default:
                    //$tableRole = 'giver';
                    SetError("GetTableRole - SomethingWrong - ERROR");
                    break;
            }
            //print "tableRole: \t[" . $tableRole ."]\n";
            /*if ($table['giverA_ID'] == $userID) $tableRole = 'giver';
            if ($table['giverB_ID'] == $userID) $tableRole =  'giver';
            if ($table['giverC_ID'] == $userID) $tableRole =  'giver';
            if ($table['giverD_ID'] == $userID) $tableRole =  'giver';
            if ($table['managerA_ID'] == $userID) $tableRole =  'manager';
            if ($table['managerB_ID'] == $userID) $tableRole =  'manager';
            if ($table['bankerID'] == $userID) $tableRole =  'banker';*/
        } else {
            SetError("GetTableRole - TableIsNotFound - ERROR");
            return 'null';
        }
    } else {
        SetError("TableIsNotFound");
        return 'null';
    }
    return $tableRole;
}
function GetUserData($userID){
    global $db;
    global $userData;
    $users = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userID}'");
    if ($users->rowCount() == 1) {
        $user = $users->fetch(PDO::FETCH_ASSOC);
        $invitedCount = $db->query("SELECT * FROM `users` WHERE `refId` = '{$userID}' AND `verf` = '1'");
        $invitedBy = $db->query("SELECT * FROM `users` WHERE `userID` = '{$user['refId']}'")->fetch(PDO::FETCH_ASSOC);
        $usersTableList = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$userID}'")->fetch(PDO::FETCH_ASSOC);

        $team = $db->query("WITH RECURSIVE descendants AS (
                SELECT userID
                FROM users
                WHERE userID= '{$userID}'
                UNION ALL
                SELECT t.userID
                FROM descendants d, users t
                WHERE t.refId=d.userID
                )
                SELECT * FROM descendants;");

        $userDatas["id"] = $user['userID'];
        $userDatas["username"] = $user['username'];
        $userDatas["refId"] = $user['refId'];
        $userDatas["invitedBy"] = $invitedBy['username'];
        $userDatas["lang"] = $user['lang'];
        $userDatas["userTableList"] = SetUserTableList($usersTableList); //TODO ОШИБКА ЗДЕСЬ
        $userDatas["level_tableType"] = $user['level_tableType'];
        $userDatas["invited"] = $invitedCount->rowCount();
        $userDatas["team"] = $team->rowCount() - 1;
        $userDatas["giftsReceived"] = $user['giftsReceived'];
        return $userDatas;
    } else {
        $userData = null;
        SetError("User is not recognized - GetUserData - ERROR");
    }
    return $userData;
}

function GetRefUserTableList($RefID){
    global $db;
    $refUser = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$RefID}'")->fetch(PDO::FETCH_ASSOC);
    return $refUser;
}
function AnyTable($tableType, $userID){
    global $db;
    //print "AnyTable\n";
    $flag = false;
    $q = $db->query("SELECT * FROM `tables` WHERE
                           (
                               (`giverA_ID` IS NULL AND `giverB_ID` IS NOT NULL AND `giverC_ID` IS NOT NULL AND `giverD_ID` IS NOT NULL) OR
                               (`giverA_ID` IS NOT NULL AND `giverB_ID` IS NULL AND `giverC_ID` IS NOT NULL AND `giverD_ID` IS NOT NULL) OR
                               (`giverA_ID` IS NOT NULL AND `giverB_ID` IS NOT NULL AND `giverC_ID` IS NULL AND `giverD_ID` IS NOT NULL) OR
                               (`giverA_ID` IS NOT NULL AND `giverB_ID` IS NOT NULL AND `giverC_ID` IS NOT NULL AND `giverD_ID` IS NULL)
                               ) AND `tableType` =  '{$tableType}'");

    //`verf_A` = '0',`EnterDateGiverA` = NOW() WHERE //`verf_A` = '0' WHERE
    //`verf_B` = '0',`EnterDateGiverB` = NOW() WHERE //`verf_B` = '0' WHERE
    //`verf_C` = '0',`EnterDateGiverC` = NOW() WHERE //`verf_C` = '0' WHERE
    //`verf_D` = '0',`EnterDateGiverD` = NOW() WHERE //`verf_D` = '0' WHERE
    //`verf_D` = '0',`EnterDateGiverD` = NOW() WHERE //`verf_D` = '0' WHERE

    if ($q->rowCount() > 0) { //Есть столы где недостает 1 дарителя
        $tableID = $q->fetch(PDO::FETCH_ASSOC);
        if ($tableID['giverA_ID'] === NULL && !$flag) {
            //echo "A_1\n";
            $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
            SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
            $flag = true;
            switch ($tableType){
                case "copper":
                    $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "bronze":
                    $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "silver":
                    $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "gold":
                    $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "platinum":
                    $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "diamond":
                    $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
            }

            $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
            return true;
        }
        if ($tableID['giverB_ID'] === NULL && !$flag) {
            //echo "B_1\n";
            $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
            SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
            $flag = true;
            switch ($tableType){
                case "copper":
                    $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "bronze":
                    $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "silver":
                    $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "gold":
                    $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "platinum":
                    $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "diamond":
                    $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
            }
            return true;
        }
        if ($tableID['giverC_ID'] === NULL && !$flag) {
            //echo "C_1\n";
            $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
            SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
            $flag = true;
            switch ($tableType){
                case "copper":
                    $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "bronze":
                    $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "silver":
                    $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "gold":
                    $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "platinum":
                    $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "diamond":
                    $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
            }
            return true;
        }
        if ($tableID['giverD_ID'] === NULL && !$flag) {
            //echo "D_1\n";
            $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
            SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
            $flag = true;
            switch ($tableType){
                case "copper":
                    $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "bronze":
                    $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "silver":
                    $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "gold":
                    $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "platinum":
                    $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
                case "diamond":
                    $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                    break;
            }
            return true;
        }

    } else { //Есть столы где недостает 2х дарителей
        $q = $db->query("SELECT * FROM `tables` WHERE (
                               (`giverA_ID` IS NULL AND `giverB_ID` IS NULL AND `giverC_ID` IS NOT NULL AND `giverD_ID` IS NOT NULL) OR
                               (`giverA_ID` IS NULL AND `giverB_ID` IS NOT NULL AND `giverC_ID` IS NULL AND `giverD_ID` IS NOT NULL) OR
                               (`giverA_ID` IS NOT NULL AND `giverB_ID` IS NULL AND `giverC_ID` IS NULL AND `giverD_ID` IS NOT NULL) OR
                               (`giverA_ID` IS NULL AND `giverB_ID` IS NOT NULL AND `giverC_ID` IS NOT NULL AND `giverD_ID` IS NULL) OR
                               (`giverA_ID` IS NOT NULL AND `giverB_ID` IS NULL AND `giverC_ID` IS NOT NULL AND `giverD_ID` IS NULL) OR
                               (`giverA_ID` IS NOT NULL AND `giverB_ID` IS NOT NULL AND `giverC_ID` IS NULL AND `giverD_ID` IS NULL))
                                AND `tableType` =  '{$tableType}'");
        if ($q->rowCount() > 0) {
            $tableID = $q->fetch(PDO::FETCH_ASSOC);
            if ($tableID['giverA_ID'] === NULL && !$flag) {
                //echo "A_2\n";
                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                $flag = true;
                switch ($tableType){
                    case "copper":
                        $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "bronze":
                        $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "silver":
                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "gold":
                        $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "platinum":
                        $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "diamond":
                        $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                }
                return true;
            }
            if ($tableID['giverB_ID'] === NULL && !$flag) {
                //echo "B_2\n";
                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                $flag = true;
                switch ($tableType){
                    case "copper":
                        $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "bronze":
                        $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "silver":
                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "gold":
                        $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "platinum":
                        $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "diamond":
                        $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                }
                return true;
            }
            if ($tableID['giverC_ID'] === NULL && !$flag) {
                //echo "C_2\n";
                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                $flag = true;
                switch ($tableType){
                    case "copper":
                        $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "bronze":
                        $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "silver":
                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "gold":
                        $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "platinum":
                        $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "diamond":
                        $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                }
                return true;
            }
            if ($tableID['giverD_ID'] === NULL && !$flag) {
                //echo "D_2\n";
                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                $flag = true;
                switch ($tableType){
                    case "copper":
                        $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "bronze":
                        $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "silver":
                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "gold":
                        $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "platinum":
                        $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                    case "diamond":
                        $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                        break;
                }
                return true;
            }
        } else {
            //Есть столы где недостает 3х дарителей
            $q = $db->query("SELECT * FROM `tables` WHERE (
                               (`giverA_ID` IS NULL AND `giverB_ID` IS NULL AND `giverC_ID` IS NULL AND `giverD_ID` IS NOT NULL) OR
                               (`giverA_ID` IS NULL AND `giverB_ID` IS NULL AND `giverC_ID` IS NOT NULL AND `giverD_ID` IS NULL) OR
                               (`giverA_ID` IS NULL AND `giverB_ID` IS NOT NULL AND `giverC_ID` IS NULL AND `giverD_ID` IS NULL) OR
                               (`giverA_ID` IS NOT NULL AND `giverB_ID` IS NULL AND `giverC_ID` IS NULL AND `giverD_ID` IS NULL))
                                AND `tableType` =  '{$tableType}'");
            if ($q->rowCount() > 0) {
                $tableID = $q->fetch(PDO::FETCH_ASSOC);
                if ($tableID['giverA_ID'] === NULL && !$flag) {
                    //echo "A_3\n";
                    $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                    SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                    $flag = true;
                    switch ($tableType){
                        case "copper":
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "bronze":
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "silver":
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "gold":
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "platinum":
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "diamond":
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                    }
                    return true;
                }
                if ($tableID['giverB_ID'] === NULL && !$flag) {
                    //echo "B_3\n";
                    $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                    SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                    $flag = true;
                    switch ($tableType){
                        case "copper":
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "bronze":
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "silver":
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "gold":
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "platinum":
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "diamond":
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                    }
                    return true;
                }
                if ($tableID['giverC_ID'] === NULL && !$flag) {
                    //echo "C_3\n";
                    $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                    SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                    $flag = true;
                    switch ($tableType){
                        case "copper":
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "bronze":
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "silver":
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "gold":
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "platinum":
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "diamond":
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                    }
                    return true;
                }
                if ($tableID['giverD_ID'] === NULL && !$flag) {
                    //echo "D_3\n";
                    $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                    SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                    $flag = true;
                    switch ($tableType){
                        case "copper":
                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "bronze":
                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "silver":
                            $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "gold":
                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "platinum":
                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                        case "diamond":
                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                            break;
                    }
                    return true;
                }
            } else {
                //Есть столы где недостает 4x дарителей
                $q = $db->query("SELECT * FROM `tables` WHERE (`giverA_ID` IS NULL AND `giverB_ID` IS NULL AND `giverC_ID` IS NULL AND `giverD_ID` IS NULL)
                                    AND `tableType` =  '{$tableType}'");

                if ($q->rowCount() > 0) {
                    $tableID = $q->fetch(PDO::FETCH_ASSOC);

                    if ($tableID['giverA_ID'] === NULL && !$flag) {
                        //echo "A_4\n";
                        $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                        SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                        $flag = true;
                        switch ($tableType){
                            case "copper":
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "bronze":
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "silver":
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "gold":
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "platinum":
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "diamond":
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                        }
                        return true;
                    }
                    if ($tableID['giverB_ID'] === NULL && !$flag) {
                        //echo "B_4\n";
                        $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                        SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                        $flag = true;
                        switch ($tableType){
                            case "copper":
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "bronze":
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "silver":
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "gold":
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "platinum":
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "diamond":
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                        }
                        return true;
                    }
                    if ($tableID['giverC_ID'] === NULL && !$flag) {
                        //echo "C_4\n";
                        $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                        SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                        $flag = true;
                        switch ($tableType){
                            case "copper":
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "bronze":
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "silver":
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "gold":
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "platinum":
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "diamond":
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                        }
                        return true;
                    }
                    if ($tableID['giverD_ID'] === NULL && !$flag) {
                        //echo "D_4\n";
                        $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$tableID['tableID']}'");
                        SetNotification("NewGiver", $tableID['tableID'], $tableID['bankerID'], $tableID['giverA_ID'], $tableID['giverB_ID'], $tableID['giverC_ID'], $tableID['giverD_ID']);
                        $flag = true;
                        switch ($tableType){
                            case "copper":
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "bronze":
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "silver":
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "gold":
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "platinum":
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                            case "diamond":
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$tableID['tableID']}' WHERE `userID` = '{$userID}'");
                                break;
                        }
                        return true;
                    }
                }
                SetError("GetTableData - TableIsNotFound - ERROR");
            }
        }

    }
    SetError("TableIsNotFound");
}

function EnterTable($tableType, $userID, $refID){
    global $db;
    //print $refID."\n";
    $user = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userID}'")->fetch(PDO::FETCH_ASSOC);
    $refUserTableList = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$refID}'")->fetch(PDO::FETCH_ASSOC);//GetRefUserTableList($refID);
    switch ($tableType){
        case 'copper':
        {
            if (!empty($refUserTableList['table_ID_copper']) && $refID != 0) {
                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_copper']}'")->fetch(PDO::FETCH_ASSOC);
                switch (true) {
                    case $table['bankerID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");
                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverA_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");
                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                                break;
                        }
                        break;
                    }
                    case $table['giverB_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverC_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverD_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                }
            } else {
                if ($refID == 0) {
                    AnyTable($tableType, $userID);
                    return false;
                }
                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                if ($refID->rowCount() == 1) {
                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                    EnterTable($tableType, $userID, $refID['userID']);
                }
                return false;
            }
            break;
        }
        case 'bronze':
        {
            if (!empty($refUserTableList['table_ID_bronze']) && $refID != 0) {
                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_bronze']}'")->fetch(PDO::FETCH_ASSOC);

                switch (true) {
                    case $table['bankerID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverA_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                                break;
                        }
                        break;
                    }
                    case $table['giverB_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverC_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverD_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                }
            } else {
                if ($refID == 0) {
                    AnyTable($tableType, $userID);
                    return false;
                }
                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                if ($refID->rowCount() == 1) {
                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                    EnterTable($tableType, $userID, $refID['userID']);
                }
                return false;
            }
            break;
        }
        case 'silver':
        {
            if (!empty($refUserTableList['table_ID_silver']) && $refID != 0) {
                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_silver']}'")->fetch(PDO::FETCH_ASSOC);

                switch (true) {
                    case $table['bankerID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverA_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                                break;
                        }
                        break;
                    }
                    case $table['giverB_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverC_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverD_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                }
            } else {
                if ($refID == 0) {
                    AnyTable($tableType, $userID);
                    return false;
                }
                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                if ($refID->rowCount() == 1) {
                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                    EnterTable($tableType, $userID, $refID['userID']);
                }
                return false;
            }
            break;
        }
        case 'gold':
        {
            if (!empty($refUserTableList['table_ID_gold']) && $refID != 0) {
                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_gold']}'")->fetch(PDO::FETCH_ASSOC);

                switch (true) {
                    case $table['bankerID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverA_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                                break;
                        }
                        break;
                    }
                    case $table['giverB_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverC_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverD_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                }
            } else {
                if ($refID == 0) {
                    AnyTable($tableType, $userID);
                    return false;
                }
                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                if ($refID->rowCount() == 1) {
                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                    EnterTable($tableType, $userID, $refID['userID']);
                }
                return false;
            }
            break;
        }
        case 'platinum':
        {
            if (!empty($refUserTableList['table_ID_platinum']) && $refID != 0) {
                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_platinum']}'")->fetch(PDO::FETCH_ASSOC);

                switch (true) {
                    case $table['bankerID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverA_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                                break;
                        }
                        break;
                    }
                    case $table['giverB_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverC_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverD_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                }
            } else {
                if ($refID == 0) {
                    AnyTable($tableType, $userID);
                    return false;
                }
                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                if ($refID->rowCount() == 1) {
                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                    EnterTable($tableType, $userID, $refID['userID']);
                }
                return false;
            }
            break;
        }
        case 'diamond':
        {
            if (!empty($refUserTableList['table_ID_diamond']) && $refID != 0) {
                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_diamond']}'")->fetch(PDO::FETCH_ASSOC);

                switch (true) {
                    case $table['bankerID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverA_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                                break;
                        }
                        break;
                    }
                    case $table['giverB_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverC_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                    case $table['giverD_ID'] == $refID:
                    {
                        switch (true) {
                            case $table['giverA_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0',`EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverB_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0',`EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverC_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0',`EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            case $table['giverD_ID'] == null:
                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0',`EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                break;
                            default:
                                if ($refID == 0) {
                                    AnyTable($tableType, $userID);
                                    return false;
                                }
                                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                                if ($refID->rowCount() == 1) {
                                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                                    EnterTable($tableType, $userID, $refID['userID']);
                                }
                                return false;
                        }
                        break;
                    }
                }
            } else {
                if ($refID == 0) {
                    AnyTable($tableType, $userID);
                    return false;
                }
                $userRefID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$refID}'");
                $userRefID = $userRefID->fetch(PDO::FETCH_ASSOC);
                $refID = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userRefID['refId']}'");

                if ($refID->rowCount() == 1) {
                    $refID = $refID->fetch(PDO::FETCH_ASSOC);

                    EnterTable($tableType, $userID, $refID['userID']);
                }
                return false;
            }
            break;
        }
    }
    return null;
}
function EnterTableReinvest($tableType, $userID, $refId){
    global $db;
    $flag = false;
    $user = $db->query("SELECT * FROM `users` WHERE `userID` = '{$userID}'")->fetch(PDO::FETCH_ASSOC);
    $invitedID = $db->query("SELECT * FROM `users` WHERE `refId` = '{$userID}'");
    if($invitedID->rowCount() > 0){
        $sql = "SELECT * FROM `users` WHERE `refId` = '{$refId}'";
        $q = $db->prepare($sql, array(PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL));
        $q->execute();
        if($q->rowCount() > 0){
            //print "Invited by: " . $userID . "\n";
            while ($row = $q->fetch(PDO::FETCH_NUM, PDO::FETCH_ORI_NEXT)) {
                $data = "UserID: [". $row[0] ."]" . "\t" . "Username: [". $row[1] ."]" . "\t" . "refID: [". $row[2] ."]" . "\n";
                $RefUsersData[] = $row[0];

                //print $data;
                $refUserTableList = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$row[0]}'");
                if($refUserTableList->rowCount() == 1) {
                    $refUserTableList = $refUserTableList->fetch(PDO::FETCH_ASSOC);
                    /*print "\ntable_ID_copper: \t[" .$refUserTableList['table_ID_copper'] . "]\n";
                    print "table_ID_bronze: \t[" .$refUserTableList['table_ID_bronze'] . "]\n";
                    print "table_ID_silver: \t[" .$refUserTableList['table_ID_silver'] . "]\n";
                    print "table_ID_gold: \t[" .$refUserTableList['table_ID_gold'] . "]\n";
                    print "table_ID_platinum: \t[" .$refUserTableList['table_ID_platinum'] . "]\n";
                    print "table_ID_diamond: \t[" .$refUserTableList['table_ID_diamond'] . "]\n";*/
                    switch ($tableType) {
                        case 'copper':
                        {
                            if (!empty($refUserTableList['table_ID_copper'])) {
                                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_copper']}'")->fetch(PDO::FETCH_ASSOC);
                                if ($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                    || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                    switch (true) {
                                        case $table['giverA_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverB_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverC_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverD_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                    }
                            }
                            break;
                        }
                        case 'bronze':
                        {
                            if (!empty($refUserTableList['table_ID_bronze'])) {
                                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_bronze']}'")->fetch(PDO::FETCH_ASSOC);
                                if ($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                    || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                    switch (true) {
                                        case $table['giverA_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverB_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverC_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverD_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                    }
                            }
                            break;
                        }
                        case 'silver':
                        {
                            if (!empty($refUserTableList['table_ID_silver'])) {
                                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_silver']}'")->fetch(PDO::FETCH_ASSOC);
                                switch (true) {
                                    case $table['giverA_ID'] == null:
                                    {
                                        //print "giverAFirstRow\n";
                                        $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                        SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                        $flag = true;
                                        return null;
                                        break;
                                    }
                                    case $table['giverB_ID'] == null:
                                    {
                                        //print "giverB\n";
                                        $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                        SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                        $flag = true;
                                        return null;
                                        break;
                                    }
                                    case $table['giverC_ID'] == null:
                                    {
                                        //print "giverC\n";
                                        $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                        SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                        $flag = true;
                                        return null;
                                        break;
                                    }
                                    case $table['giverD_ID'] == null:
                                    {
                                        //print "giverD\n";
                                        $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                        $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                        SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                        $flag = true;
                                        return null;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                        case 'gold':
                        {
                            if (!empty($refUserTableList['table_ID_gold'])) {
                                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_gold']}'")->fetch(PDO::FETCH_ASSOC);
                                if ($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                    || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                    switch (true) {
                                        case $table['giverA_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverB_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverC_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverD_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                    }
                            }
                            break;
                        }
                        case 'platinum':
                        {
                            if (!empty($refUserTableList['table_ID_platinum'])) {
                                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_platinum']}'")->fetch(PDO::FETCH_ASSOC);
                                if ($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                    || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                    switch (true) {
                                        case $table['giverA_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverB_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverC_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverD_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                    }
                            }
                            break;
                        }
                        case 'platinum':
                        {
                            if (!empty($refUserTableList['table_ID_platinum'])) {
                                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_platinum']}'")->fetch(PDO::FETCH_ASSOC);
                                if ($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                    || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                    switch (true) {
                                        case $table['giverA_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverB_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverC_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverD_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                    }
                            }
                            break;
                        }
                        case 'diamond':
                        {
                            if (!empty($refUserTableList['table_ID_diamond'])) {
                                $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_diamond']}'")->fetch(PDO::FETCH_ASSOC);
                                if ($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                    || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                    switch (true) {
                                        case $table['giverA_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverB_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverC_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                        case $table['giverD_ID'] == null:
                                        {
                                            $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                            $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                            SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                            $flag = true;
                                            return null;
                                            break;
                                        }
                                    }
                            }
                            break;
                        }
                    }
                }
            }

            if(!$flag){ //ЗДЕСЬ ПРИГЛАШЕННЫЕ ПРИГЛАШЕННЫМИ
                foreach ($RefUsersData as $value){
                    $sql = "SELECT * FROM `users` WHERE `refId` = '{$value}'";
                    $q = $db->prepare($sql, array(PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL));
                    $q->execute();
                    $r = $db->query($sql)->fetch(PDO::FETCH_ASSOC);
                    //print "Invited by: " . $value . "\n";
                    while ($row = $q->fetch(PDO::FETCH_NUM, PDO::FETCH_ORI_NEXT)) {
                        if($flag) break;
                        $data = "UserID: [". $row[0] ."]" . "\t" . "Username: [". $row[1] ."]" . "\t" . "refID: [". $row[2] ."]" . "\n";
                        //print $data;
                        $refUserTableList = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$row[0]}'")->fetch(PDO::FETCH_ASSOC);
                        switch ($tableType){
                            case 'copper':
                            {
                                if (!empty($refUserTableList['table_ID_copper'])) {
                                    $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_copper']}'")->fetch(PDO::FETCH_ASSOC);
                                    if($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                        || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                        switch (true) {
                                            case $table['giverA_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverB_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverC_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverD_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_copper` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                        }
                                }
                                break;
                            }
                            case 'bronze':
                            {
                                if (!empty($refUserTableList['table_ID_bronze'])) {
                                    $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_bronze']}'")->fetch(PDO::FETCH_ASSOC);
                                    if($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                        || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                        switch (true) {
                                            case $table['giverA_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverB_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverC_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverD_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_bronze` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                        }
                                }
                                break;
                            }
                            case 'silver':
                            {
                                if (!empty($refUserTableList['table_ID_silver'])) {
                                    $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_silver']}'")->fetch(PDO::FETCH_ASSOC);
                                    if($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                        || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                        switch (true) {
                                            case $table['giverA_ID'] == null:
                                            {
                                                //print "giverA\n";
                                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverB_ID'] == null:
                                            {
                                                //print "giverB\n";
                                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverC_ID'] == null:
                                            {
                                                //print "giverC\n";
                                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverD_ID'] == null:
                                            {
                                                //print "giverD\n";
                                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_silver` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                        }
                                }
                                break;
                            }
                            case 'gold':
                            {
                                if (!empty($refUserTableList['table_ID_gold'])) {
                                    $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_gold']}'")->fetch(PDO::FETCH_ASSOC);
                                    if($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                        || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                        switch (true) {
                                            case $table['giverA_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverB_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverC_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverD_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_gold` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                        }
                                }
                                break;
                            }
                            case 'platinum':
                            {
                                if (!empty($refUserTableList['table_ID_platinum'])) {
                                    $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_platinum']}'")->fetch(PDO::FETCH_ASSOC);
                                    if($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                        || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                        switch (true) {
                                            case $table['giverA_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverB_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverC_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverD_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_platinum` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                        }
                                }
                                break;
                            }
                            case 'diamond':
                            {
                                if (!empty($refUserTableList['table_ID_diamond'])) {
                                    $table = $db->query("SELECT * FROM `tables` WHERE `tableID` = '{$refUserTableList['table_ID_diamond']}'")->fetch(PDO::FETCH_ASSOC);
                                    if($table['bankerID'] == $row[0] || $table['giverA_ID'] == $row[0]
                                        || $table['giverB_ID'] == $row[0] || $table['giverC_ID'] == $row[0] || $table['giverD_ID'] == $row[0])
                                        switch (true) {
                                            case $table['giverA_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverA_ID` = '{$userID}', `verf_A` = '0', `EnterDateGiverA` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverB_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverB_ID` = '{$userID}', `verf_B` = '0', `EnterDateGiverB` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverC_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverC_ID` = '{$userID}', `verf_C` = '0', `EnterDateGiverC` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                            case $table['giverD_ID'] == null:
                                            {
                                                $db->query("UPDATE `tables` SET `giverD_ID` = '{$userID}', `verf_D` = '0', `EnterDateGiverD` = NOW() WHERE `tableID` = '{$table['tableID']}'");
                                                $db->query("UPDATE `userTableList` SET `table_ID_diamond` = '{$table['tableID']}' WHERE `userID` = '{$userID}'");
                                                SetNotification("NewGiver", $table['tableID'], $table['bankerID'], $table['giverA_ID'], $table['giverB_ID'], $table['giverC_ID'], $table['giverD_ID']);
                                                $flag = true;
                                                return null;
                                                break;
                                            }
                                        }
                                }
                                break;
                            }
                        }
                    }
                    if(!$flag) if(AnyTable($tableType, $userID)) {
                        return true;
                    }
                }
            }

        } //else AnyTable($tableType, $userID);
        //echo $q->rowCount()."\n";

        //print_r($invitedID->);
    } else if(AnyTable($tableType, $userID)) {
        return true;
    }
    $refUserTableList = $db->query("SELECT * FROM `userTableList` WHERE `userID` = '{$refId}'")->fetch(PDO::FETCH_ASSOC);

}
function SetError($text)
{
    global $userData;
    $userData["userProfile"] = null;
    $userData["error"]["isError"] = true;
    $userData["error"]["errorText"] = $text;
}
function SetNotification($text, $tableID, $banker, $giverA, $giverB, $giverC, $giverD){
    global $userData;
    $userData["notification"]["notificationText"] = $text;

    $userData["notification"]["tableID"] = $tableID;
    $userData["notification"]["bankerID"] = $banker;
    //$userData["notification"]["managerA_ID"] = $managerA;
    //$userData["notification"]["managerB_ID"] = $managerB;
    $userData["notification"]["giverA_ID"] = $giverA;
    $userData["notification"]["giverB_ID"] = $giverB;
    $userData["notification"]["giverC_ID"] = $giverC;
    $userData["notification"]["giverD_ID"] = $giverD;

    $userData["notification"]["isNotify"] = true;
    $userData["notification"]["tableType"] = "copper";

}
function SetNotificationWithTableType($text,$tableType, $tableID, $banker, $giverA, $giverB, $giverC, $giverD){
    global $userData;
    $userData["notification"]["notificationText"] = $text;

    $userData["notification"]["tableID"] = $tableID;
    $userData["notification"]["bankerID"] = $banker;
    //$userData["notification"]["managerA_ID"] = $managerA;
    //$userData["notification"]["managerB_ID"] = $managerB;
    $userData["notification"]["giverA_ID"] = $giverA;
    $userData["notification"]["giverB_ID"] = $giverB;
    $userData["notification"]["giverC_ID"] = $giverC;
    $userData["notification"]["giverD_ID"] = $giverD;

    $userData["notification"]["isNotify"] = true;
    $userData["notification"]["tableType"] = $tableType;

}
/*


 */

echo json_encode($userData, JSON_UNESCAPED_UNICODE);
?>
