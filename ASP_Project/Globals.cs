using System.Globalization;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public static class Globals{
    public const string DISPLAYBADNAMEERRORMESSAGE = "Did not find the pokemon, Please ensure the Pokemon's name is correct."; 
    public const string FILETEAMS = "SavedTeamsByTrainers.json";
    public const string FILE = "SavedTeamsByTrainerName.json";
    public const string TESTFILE = "test.json";
    public const string SQLSERVERCREDENTIALS = "serverCredentials.md";
    private static string dateTimeNow = DateTime.Now.ToString("MM-dd-yy_Hmmss", CultureInfo.InvariantCulture);
    public static string LOGFILE{
       get { return "PKMAppErrorLog_" +  dateTimeNow + ".md"; }
       set{}
    }
     public const int MAXPKMTEAMSIZE = 6; 
}