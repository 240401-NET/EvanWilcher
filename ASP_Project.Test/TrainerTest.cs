using Moq;
using ASP_Project.Service;
using ASP_Project.Model;
using ASP_Project.Controller.Exceptoins;
using ASP_Project.Repository;

namespace ASP_Project.Test;

public class TrainerTest
{
    [Fact]
    public void TestGetTrainerByID_Success()
    {
        int testID = 1;
        Mock<ITrainerRepo> trainerRepoMock = new();
        Trainer fakeTrainer = new Trainer{
                                            Id = testID,
                                            Name = "TestName"
                                         };

        trainerRepoMock.Setup(repo => repo.GetByID(testID)).Returns(fakeTrainer);
        TrainerService trainerService = new(trainerRepoMock.Object);

        Trainer? testTrainer = (Trainer)trainerService.GetByID(testID);

        Assert.NotNull(testTrainer);

        trainerRepoMock.Verify(repo => repo.GetByID(testID), Times.Exactly(1));
    }
    
    [Fact]
    public void TestGetTrainerByID_Fail()
    {
        int testID = -1;
        Mock<ITrainerRepo> trainerRepoMock = new();
        Trainer fakeTrainer = new Trainer{
                                            Id = 1,
                                            Name = "TestName"
                                         };

        trainerRepoMock.Setup(repo => repo.GetByID(1)).Returns(fakeTrainer);
        TrainerService trainerService = new(trainerRepoMock.Object);

        Assert.Throws<NullReferenceException>(() => trainerService.GetByID(testID));

        trainerRepoMock.Verify(repo => repo.GetByID(testID), Times.Exactly(1));
    }
    
    [Theory]
    [InlineData("Soulem")]
    [InlineData("April")]
    [InlineData("    Titi")]
    public void TestCreateTrainer_Success(string _name){
        Mock<ITrainerRepo> trainerRepoMock = new();
        
        Trainer fakeTrainer = new Trainer{
                                            Id = 1,
                                            Name = _name.Trim()
                                         };
        trainerRepoMock.Setup(repo => repo.CreateNew(_name.Trim())).Returns(fakeTrainer);
        TrainerService trainerService = new(trainerRepoMock.Object);

        Trainer? testTrainer = trainerService.CreateNew(_name);
        Assert.NotNull(testTrainer);
        trainerRepoMock.Verify(repo => repo.CreateNew(_name.Trim()), Times.Exactly(1));
    }
    
    [Theory]
    [InlineData("!$$$$")]
    [InlineData("Apr il")]
    [InlineData("    Ti=Ti")]
    public void TestCreateTrainer_BadName_Fail(string _name){
        Mock<ITrainerRepo> trainerRepoMock = new();
        
        Trainer fakeTrainer = new Trainer{
                                            Id = 1,
                                            Name = "TestName"
                                         };
        trainerRepoMock.Setup(repo => repo.CreateNew("TestName")).Returns(fakeTrainer);
        TrainerService trainerService = new(trainerRepoMock.Object);

        Assert.Throws<BadNameException>(() => trainerService.CreateNew(_name));
        trainerRepoMock.Verify(repo => repo.CreateNew(_name), Times.Exactly(0));
    }
    
    [Theory]
    [InlineData(null)]
    public void TestCreateTrainer_NULL_Fail(string _name){
        Mock<ITrainerRepo> trainerRepoMock = new();
        
        Trainer fakeTrainer = new Trainer{
                                            Id = 1,
                                            Name = "TestName"
                                         };
        trainerRepoMock.Setup(repo => repo.CreateNew("TestName")).Returns(fakeTrainer);
        TrainerService trainerService = new(trainerRepoMock.Object);

        Assert.Throws<NullReferenceException>(() => trainerService.CreateNew(_name));
        trainerRepoMock.Verify(repo => repo.CreateNew(_name), Times.Exactly(0));
    }

    [Fact]
    public void TestDeleteTrainer_Success()
    {
        int testID = 1;
        Mock<ITrainerRepo> trainerRepoMock = new();
        Trainer fakeTrainer = new Trainer{
                                            Id = testID,
                                            Name = "TestName"
                                         };

        trainerRepoMock.Setup(repo => repo.DeleteByID(testID)).Returns(fakeTrainer);
        trainerRepoMock.Setup(repo => repo.DoesTrainerExist(testID)).Returns(true);

        TrainerService trainerService = new(trainerRepoMock.Object);

        Trainer? testTrainer = (Trainer)trainerService.DeleteByID(testID);

        Assert.NotNull(testTrainer);

        trainerRepoMock.Verify(repo => repo.DeleteByID(testID), Times.Exactly(1));
        trainerRepoMock.Verify(repo => repo.DoesTrainerExist(testID), Times.Exactly(1));
    }

