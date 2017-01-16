using System;
using System.Linq;

namespace PasswordValidation
{
    public class PasswordValidator
    {
        public bool Verify(string password, string UserType)
        {
            if (UserType == "Internal")
            {
                InternalPasswordValidator obj = new InternalPasswordValidator();
                bool result = obj.Verify(password);
                return result;
            }
            else if (UserType == "External")
            {
                ExternalPasswordValidator obj = new ExternalPasswordValidator();
                bool result = obj.Verify(password);
                return result;
            }
            else
                throw new InvalidPasswdException("Not a valid user type");

        }
    }
    public class InternalPasswordValidator:PasswordValidator
    {
        public bool Verify(string password)
        {
            if (password.Length <= 8)
            {
                    throw new InvalidPasswdException("Password should be atleast 8 in length ");
            }
            else
            {
                    return true;
            }
        }

    }
    public class ExternalPasswordValidator:PasswordValidator
    {
        int count=0;
        public bool Verify(string password)
        {
            if (!CheckNull(password) || !CheckUppercase(password))
            {
                return false;
            }
            else if (!CheckLowercase(password)&&!CheckLength(password) && !CheckDigit(password))
            {
                return false;
            }
            else
                return true;
        }
        public bool CheckNull(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new InvalidPasswdException("Password cannot be empty");
            else
            {
                count++;
                return true;
            }

        }
        public bool CheckUppercase(string password)
        {
            if (password.Where(char.IsUpper).Count() < 1)
            {
                throw new InvalidPasswdException("Password should have atleast one Uppercase");

            }
            else
            {
                return true;
            }
        }
        public bool CheckLength(string password)
        {
            if (password.Length <= 8)
            {
                throw new InvalidPasswdException("Password should be atleast 8 in length ");
            }
            else
            {
                count++;
                return true;
            }
        }
        public bool CheckLowercase(string password)
        {
            if (password.Where(char.IsLower).Count() < 1)
            {
                throw new InvalidPasswdException("Password should be atleast 1 lowercase ");
            }
            else
            {
                count++;
                return true;
            }
        }
        public bool CheckDigit(string password)
        {
            if (password.Where(char.IsDigit).Count() < 1)
            {
                throw new InvalidPasswdException("Password should be atleast 1 digit ");
            }
            else
            {
                count++;
                return true;
            }
        }
    }

    public class InvalidPasswdException : Exception
    {
        public InvalidPasswdException(string message):base(message)
        {

        }
    }
}
