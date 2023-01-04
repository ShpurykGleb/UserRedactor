using CSharpCourseWork;
using CSharpCourseWork.Controllers;

namespace TestRedactor
{
    public class TestRedactor
    {
        [Fact]
        public void TestIsOldPasswordCorrect()
        {
            Admin admin = new Admin("Admin", CryptController.SHA256Crypt("Admin111"));
            Assert.True(ChangePasswordController.IsOldPasswordCorrect("Admin111", admin));
        }

        [Fact]
        public void TestAreLoginsEqual()
        {
            Assert.True(ChangeLoginController.AreLoginsEqual("bebebe", "bebebe"));
        }

        [Fact]
        public void TestIsLoginAlreadyTaken()
        {
            Assert.True(EntireController.IsLoginAlreadyTaken("Admin"));
        }

        [Fact]
        public void TestCheckPassword()
        {
            string result = LoginAndPasswordController.CheckPassword(null);
            Assert.Equal("Password is null!\nEnter the password!", result);
        }
    }
}