using AR_NavigationAPI.Controllers;
using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest.ControllerTest
{
    public class MapsControllerTests
    {
       /* private readonly Mock<IMapRepository> _mockMapRepository;
        private readonly MapsController _controller;

        public MapsControllerTests()
        {
            _mockMapRepository = new Mock<IMapRepository>();
            _controller = new MapsController(_mockMapRepository.Object);
        }

        [Fact]
        public void GetAllMaps_ReturnsOkObjectResult()
        {
            // Arrange
            _mockMapRepository.Setup(repo => repo.GetAllMaps()).Returns(new List<MapDTO>());

            // Act
            var result = _controller.GetAllMaps();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetMapById_WithValidId_ReturnsOkObjectResult()
        {
            // Arrange
            int validId = 1;
            _mockMapRepository.Setup(repo => repo.GetMapById(validId)).Returns(new MapDTO());

            // Act
            var result = _controller.GetMapById(validId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        

        [Fact]
        public void AddMap_WithValidMap_ReturnsOkObjectResult()
        {
            // Arrange
            var validMap = new MapAddDTO();
            _mockMapRepository.Setup(repo => repo.AddMap(validMap));

            // Act
            var result = _controller.AddMap(validMap);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        

        [Fact]
        public void UpdateMapById_WithValidId_AndValidMap_ReturnsOkResult()
        {
            // Arrange
            int validId = 1;
            var validMap = new MapUpdateDTO();
            _mockMapRepository.Setup(repo => repo.GetMapById(validId)).Returns(new MapDTO());
            _mockMapRepository.Setup(repo => repo.UpdateMap(validMap));

            // Act
            var result = _controller.UpdateMapById(validId, validMap);

            // Assert
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public void DeleteMapById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            int validId = 1;
            _mockMapRepository.Setup(repo => repo.GetMapById(validId)).Returns(new MapDTO());

            // Act
            var result = _controller.DeleteMapById(validId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        */
    }
}
