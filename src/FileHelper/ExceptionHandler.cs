using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace src.FileHelper 
{
    public class ExceptionHandler : Exception
    {
        private string _message;
        private int _errorCode;

        public ExceptionHandler(string message, int errorCode)
        {
            _message = message;
            _errorCode = errorCode;
        }

        public static ExceptionHandler EmailException()
        {
            return new ExceptionHandler("Email must be unique", 500);
        }

        public static ExceptionHandler FindCustomerException()
        {
            return new ExceptionHandler("The customer does not exist", 500);
        }

        public static ExceptionHandler UpdateCustomerException()
        {
            return new ExceptionHandler("This email is already existed. Cannot update this data", 500);
        }

        public static ExceptionHandler FileException(string? message)
        {
            return new ExceptionHandler(message ?? "There is error happened when processing the file", 500);
        }

        public static ExceptionHandler FetchDataException(string? message)
        {
            return new ExceptionHandler(message ?? "Cannot read data from the file", 500);
        }

        public static ExceptionHandler UpdateDataException(string? message)
        {
            return new ExceptionHandler(message ?? "Cannot update data in the file", 500);
        }
    }
}