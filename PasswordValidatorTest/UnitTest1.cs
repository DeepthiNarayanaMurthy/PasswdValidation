using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordValidation;

namespace PasswordValidatorTest
{
    [TestClass]
    public class UnitTest1
    {
        private bool ConstructAndCall(string password,string UserType)
        {
            PasswordValidator Password = new PasswordValidator();
            bool result = Password.Verify(password,UserType);
            return result;
        }
        [TestMethod]
        public void GivingNullStringAsPassword_VerifyingIt_ShouldThrowException()
        {
            ExternalPasswordValidator Password = new ExternalPasswordValidator();
            try
            {
                bool expected = Password.CheckNull("");
            }
            catch(InvalidPasswdException ex)
            {
                Assert.AreEqual("Password cannot be empty", ex.Message, "Null password exception raised");
            }
        }

        [TestMethod]
        public void GivingNoUppercaseCharacter_VerifyingIt_ThrowsException()
        {
            ExternalPasswordValidator Password = new ExternalPasswordValidator();
            try
            {
                bool expected = Password.CheckUppercase("sadadecd1");
            }
            catch (InvalidPasswdException ex)
            {
                Assert.AreEqual("Password should have atleast one Uppercase", ex.Message, "No Uppercase exception raised");
            }
        }
        [TestMethod, ExpectedException(typeof(InvalidPasswdException))]
        public void GivingPasswordOfLengthLessThanEight_VerifyIt_ThrowsException()
        {
            ExternalPasswordValidator Password = new ExternalPasswordValidator();
            bool expected = Password.CheckLength("KIJU");           
        }

        [TestMethod,ExpectedException(typeof(InvalidPasswdException))]
        public void GivingNoLowercaseCharacter_VerifyingIt_ThrowsException()
        {
            ExternalPasswordValidator Password = new ExternalPasswordValidator();
            bool expected = Password.CheckLowercase("ASD");          
        }
       


        [TestMethod, ExpectedException(typeof(InvalidPasswdException))]
        public void GivingNoNumberInPassword_VerifyingIt_ThrowsException()
        {
            ExternalPasswordValidator Password = new ExternalPasswordValidator();
            bool expected = Password.CheckDigit("ASD");       
        }
        
        [TestMethod, ExpectedException(typeof(InvalidPasswdException))]
        public void GivingPasswordWithLessThanThreeConditions_ForExternal_VerifyIt_ThrowsException()
        {
            bool expected = ConstructAndCall("WESDASXD", "External");
        }

        [TestMethod]
        public void GivingPasswordWithGreaterThanThreeCondition_ForExternal_VerifyIt_ThrowsException()
        {
            bool expected = ConstructAndCall("aaAdadaas", "External");
            Assert.AreEqual(true, expected, "Correct password");

        }
        [TestMethod]
        public void GivingPasswordWithAllCondition_ForExternal_VerifyIt_ReturnsTrue()
        { 
            bool expected = ConstructAndCall("aWadaascd12","External");
            Assert.AreEqual(true, expected, "Correct password");
        }

        [TestMethod]
        public void GivingPasswordWithMoreThanEightCharacter_ForInternal__VerifyIt_ReturnsTrue()
        {
            bool expected = ConstructAndCall("aWadaascd", "Internal");
            Assert.AreEqual(true, expected, "Correct password");
        }

        [TestMethod]
        public void GivingPasswordWithLessThanEightCharacter_ForInternal__VerifyIt_ThrowsException()
        {
            try
            {
                bool expected = ConstructAndCall("aWadaascd", "Internal");
            }
            catch (InvalidPasswdException ex)
            {
                Assert.AreEqual("Password should be atleast 8 in length", ex.Message, "Invalid exception raised");
            }
            
        }


    }
}
