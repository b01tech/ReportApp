namespace ReportApp.Models;

public static class User
{
    private static SortedSet<string> Users = new SortedSet<string> {
        "Bruno Marçau", "Roni Cleber", "Marcelo Moreira", "Dimas dos Reis", "Paulo", "Israel Peres","Welington"
    };

    public static SortedSet<string> GetUsers()
    {
        return Users;
    }

    public static SortedSet<string> GetManagers()
    {
        return new SortedSet<string> { "Roni Cleber" };
    }
}
