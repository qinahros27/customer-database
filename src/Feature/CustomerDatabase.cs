using src.FileHelper;

namespace src.Feature 
{
    class CustomerDatabase 
    {
        private List<Customer> customers;
        private Stack<(String PrevAction, Customer Customer)> undoStack;
        private Stack<(String PrevAction, Customer Customer)> redoStack;

        public CustomerDatabase()
        {
            customers = new List<Customer>();
            undoStack = new Stack<(String PrevAction, Customer Customer)>();
            redoStack = new Stack<(String PrevAction, Customer Customer)>();
        }

        public bool CheckEmail(string email) {
            var existedCheck = customers.Exists(c => c.Email == email);
            if(existedCheck == true) {
                return true;
            }
            else {
                return false;
            }
        }

        public bool CheckId(int id) {
            var existedCheck = customers.Exists(c => c.Id == id);
            if(existedCheck == true) {
                return true;
            }
            else {
                return false;
            }
        }

        public void AddCustomer(Customer customer) {
            var existingEmail = CheckEmail(customer.Email);
            var existingId = CheckId(customer.Id);
            if(existingEmail != false || existingId != false) 
            {
                throw ExceptionHandler.EmailException();
            }
            else 
            {
                customers.Add(customer);
                var action = (PrevAction: "Add", Customer: customer);
                undoStack.Push(action);
                redoStack.Clear();
            }
        }

        public void ReadingCustomer(Customer customer) {
            var existingCustomer =  CheckId(customer.Id);
            if(existingCustomer == false) 
            {
                throw ExceptionHandler.FindCustomerException();
            }
            else 
            {
                Console.WriteLine($"Id: {customer.Id} ; Firstname: {customer.FirstName} ; Lastname: {customer.LastName} ; Email: {customer.Email} ; Address: {customer.Address}");
            }
        }

        public void UpdateCustomer(Customer updateInfo) {
            var existingCustomer = customers.FirstOrDefault(c => c.Id == updateInfo.Id);
            if(existingCustomer == null) 
            {
                throw ExceptionHandler.FindCustomerException();
            }
            else 
            {
                if(existingCustomer.Email != updateInfo.Email) {
                    var checkEmail = customers.FirstOrDefault(c => c.Email == updateInfo.Email); 
                    if(checkEmail != null) {
                        throw ExceptionHandler.UpdateCustomerException();
                    }
                }      
                existingCustomer.Email = updateInfo.Email;
                existingCustomer.FirstName = updateInfo.FirstName;
                existingCustomer.LastName = updateInfo.LastName;
                existingCustomer.Address = updateInfo.Address;
            }
        }

        public void DeleteCustomer(Customer customer) {
            var existingCustomer = CheckId(customer.Id);
            if(existingCustomer == false) 
            {
                throw ExceptionHandler.FindCustomerException();
            }
            else 
            {
                customers.Remove(customer);
                var action = (PrevAction: "Delete" ,Customer: customer);
                undoStack.Push(action);
                redoStack.Clear();
            }
        }

        public void SearchCustomer(int id) {
            var existingCustomer = customers.FirstOrDefault(c => c.Id == id);
            if(existingCustomer != null) 
            {
                Console.WriteLine($"Id: {existingCustomer.Id} ; Firstname: {existingCustomer.FirstName} ; Lastname: {existingCustomer.LastName} ; Email: {existingCustomer.Email} ; Address: {existingCustomer.Address}");
            }
            else 
            {
                throw ExceptionHandler.FindCustomerException();
            }
        }

        public void Undo() {
            if(undoStack.Count>0) 
            {
                var action = undoStack.Pop();
                if(action.PrevAction == "Add") {
                    //do the opposite action 
                    customers.Remove(action.Customer);
                    //add the type of the undo action
                    var redoAdd = (PrevAction: "Delete" , Customer: action.Customer);
                    redoStack.Push(redoAdd);
                }
                else if(action.PrevAction == "Delete") {
                    customers.Add(action.Customer);
                    var redoDelete = (PrevAction: "Add" , Customer: action.Customer);
                    redoStack.Push(redoDelete);
                }
            }
        }

        public void Redo() {
            if(redoStack.Count > 0) 
            {
                var action = redoStack.Pop();
                if(action.PrevAction == "Add") {
                    customers.Remove(action.Customer);
                }
                else if(action.PrevAction == "Delete") {
                    // do the opposite action 
                    customers.Add(action.Customer);
                }
            }
        }

        public void ViewDatabase() {
            foreach(var customer in customers) 
            {
                Console.WriteLine($"Id: {customer.Id} ; Firstname: {customer.FirstName} ; Lastname: {customer.LastName} ; Email: {customer.Email} ; Address: {customer.Address}");
            }
        } 

        public string[] AddToFile() {
            List<string> data = new List<string>();
            foreach(var customer in customers) 
            {
                data.Add($"{customer.Id},{customer.FirstName},{customer.LastName},{customer.Email},{customer.Address}");
            }
            return data.ToArray();
        }  
    }
}