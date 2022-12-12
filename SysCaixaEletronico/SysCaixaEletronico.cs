using System.Globalization;
using SysCaixaEletronico.userData;

namespace SysCaixaEletronico;

public static class SysCaixaEletronico
{
    public static void Main()
    {
        var userList = new List<User>();
        InitialMenu(userList);
    }
    public static void InitialMenu(List<User> userList)
    {
        Console.Clear();

        Console.WriteLine("***** Cash Machine *****");
        Console.WriteLine("1 - Access the cashier");
        Console.WriteLine("2 - Register a new user");
        Console.WriteLine("0 - Exit");

        int option = int.Parse(Console.ReadLine() ?? string.Empty);
        // Se a resposta não for nula, ela retorna o que vem a esquerda, se não, retorna o que vem a direita, no caso uma string de cadeia vazia.

        switch (option)
        {
            case 0: System.Environment.Exit(0);
                break;
            case 1:
            {
                if (userList.Count == 0)
                {
                    Console.WriteLine("\nNo existing users!");
                    Thread.Sleep(2000);
                    InitialMenu(userList);
                }
                AccessCashier(userList);
                break;
            }
            case 2:
            {
                var user = new User("", "");
                RegisterUser(user);

                var userExist = userList.FirstOrDefault(u => u.Name == user.Name); // Verificando se o usuário já existe
                if (userExist != null)
                {
                    Console.WriteLine("\nThis username is already in use.");
                    Thread.Sleep(2000);
                    InitialMenu(userList);
                }
                
                userList.Add(user);
                Console.WriteLine("\nSuccessfully registered user!");
                Thread.Sleep(2000);
                InitialMenu(userList);
                break;
            }
            default: InitialMenu(userList);
                break;
        }
    }
    public static void AccessCashier(List<User> userList)
    {
        Console.Clear();
        var userLogged = UserLogin(userList);
        BankMenu(userList, userLogged);
    }
    public static User RegisterUser(User user)
    {
        Console.Clear();

        Console.WriteLine("***** Register user *****");
        Console.WriteLine("Name: ");
        user.Name = Console.ReadLine();
        Console.WriteLine("Password: ");
        user.Password = Console.ReadLine();

        if (user.Name == string.Empty || user.Password == string.Empty)
        {
            Console.WriteLine("\nSome fields are wrong! Please try again.");
            Thread.Sleep(2000);
            RegisterUser(user);
        }
        return user;
    }
    public static void BankMenu(List<User> userList, User userLogged)
    {
        Console.Clear();
        
        Console.WriteLine("***** SysCashier *****");
        Console.WriteLine("1 - Deposit");
        Console.WriteLine("2 - Withdraw");
        Console.WriteLine("3 - Check Balance");
        Console.WriteLine("0 - Return");

        int option = int.Parse(Console.ReadLine() ?? string.Empty);

        switch (option)
        {
            case 0: InitialMenu(userList);
                break;
            case 1:
            {
                Deposit(userLogged);
                Console.WriteLine("Amount deposited successfully!");
                Thread.Sleep(2000);
                BankMenu(userList, userLogged);
                break;
            }
            case 2:
            {
                if (userLogged.Balance == 0 || userLogged.Balance.ToString(CultureInfo.InvariantCulture) == string.Empty)
                {
                    Console.WriteLine("\nThe entered value cannot be withdrawn because it is negative or null. Try again!");
                    Thread.Sleep(2000);
                    BankMenu(userList, userLogged);
                }
                Withdraw(userLogged);
                Console.WriteLine("Amount withdrawn successfully!");
                Thread.Sleep(2000);
                BankMenu(userList, userLogged);
                break;
            }
            case 3:
            {
                CheckBalance(userLogged);
                BankMenu(userList, userLogged);
                break;
            }
            default: BankMenu(userList, userLogged);
                break;
        }
    }
    public static User Deposit(User user)
    {
        Console.Clear();

        Console.WriteLine("***** Deposit *****");
        Console.WriteLine("Enter an amount to be deposited: ");
        decimal value = decimal.Parse(Console.ReadLine() ?? string.Empty);

        if (value < 0 || value.ToString(CultureInfo.InvariantCulture) == string.Empty) // Converte a representação da cadeia de caracteres de um número no Decimal equivalente.
        {
            Console.WriteLine("\nThe balance is null. Try again!");
            Thread.Sleep(2000);
            Deposit(user);
        }

        user.Balance = (user.Balance + value);
        Console.WriteLine($"\nCurrent account balance: {user.Balance}.");
        Thread.Sleep(2000);
        return user;
    }

    public static User Withdraw(User user)
    {
        Console.Clear();
        
        Console.WriteLine("***** Withdraw *****");
        Console.WriteLine("Enter an amount to withdraw: ");
        decimal value = decimal.Parse(Console.ReadLine() ?? string.Empty);

        if (value > user.Balance)
        {
            Console.WriteLine("\nInsufficient funds!");
            Thread.Sleep(2000);
            Withdraw(user);
        }

        if (value < 0)
        {
            Console.WriteLine("\nIt is not possible to remove this value!");
            Thread.Sleep(2000);
            Withdraw(user);
        }

        user.Balance = (user.Balance - value);
        Console.WriteLine($"\nCurrent account balance: {user.Balance}.");
        Thread.Sleep(2000);
        return user;
    }
    public static void CheckBalance(User user)
    {
        Console.Clear();

        Console.WriteLine("***** Balance *****");
        Console.WriteLine($"Current balance: {user.Balance}.");
        Thread.Sleep(2000);
    }

    public static User UserLogin(List<User> userList)
    {
        Console.Clear();

        Console.WriteLine("***** Login *****");
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        Console.WriteLine("Passsword: ");
        var password = Console.ReadLine();

        var userExist = userList.Find(u => u.Name == name && u.Password == password);

        if (userExist == null)
        {
            Console.WriteLine("\nSome fields are wrong! Please try again.");
            Thread.Sleep(2000);
            InitialMenu(userList);
        }

        Console.WriteLine($"Welcome to {userExist.Name}.");
        Thread.Sleep(2000);
        return userExist;
    }
}