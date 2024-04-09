using BusinessObject.DTO;
using DataAccess.DAO;
using System.Collections.Generic;

namespace DataAccess.IRepository
{
    public interface IMapPointRepository
    {
        MapPointDTO GetMapPointById(int mapPointId);
        List<MapPointDTO> GetAllMapPoints();
        void AddMapPoint(MapPointAddDTO mapPoint);
        void UpdateMapPoint(MapPointUpdateDTO mapPoint);
        void DeleteMapPoint(int mapPointId);

        // Các phương thức tìm kiếm và các phương thức khác có thể được thêm ở đây tùy thuộc vào yêu cầu cụ thể.
    }
}
