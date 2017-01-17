using System;
using System.Linq;

namespace PasswordValidation
{
    public class PasswordValidator
    {
        public bool Verify(string password, string UserType)
        {
            if (UserType.Equals( "Internal"))
            {
                InternalPasswordValidator obj = new InternalPasswordValidator();
                bool result = obj.Verify(password);
                return result;
            }
            else if (UserType.Equals("External"))
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
        int PassCount=0;
        public bool Verify(string password)
        {
            bool res;
            try
            {
                res =(CheckNull(password) && CheckUppercase(password) && CheckLowercase(password) && CheckLength(password) && CheckDigit(password));
            }
            catch
            {
                if (PassCount >= 2)
                {
                    PassCount = PassCount - 2;
                }
                try
                {
                    res = (CheckUppercase(password) || CheckNull(password));
                }
                catch
                {
                    return false;
                }
                if (PassCount > 2)
                {
                    return true;
                }
                return false;
            }
            if (PassCount > 2)
            {
                return true;
            }
           
            
            return false;
        }
        public bool CheckNull(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new InvalidPasswdException("Password cannot be empty");
            else
            {
                PassCount++;
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
                PassCount++;
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
                PassCount++;
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
                PassCount++;
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
                PassCount++;
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
