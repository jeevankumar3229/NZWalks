using BLLayer.Repository;
using Castle.Core.Logging;
using DALayer;
using DevicesAPI.Controllers;
using DeviceTest.MockDatas;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Models.Requests;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeviceTest.Testing.Controller
{
    public class TestAuthControllerClass
    {
       



        
        [Fact]
        public async Task RegisterUser_ReturnsBadRequestObject_CreationFailed()
        {
            var register = new Register
            {
                Username="jeevankumar@gmail.com",
                Password="Jeevan@30"
            };
            var user = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Username
            };
           
            var repository = new Mock<ITokenRepository>();
            var logger = new Mock<ILogger<AuthController>>();
            var usermanager = new Mock<UserManager<IdentityUser>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(),It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Failed()));
            var controller = new AuthController(userManagerMock.Object,repository.Object, logger.Object);

            var result = controller.RegisterUser(register);

            result.Result.GetType().Should().Be(typeof(BadRequestObjectResult));

            
        }

        [Fact]
        public async Task RegisterUser_ReturnsBadRequestObject_AddRolesFailed()
        {
            var register = new Register
            {
                Username = "jeevankumar@gmail.com",
                Password = "Jeevan@30",
                roles = new string[] {"Read","Write"}
            };
            var user = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Username
            };

            var repository = new Mock<ITokenRepository>();
            var logger = new Mock<ILogger<AuthController>>();
            var usermanager = new Mock<UserManager<IdentityUser>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            userManagerMock.Setup(x => x.AddToRolesAsync(It.IsAny<IdentityUser>(), It.IsAny<String[]>())).Callback(() => throw new Exception());
            var controller = new AuthController(userManagerMock.Object, repository.Object, logger.Object);

            var result = controller.RegisterUser(register);
            

            result.Result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task RegisterUser_ReturnsOkObjectResult()
        {
            var register = new Register
            {
                Username = "jeevankumar@gmail.com",
                Password = "Jeevan@30",
                roles = new string[] { "Read", "Write" }
            };
            var user = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Username
            };

            var repository = new Mock<ITokenRepository>();
            var logger = new Mock<ILogger<AuthController>>();
            var usermanager = new Mock<UserManager<IdentityUser>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            userManagerMock.Setup(x => x.AddToRolesAsync(It.IsAny<IdentityUser>(), It.IsAny<String[]>())).Returns(Task.FromResult(IdentityResult.Success));
            var controller = new AuthController(userManagerMock.Object, repository.Object, logger.Object);

            var result = controller.RegisterUser(register);


            result.Result.GetType().Should().Be(typeof(OkObjectResult));
        }



        [Fact]
        public async Task LoginUser_ReturnsBadRequestObject_InValidUser()
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
            IdentityUser users = null;
            var repository = new Mock<ITokenRepository>();
            var logger = new Mock<ILogger<AuthController>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByEmailAsync(user.UserName)).Returns(Task.FromResult(users));
            var controller = new AuthController(userManagerMock.Object, repository.Object, logger.Object);

            
            var result = controller.LoginUser(login);
            result.Result.GetType().Should().Be(typeof(BadRequestObjectResult));



        }


        [Fact]
        public async Task LoginUser_ReturnsBadRequestObject_InValidPassword()
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
            var b = false;
            var repository = new Mock<ITokenRepository>();
            var logger = new Mock<ILogger<AuthController>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByEmailAsync(user.UserName)).Returns(Task.FromResult(user));
            userManagerMock.Setup(x => x.CheckPasswordAsync(user,login.Password)).Returns(Task.FromResult(b));
            var controller = new AuthController(userManagerMock.Object, repository.Object, logger.Object);


            var result = controller.LoginUser(login);
            result.Result.GetType().Should().Be(typeof(BadRequestObjectResult));



        }
        
        [Fact]
        public async Task LoginUser_ReturnsOkObjectResult()
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
            var b = true;
            List<string> b1 = new List<string> {"Read" };
            var token = "abc";
            var repository = new Mock<ITokenRepository>();
            var logger = new Mock<ILogger<AuthController>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByEmailAsync(user.UserName)).Returns(Task.FromResult(user));
            userManagerMock.Setup(x => x.CheckPasswordAsync(user, login.Password)).Returns(Task.FromResult(b));
            userManagerMock.Setup(x => x.GetRolesAsync(user)).Returns(Task.FromResult((IList<string>)b1));
            repository.Setup(x => x.CreateToken(It.IsAny<IdentityUser>(),It.IsAny<List<String>>() )).Returns(token);
            var controller = new AuthController(userManagerMock.Object, repository.Object, logger.Object);


            var result = controller.LoginUser(login);
            result.Result.GetType().Should().Be(typeof(OkObjectResult));



        }
    }
}
