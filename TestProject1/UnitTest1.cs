using identity;
using identity.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async Task LoginUser_ReturnsBadRequestObject()
        {
            var login = new LoginRequest
            {
                Username = "jeevankumar@gmail.com",
                Password = "Jeevan@30"
            };
            var user = new IdentityUser
            {
                UserName = login.Username,
                Email = login.Username
            };
            var users = new IdentityUser();
            var repository = new Mock<ITokenRepository>();
            var logger = new Mock<ILogger<AuthController>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByEmailAsync(user.UserName)).Returns(Task.FromResult(new Exception()));
            var controller = new AuthController(userManagerMock.Object,repository.Object, logger.Object);

            var result = controller.LoginUser(login);

            result.GetType().s



            





        }
    }
}