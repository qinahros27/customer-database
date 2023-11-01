using src.FileHelper;
using src.Feature;
internal class Program
{
    private static void Main(string[] args)
    {
        //create new customer
        Customer customer1 = new Customer(1,"Anh","Nguyen","anh@gmail.com","abc");
        Customer customer2 = new Customer(2, "Han","Nguyen","han@gmail.com","abc");
        //create customer database
        CustomerDatabase customerDatabase = new CustomerDatabase() ;
        
        // Run customer database function : add, delete, update, read, undo , redo
        customerDatabase.AddCustomer(customer1);
        customerDatabase.AddCustomer(customer2);
        Console.WriteLine("Customer database after add: ");
        customerDatabase.ViewDatabase();
        customerDatabase.UpdateCustomer(new Customer(1,"Anh","Nguyen","anh123@gmail.com","abc"));
        customerDatabase.DeleteCustomer(customer2);
        Console.WriteLine("Customer database after update customer1 and delete customer2: ");
        customerDatabase.ViewDatabase();
        customerDatabase.Undo();
        Console.WriteLine("Customer database after undo - undo delete customer2 action. Customer2 apppear in database: ");
        customerDatabase.ViewDatabase();
        customerDatabase.Redo();
        Console.WriteLine("Customer database after redo - redo delete customer2 action. Customer2 disapppear in database: ");
        customerDatabase.ViewDatabase();
        Console.WriteLine("Read only customer1: ");
        customerDatabase.SearchCustomer(1);
       
        // Add data to file 
        FileHelper file = new FileHelper("customers.csv");
        string[] customerData = customerDatabase.AddToFile();
        for(int i=0; i<customerData.Length; i++) {
            file.AddNewCustomer(customerData[i]+"\n");
        }
        
        //Read data from file
        Console.WriteLine("Data from file: ");
        foreach (var line in file.GetAllData())
        {
            Console.WriteLine(line);
        } 
    }
}
