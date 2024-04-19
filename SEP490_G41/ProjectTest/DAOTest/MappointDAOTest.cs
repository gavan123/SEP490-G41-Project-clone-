using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using Moq;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace ProjectTest.DAOTest
{
    public class MappointDAOTests
    {
<<<<<<< HEAD
        /*private readonly Mock<finsContext> _mockContext;
=======
       /* private readonly Mock<finsContext> _mockContext;
>>>>>>> main
        private readonly MappointDAO _mappointDAO;

        public MappointDAOTests()
        {
            _mockContext = new Mock<finsContext>();
            _mappointDAO = new MappointDAO(_mockContext.Object);
        }

        [Fact]
        public void AddMappoint_WhenMappointIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Mappoint mappoint = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _mappointDAO.AddMappoint(mappoint));
        }

        [Fact]
        public void AddMappoint_WhenMapIdIsNotPositive_ThrowsArgumentException()
        {
            // Arrange
            var mappoint = new Mappoint { MapPointId = 1, MapId = -1, MappointName = "Point 1", LocationWeb = new Point(0, 0), LocationApp = new Point(1, 1), LocationGps = new Point(2, 2), FloorId = 1, BuildingId = 101, Image = "point1.jpg" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mappointDAO.AddMappoint(mappoint));
        }

        [Fact]
        public void AddMappoint_WhenLocationIsNull_ThrowsArgumentException()
        {
            // Arrange
            var mappoint = new Mappoint { MapPointId = 1, MapId = 1, MappointName = "Point 1", LocationWeb = null, LocationApp = null, LocationGps = null, FloorId = 1, BuildingId = 101, Image = "point1.jpg" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mappointDAO.AddMappoint(mappoint));
        }

        [Fact]
        public void GetMappointById_WhenMappointIdIsNotPositive_ThrowsArgumentException()
        {
            // Arrange
            int mappointId = 0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mappointDAO.GetMappointById(mappointId));
        }

        [Fact]
        public void UpdateMappoint_WhenMappointIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Mappoint mappoint = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _mappointDAO.UpdateMappoint(mappoint));
        }

        [Fact]
        public void UpdateMappoint_WhenMapPointIdIsNotPositive_ThrowsArgumentException()
        {
            // Arrange
            var mappoint = new Mappoint { MapPointId = -1, MapId = 1, MappointName = "Point 1", LocationWeb = new Point(0, 0), LocationApp = new Point(1, 1), LocationGps = new Point(2, 2), FloorId = 1, BuildingId = 101, Image = "point1.jpg" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mappointDAO.UpdateMappoint(mappoint));
        }

        [Fact]
        public void UpdateMappoint_WhenMapIdIsNotPositive_ThrowsArgumentException()
        {
            // Arrange
            var mappoint = new Mappoint { MapPointId = 1, MapId = -1, MappointName = "Point 1", LocationWeb = new Point(0, 0), LocationApp = new Point(1, 1), LocationGps = new Point(2, 2), FloorId = 1, BuildingId = 101, Image = "point1.jpg" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mappointDAO.UpdateMappoint(mappoint));
        }

        [Fact]
        public void UpdateMappoint_WhenLocationIsNull_ThrowsArgumentException()
        {
            // Arrange
            var mappoint = new Mappoint { MapPointId = 1, MapId = 1, MappointName = "Point 1", LocationWeb = null, LocationApp = null, LocationGps = null, FloorId = 1, BuildingId = 101, Image = "point1.jpg" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mappointDAO.UpdateMappoint(mappoint));
        }

        [Fact]
        public void DeleteMappoint_WhenMappointIdIsNotPositive_ThrowsArgumentException()
        {
            // Arrange
            int mappointId = 0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mappointDAO.DeleteMappoint(mappointId));
        }

        [Fact]
        public void DeleteMappoint_WhenMappointDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            int mappointId = 1;
            var mockSet = new Mock<DbSet<Mappoint>>();
            _mockContext.Setup(c => c.Mappoints).Returns(mockSet.Object);

            // Setup LINQ query behavior to return null when FirstOrDefault is called with an expression matching the specified ID
            mockSet.As<IQueryable<Mappoint>>().Setup(m => m.Provider).Returns(Mock.Of<IQueryProvider>());
            mockSet.As<IQueryable<Mappoint>>().Setup(m => m.Expression).Returns(Expression.Constant(new List<Mappoint>().AsQueryable().Expression));
            mockSet.As<IQueryable<Mappoint>>().Setup(m => m.ElementType).Returns(new List<Mappoint>().AsQueryable().ElementType);
            mockSet.As<IQueryable<Mappoint>>().Setup(m => m.GetEnumerator()).Returns(new List<Mappoint>().GetEnumerator());


            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mappointDAO.DeleteMappoint(mappointId));
        }



        [Fact]
        public void GetAllMappoints_ReturnsListOfMappoints()
        {
            // Arrange
            var mappoints = new List<Mappoint>
                {
                    new Mappoint { MapPointId = 1, MapId = 1, MappointName = "Point 1", LocationWeb = new Point(0, 0), LocationApp = new Point(1, 1), LocationGps = new Point(2, 2), FloorId = 1, BuildingId = 101, Image = "point1.jpg" },
                    new Mappoint { MapPointId = 2, MapId = 1, MappointName = "Point 2", LocationWeb = new Point(3, 3), LocationApp = new Point(4, 4), LocationGps = new Point(5, 5), FloorId = 2, BuildingId = 102, Image = "point2.jpg" },
                    new Mappoint { MapPointId = 3, MapId = 2, MappointName = "Point 3", LocationWeb = new Point(6, 6), LocationApp = new Point(7, 7), LocationGps = new Point(8, 8), FloorId = 3, BuildingId = 103, Image = "point3.jpg" }
                };
            var mockSet = new Mock<DbSet<Mappoint>>();
            mockSet.As<IQueryable<Mappoint>>().Setup(m => m.Provider).Returns(mappoints.AsQueryable().Provider);
            mockSet.As<IQueryable<Mappoint>>().Setup(m => m.Expression).Returns(mappoints.AsQueryable().Expression);
            mockSet.As<IQueryable<Mappoint>>().Setup(m => m.ElementType).Returns(mappoints.AsQueryable().ElementType);
            mockSet.As<IQueryable<Mappoint>>().Setup(m => m.GetEnumerator()).Returns(mappoints.GetEnumerator());
            _mockContext.Setup(c => c.Mappoints).Returns(mockSet.Object);

            // Act
            var result = _mappointDAO.GetAllMappoints();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(1, result[0].MapId);
            Assert.Equal(1, result[1].MapId);
            Assert.Equal(2, result[2].MapId);
        }*/
    }
}
