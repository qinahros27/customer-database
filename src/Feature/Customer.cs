namespace src.Feature 
{
    class Customer 
    {
        private readonly int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _address;

        public Customer(int id, string firstName, string lastName, string email, string address) {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _address = address;
        }

        public int Id {
            get{return _id;}
        }

        public string Email {
            set {_email =value;}
            get{return _email;}
        }

        public string FirstName {
            set {_firstName = value;}
            get{return _firstName;}
        }

        public string LastName {
            set {_lastName = value;}
            get{return _lastName;}
        }

        public string Address {
            set {_address = value;}
            get{return _address;}
        }
    }
}