namespace Project1Test;

using System.Text.Json;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestSaveTrainer()
    {
        Dictionary<string, Trainer> tempData = new();
        Trainer tempTrainer = new();
        tempTrainer.name = "TestUser";
        tempTrainer.team = new();
        tempData.Add("TestUser", tempTrainer);
        FileService.SaveData(tempTrainer);
        tempData = JsonSerializer.Deserialize<Dictionary<string, Trainer>>(File.ReadAllText(Globals.FILE));
        bool result = tempData.Keys.Contains(tempTrainer.name);
        Assert.AreEqual(true, result);
        if (tempData.Keys.Contains(tempTrainer.name)){
            FileService.SaveData(null, tempTrainer);
        }
    }
}