    [Fact]
    public void TestDeleteTrainer_Fail()
    {
        int testID = -1;
        Mock<ITrainerRepo> trainerRepoMock = new();
        Trainer fakeTrainer = new Trainer{
                                            Id = 1,
                                            Name = "TestName"
                                         };

        trainerRepoMock.Setup(repo => repo.DeleteByID(1)).Returns(fakeTrainer);
        trainerRepoMock.Setup(repo => repo.DoesTrainerExist(1)).Returns(true);

        TrainerService trainerService = new(trainerRepoMock.Object);

        Assert.Throws<NullReferenceException>(() => trainerService.DeleteByID(testID));

        trainerRepoMock.Verify(repo => repo.DeleteByID(testID), Times.Exactly(0));
        trainerRepoMock.Verify(repo => repo.DoesTrainerExist(testID), Times.Exactly(1));
    }
    [Fact]
    public void TestGetAll_Success(){
        Mock<ITrainerRepo> trainerRepoMock = new();
        IEnumerable<Trainer> fakeTrainers = [
        new Trainer{
                        Id = 1,
                        Name = "TestName"
                    }];
        trainerRepoMock.Setup(repo => repo.GetAll()).Returns(fakeTrainers);
        TrainerService trainerService = new(trainerRepoMock.Object);
        Assert.Single(trainerService.GetAll());
        trainerRepoMock.Verify(repo => repo.GetAll(), Times.Exactly(1));
    }

    [Theory]
    [InlineData("Soulem")]
    [InlineData("April")]
    [InlineData("    Titi")]
    public void TestEditTrainerByID_Success(string _name)
    {
        Mock<ITrainerRepo> trainerRepoMock = new();
        Trainer tempTrainer = new(){
            Id = 1,
            Name = _name.Trim()
        };

        trainerRepoMock.Setup(repo => repo.EditByID(1, _name.Trim())).Returns(tempTrainer);
        trainerRepoMock.Setup(repo => repo.DoesTrainerExist(1)).Returns(true);
        TrainerService tempTrainerService = new(trainerRepoMock.Object);

        Trainer? testTrainer = tempTrainerService.EditByID(1, _name);

        Assert.Equal(tempTrainer.Name, testTrainer.Name);
        trainerRepoMock.Verify(repo => repo.EditByID(1, _name.Trim()), Times.Exactly(1));

    }

    [Theory]
    [InlineData("!$$$$")]
    [InlineData("Apr il")]
    [InlineData("    Ti=Ti")]
    public void TestEditTrainerByID_BadName_Fail(string _name)
    {
        Mock<ITrainerRepo> trainerRepoMock = new();
        Trainer tempTrainer = new(){
            Id = 1,
            Name = "TestName"
        };

        trainerRepoMock.Setup(repo => repo.EditByID(1, "TestName")).Returns(tempTrainer);
        trainerRepoMock.Setup(repo => repo.DoesTrainerExist(1)).Returns(true);
        TrainerService tempTrainerService = new(trainerRepoMock.Object);

        Assert.Throws<BadNameException>(() => tempTrainerService.EditByID(1, _name));
        trainerRepoMock.Verify(repo => repo.EditByID(1, _name), Times.Exactly(0));

    }
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(999)]
    public void TestEditTrainerByID_NULL_Fail(int _id)
    {
        Mock<ITrainerRepo> trainerRepoMock = new();
        Trainer tempTrainer = new(){
            Id = 1,
            Name = "TestName"
        };

        trainerRepoMock.Setup(repo => repo.EditByID(1, "TestName")).Returns(tempTrainer);
        trainerRepoMock.Setup(repo => repo.DoesTrainerExist(1)).Returns(true);
        TrainerService tempTrainerService = new(trainerRepoMock.Object);

        Assert.Throws<NullReferenceException>(() => tempTrainerService.EditByID(_id, "TestName"));
        trainerRepoMock.Verify(repo => repo.EditByID(1, "TestName"), Times.Exactly(0));

    }
}

