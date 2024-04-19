namespace ASP_Project.Controller.Exceptoins;

public class BadNameException:Exception{
    public BadNameException(){

    }
    public BadNameException(string _message):base(_message){

    }
    public BadNameException(string _message, Exception _inner):base(_message, _inner){

    }
}

public class TeamSizeException:Exception{
    public TeamSizeException(){

    }
    public TeamSizeException(string _message):base(_message){

    }
    public TeamSizeException(string _message, Exception _inner):base(_message, _inner){

    }
}