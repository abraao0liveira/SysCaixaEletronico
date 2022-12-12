namespace SysCaixaEletronico.userData;

public class User
{
    public User(string? name, string? password, decimal balance = 0)
    {
        Name = name;
        Password = password;
        Balance = balance;
        Users = new List<User>();
    }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public decimal Balance { get; set; }
    public List<User> Users { get; set; }
}