using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ProjectTest.DAOTest

{
    public class MapDAOTests
    {/*
        private readonly Mock<finsContext> _mockContext;
        private readonly MapDAO _mapDAO;

        public MapDAOTests()
        {
            _mockContext = new Mock<finsContext>();
            _mapDAO = new MapDAO(_mockContext.Object);
        }

        [Fact]
        public void AddMap_WhenMapIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Map map = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _mapDAO.AddMap(map));
        }

        [Fact]
        public void AddMap_WhenMapNameIsNullOrEmpty_ThrowsArgumentException()
        {
            // Arrange
            Map map = new Map { MapName = "" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapDAO.AddMap(map));
        }

        [Fact]
        public void AddMap_WhenImage2DIsNull_ThrowsArgumentException()
        {
            // Arrange
            Map map = new Map { MapName = "Test Map", Image2D = null };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapDAO.AddMap(map));
        }

        [Fact]
        public void AddMap_WhenFloorIdIsLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            Map map = new Map { MapName = "Test Map", Image2D = "aaa", FloorId = 0 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapDAO.AddMap(map));
        }

        [Fact]
        public void GetMapById_WhenMapIdIsLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            int mapId = -1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapDAO.GetMapById(mapId));
        }
        [Fact]
        public void UpdateMap_WhenMapIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Map map = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _mapDAO.UpdateMap(map));
        }

        [Fact]
        public void UpdateMap_WhenMapIdIsLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            Map map = new Map { MapId = -1 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapDAO.UpdateMap(map));
        }

        [Fact]
        public void UpdateMap_WhenMapNameIsNullOrEmpty_ThrowsArgumentException()
        {
            // Arrange
            Map map = new Map { MapId = 1, MapName = "" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapDAO.UpdateMap(map));
        }

        [Fact]
        public void UpdateMap_WhenImage2DIsNull_ThrowsArgumentException()
        {
            // Arrange
            Map map = new Map { MapId = 1, MapName = "Test Map", Image2D = null };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapDAO.UpdateMap(map));
        }

        [Fact]
        public void UpdateMap_WhenFloorIdIsLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            Map map = new Map { MapId = 1, MapName = "Test Map", Image2D = "aaa", FloorId = 0 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapDAO.UpdateMap(map));
        }

        [Fact]
        public void DeleteMap_WhenMapIdIsLessThanOrEqualToZero_ThrowsArgumentException()
        {
            // Arrange
            int mapId = -1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mapDAO.DeleteMap(mapId));
        }
        [Fact]
        public void DeleteMap_WhenMapIdDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            int mapId = 1;
            var maps = new List<Map>();
            var mockSet = new Mock<DbSet<Map>>();
            mockSet.As<IQueryable<Map>>().Setup(m => m.Provider).Returns(maps.AsQueryable().Provider);
            mockSet.As<IQueryable<Map>>().Setup(m => m.Expression).Returns(maps.AsQueryable().Expression);
            mockSet.As<IQueryable<Map>>().Setup(m => m.ElementType).Returns(maps.AsQueryable().ElementType);
            mockSet.As<IQueryable<Map>>().Setup(m => m.GetEnumerator()).Returns(() => maps.GetEnumerator());

            var mockContext = new Mock<finsContext>();
            mockContext.Setup(c => c.Maps).Returns(mockSet.Object);

            var mapDAO = new MapDAO(mockContext.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => mapDAO.DeleteMap(mapId));
        }


        [Fact]
        public void DeleteMap_WhenMapIdIsValid_RemovesMapFromDatabase()
        {
            // Arrange
            int mapId = 1;
            var mapToDelete = new Map { MapId = mapId };
            var maps = new List<Map> { mapToDelete };
            var mockSet = new Mock<DbSet<Map>>();
            mockSet.As<IQueryable<Map>>().Setup(m => m.Provider).Returns(maps.AsQueryable().Provider);
            mockSet.As<IQueryable<Map>>().Setup(m => m.Expression).Returns(maps.AsQueryable().Expression);
            mockSet.As<IQueryable<Map>>().Setup(m => m.ElementType).Returns(maps.AsQueryable().ElementType);
            mockSet.As<IQueryable<Map>>().Setup(m => m.GetEnumerator()).Returns(() => maps.GetEnumerator());

            var mockContext = new Mock<finsContext>();
            mockContext.Setup(c => c.Maps).Returns(mockSet.Object);

            var mapDAO = new MapDAO(mockContext.Object);

            // Act
            mapDAO.DeleteMap(mapId);

            // Assert
            mockSet.Verify(m => m.Remove(mapToDelete), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }


        [Fact]
        public void GetAllMaps_ReturnsListOfMaps()
        {
            // Arrange
            var maps = new List<Map>
    {
        new Map { MapId = 1, MapName = "Map 1" },
        new Map { MapId = 2, MapName = "Map 2" },
        new Map { MapId = 3, MapName = "Map 3" }
    };
            var mockSet = new Mock<DbSet<Map>>();
            mockSet.As<IQueryable<Map>>().Setup(m => m.Provider).Returns(maps.AsQueryable().Provider);
            mockSet.As<IQueryable<Map>>().Setup(m => m.Expression).Returns(maps.AsQueryable().Expression);
            mockSet.As<IQueryable<Map>>().Setup(m => m.ElementType).Returns(maps.AsQueryable().ElementType);
            mockSet.As<IQueryable<Map>>().Setup(m => m.GetEnumerator()).Returns(() => maps.GetEnumerator());

            _mockContext.Setup(c => c.Maps).Returns(mockSet.Object);

            // Act
            var result = _mapDAO.GetAllMaps();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Map 1", result[0].MapName);
            Assert.Equal("Map 2", result[1].MapName);
            Assert.Equal("Map 3", result[2].MapName);
        }


        [Fact]
        public void SearchMapsByName_WithValidKeyword_ReturnsMatchingMaps()
        {
            // Arrange
            var maps = new List<Map>
    {
        new Map { MapId = 1, MapName = "Map A" },
        new Map { MapId = 2, MapName = "Map B" },
        new Map { MapId = 3, MapName = "Map C" },
        new Map { MapId = 4, MapName = "Another Map" }
    };
            var mockSet = new Mock<DbSet<Map>>();
            mockSet.As<IQueryable<Map>>().Setup(m => m.Provider).Returns(maps.AsQueryable().Provider);
            mockSet.As<IQueryable<Map>>().Setup(m => m.Expression).Returns(maps.AsQueryable().Expression);
            mockSet.As<IQueryable<Map>>().Setup(m => m.ElementType).Returns(maps.AsQueryable().ElementType);
            mockSet.As<IQueryable<Map>>().Setup(m => m.GetEnumerator()).Returns(() => maps.GetEnumerator());

            var mockContext = new Mock<finsContext>();
            mockContext.Setup(c => c.Maps).Returns(mockSet.Object);

            var mapDAO = new MapDAO(mockContext.Object);

            // Act
            var result = mapDAO.SearchMapsByName("Map");

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Contains(result, m => m.MapName == "Map A");
            Assert.Contains(result, m => m.MapName == "Map B");
            Assert.Contains(result, m => m.MapName == "Map C");
        }




        [Fact]
        public void SearchMapsByName_WithNullKeyword_ReturnsEmptyList()
        {
            // Act
            var result = _mapDAO.SearchMapsByName(null);

            // Assert
            Assert.Empty(result);
        }
        */
    }
}