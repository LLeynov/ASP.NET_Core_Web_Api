using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.NET_Core_Web_Api.Controllers;
using ASP.NET_Core_Web_Api.Models.Requests;
using ASP.NET_Core_Web_Api.Services;
using ASP.NET_Core_Web_Api.Services.Impl;
using EmployeeService.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ASP.NET_Core_Web_Api_Tests
{
    public class DictionariesControllerTests
    {
        private readonly DictionariesController _dictionariesController;
        private readonly Mock<IEmployeeTypeRepository> _mockEmployeeTypeRepository;

        public DictionariesControllerTests()
        {
            _mockEmployeeTypeRepository = new Mock<IEmployeeTypeRepository>();
            _dictionariesController = new DictionariesController(_mockEmployeeTypeRepository.Object);
        }

        [Fact]
        public void GetAllEmployeeTypesTest()
        {
            _mockEmployeeTypeRepository.Setup(repository => repository
              .GetAll()).Returns(new List<EmployeeType>());

          var result = _dictionariesController.GetAllEmployeeTypes();

          _mockEmployeeTypeRepository.Verify(repository => repository.GetAll(), Times.Once());
        }

        
        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        public void CreateEmployeeTypeTest(string description)
        {
            _mockEmployeeTypeRepository.Setup(repository =>
                repository.Create(It.IsAny<EmployeeType>())).Verifiable();

            var result = _dictionariesController.CreateEmployeeType(description);

            _mockEmployeeTypeRepository.Verify(repository => repository
                .Create(It.IsAny<EmployeeType>()),Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DeleteEmployeeTypeTest(int id)
        {
            _mockEmployeeTypeRepository.Setup(repository =>
                repository.Delete(It.IsAny<int>())).Verifiable();


            var result = _dictionariesController.DeleteEmployeeType(id);

            _mockEmployeeTypeRepository.Verify(repository => repository
                .Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
