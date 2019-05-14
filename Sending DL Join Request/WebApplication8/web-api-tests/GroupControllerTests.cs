using Contracts;
using Entities;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApplication8;
using WebApplication8.Controllers;

using Xunit;

namespace web_api_tests
{
    /// <summary>Class containing all the test cases for Group controller.</summary>
    public class GroupControllerTests: Controller                   //class containing testcases for all methods in Group Controller
    {
        ILoggerManager logger;
        IRepositoryWrapper repository;

        /// <summary>Gets the groups returns correct response.</summary>
        /// <returns>Generic task</returns>
        [Fact]
        public async Task GetGroups_ReturnsCorrectResponse()            //testing the response.
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "GetGroups_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);           //mocking the group controller ctor.
            var result = controller.GetAllGroups();
            Assert.IsAssignableFrom<IActionResult>(result);                     //asserting the correct response returned by the http get request.
        }

        /// <summary>Gets the groups by identifier returns correct response.</summary>
        /// <returns>Generic task</returns>
        [Fact]
        public async Task GetGroupsById_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "GetGroupsById_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            seed(_context);                                                      //seeding the fake data to our in-memory db.
            int id = 1;
            //Guid id = new Guid("03e91478-5608-4132-a753-d494dafce00b");
            var result = controller.GetGroupById(id);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task GetGroupsByIdIfNull_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "GetGroupsByIdIfNull_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            seed(_context);
            int id = 123;
            //Guid id = new Guid("eccadf79-85fe-402f-893c-32d3f03ed9b1");
            var result = controller.GetGroupById(id);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task GetGroupsByName_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "GetGroupsByName_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            seed(_context);
            string name= "Test";
            var result = controller.GetGroupByName(name);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task GetGroupsByNameIfNull_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "GetGroupsByNameIfNull_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            seed(_context);
            string name = "TestGroup";
            var result = controller.GetGroupByName(name);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task GetAllAssocGroups_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "GetGroups_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            string AssocID = "YS067342";
            var result = controller.GetAllAssocGroups(AssocID);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task CreateGroup_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "CreateGroup_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            seed(_context);
            var result = controller.CreateGroup(new Group { id = 1234, Name = "Test", Description = "Test", Admin = "Test" });
            Assert.IsAssignableFrom<IActionResult>(result);
            
        }

        [Fact]
        public async Task CreateGroupIfNull_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "CreateGroupIfNull_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            seed(_context);
            var result = controller.CreateGroup(new Group { });
            var result1 = controller.CreateGroup(null);
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsAssignableFrom<IActionResult>(result1);
        }

        [Fact]
        public async Task CreateGroupIfModelInvalid_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "CreateGroupIfModelInvalid_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            seed(_context);
            var grp = new Group();
            var context = new ValidationContext(grp, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(grp, context, results, true);
            controller.ModelState.AddModelError("test","test");
            var result = controller.CreateGroup(grp);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task UpdateGroup_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "UpdateGroup_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            int id = 1;
            //Guid id = new Guid("03e91478-5608-4132-a753-d494dafce00b");
            seed(_context);
            var result = controller.UpdateGroup(id, new Group { id = 123, Name = "Test", Description = "Test", Admin = "Test" });
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task UpdateGroupIfModelInvalid_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "UpdateGroupIfModelInvalid_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            int id = 1;                                                         //passing group present in database
            //Guid id = new Guid("03e91478-5608-4132-a753-d494dafce00b");
            seed(_context);
            var grp = new Group();
            var context = new ValidationContext(grp, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(grp, context, results, true);   
            controller.ModelState.AddModelError("test", "test");
            var result = controller.UpdateGroup(id, grp);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task UpdateGroupIfModelInvalidNull_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "UpdateGroupIfModelInvalidNull_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            int id = 123; 
            //Guid id = new Guid("eccadf79-85fe-402f-893c-32d3f03ed9b1");
            seed(_context);
            var result = controller.UpdateGroup(id, new Group { });           //for exception raising
            var result1 = controller.UpdateGroup(id, null);             //passing null group object.
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsAssignableFrom<IActionResult>(result1);
        }

        [Fact]
        public async Task DeleteGroup_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "DeleteGroup_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            int id = 1;                                                              //passing group present in database
            //Guid id = new Guid("03e91478-5608-4132-a753-d494dafce00b");
            seed(_context);
            var result = controller.DeleteGroup(id);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task DeleteGroupIfNull_ReturnsCorrectResponse()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "DeleteGroupIfNull_ReturnsCorrectResponse").Options;
            var _context = new RepositoryContext(options);
            logger = new LoggerManager();
            repository = new RepositoryWrapper(_context);
            var controller = new GroupController(logger, repository);
            //Guid id = new Guid("eccadf79-85fe-402f-893c-32d3f03ed9b1");
            int id = 123;                                               //passing group not present in database
            seed(_context);
            var result = controller.DeleteGroup(id);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public async Task ModelValid_ReturnsCorrectResponse()
        {
            var grp = new Group {id = 5, Name = "Test", Description = "Test", Admin = "Test"};
            //var grp = new Group { id = new Guid("a3fbad0b-7f48-4feb-8ac0-6d3bbc997bfc"), Name = "Test", Description = "Test", Admin = "Test" };
            var errorcount = myValidation(grp).Count();
            Assert.Equal(0, errorcount);
        }

        public IList<ValidationResult> myValidation(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);
            return result;
        }

        private void seed(RepositoryContext context)                        //seeding data to our in memory database
        {
            var groups = new[]
            {
                new Group{ id =1, Name = "Test", Description = "Test", Admin = "Test" },
                new Group{ id =2, Name = "Test", Description = "Test", Admin = "Test" },
                new Group{ id = 3, Name = "Test", Description = "Test", Admin = "Test" },
                new Group{ id = 4, Name = "Test", Description = "Test", Admin = "Test" },
                new Group{ id = 5, Name = "Test", Description = "Test", Admin = "Test" }

            };
            context.Groups.AddRange(groups);
            context.SaveChangesAsync();
        }
    }
}

