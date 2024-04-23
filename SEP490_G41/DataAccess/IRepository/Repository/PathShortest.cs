using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Org.BouncyCastle.Math.EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository.Repository
{
    public class PathShortest
    {
        private readonly EdgeDAO _edgeDAO;
        private readonly MappointDAO _mappointDAO;
        public PathShortest( MappointDAO mappointDAO, EdgeDAO edgeDAO)
        {
            _mappointDAO = mappointDAO;
            _edgeDAO = edgeDAO;
        }
        public Mappoint FindPoint(int input) // get point from database where name 
        {
            List<Mappoint> listOfMapPoint = _mappointDAO.GetAllMappoints();
            foreach (Mappoint find in listOfMapPoint)
            {
                if (find.MapPointId.Equals(input))
                {
                    return find;
                }
            }
            return null;
        }

        public double NumberFomula(double ax, double ay, double bx, double by) //distance
        {
            double result;
            double half1, half2;
            half1 = ax - bx;
            half2 = ay - by;
            result = half1 * half1 + half2 * half2;
            return result;
        }

        public List<Mappoint> PointInArea(Mappoint pos1, Mappoint pos2,int multi) //get local points important
        {
            
            List<Mappoint> listOfMapPoint = _mappointDAO.GetAllMappoints();
            List<Mappoint> pointInAreaList = new List<Mappoint>();
            double result;
            double rotate = NumberFomula(pos2.LocationApp.X,
                pos2.LocationApp.Y, pos1.LocationApp.X,
                pos1.LocationApp.Y);
            int min = pos1.FloorId;
            int max = pos2.FloorId;
            foreach (Mappoint find in listOfMapPoint)
            {
                result = NumberFomula(find.LocationApp.X,
                    find.LocationApp.Y, pos1.LocationApp.X,
                    pos1.LocationApp.Y);
                if (result <= rotate * multi && find.FloorId>=min && find.FloorId<=max)
                {
                    if (!pointInAreaList.Contains(find))
                    {
                        pointInAreaList.Add(find);
                    }
                }
            }
            return pointInAreaList;
        }

        public List<Mappoint> GetAdjacentMapPoint(int mapPointId, List<Mappoint> pointInAreaList) //djiktra
        {
            List<Mappoint> adjacentMapPoints = new List<Mappoint>();
            List<Edge> listOfEdge = _edgeDAO.GetAllEdges();
            // Duyệt qua danh sách các cạnh (listOfEdge)
            foreach (Edge edge in listOfEdge)
            {
                // Kiểm tra nếu mapPointId hiện tại nằm trong cạnh này
                if (edge.MapPointA == mapPointId || edge.MapPointB == mapPointId)
                {
                    // Lấy MapPointId của điểm lân cận
                    int adjacentMapPointId = edge.MapPointA == mapPointId ? edge.MapPointB : edge.MapPointA;

                    // Tìm đối tượng Mappoint tương ứng trong danh sách pointInAreaList
                    Mappoint adjacentMapPoint = pointInAreaList.Find(mp => mp.MapPointId == adjacentMapPointId);

                    // Nếu tìm thấy, thêm vào danh sách adjacentMapPoints
                    if (adjacentMapPoint != null)
                    {
                        adjacentMapPoints.Add(adjacentMapPoint);
                    }
                }
            }
                                                                            
            return adjacentMapPoints;
        }

        //public List<Edge> GenerateGraphLine(int mapPointId) //to comment
        //{
        //    List<Mappoint> adjacentMapPoints = GetAdjacentMapPoint(mapPointId);
        //    List<Edge> listOfEdge = new List<Edge>();

        //    foreach (Mappoint find in pointInAreaList)
        //    {
        //        int id = find.MapPointId;

        //        if (adjacentMapPoints.Any(mp => mp.MapPointId == id))
        //        {
        //            Edge edge = listOfEdge.FirstOrDefault(e => (e.MapPointA == mapPointId && e.MapPointB == id) || (e.MapPointA == id && e.MapPointB == mapPointId));
        //            listOfEdge.Add(edge);
        //        }
        //        else
        //        {
        //            Console.Write("not have edge");
        //        }
        //    }

        //    return listOfEdge;
        //}

        //public void GenerateGraph() //to comment
        //{
        //    foreach (Mappoint find in pointInAreaList)
        //    {
        //        GenerateGraphLine(find.MapPointId);
        //    }
        //}

        public int getIndex(int mappointId, List<Mappoint> pointInAreaList) //get index of mappoint in local points list
        {

            int index = 0;
            while (pointInAreaList.Count() > index)
            {
                if (mappointId == pointInAreaList[index].MapPointId)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public int FindMinDistance(double[] shortestDistances, bool[] visited) //djiktra 
        {
            double minDistance = double.MaxValue;
            int minMapPoint = -1;
            for (int mapPoint = 0; mapPoint < shortestDistances.Length; mapPoint++)
            {
                if (!visited[mapPoint] && shortestDistances[mapPoint] <= minDistance)
                {
                    minDistance = shortestDistances[mapPoint];
                    minMapPoint = mapPoint;
                }
            }
            return minMapPoint;
        }

        public Edge GetEdge(int p1, int p2)
        {
            Edge edge1 = null;
            List<Edge> listOfEdge = _edgeDAO.GetAllEdges();
            foreach (Edge edge in listOfEdge)
            {
                if ((edge.MapPointA == p1 && edge.MapPointB == p2) || (edge.MapPointA == p2 && edge.MapPointB == p1)) ;
                edge1 = edge;
            }
            return edge1;
        }
        //public List<Mappoint> ShortestPath(int inputPosition, int inputDestination, int multi)
        //{
        //    List<Mappoint> result = new List<Mappoint>();
        //    List<Mappoint> resultHalf = new List<Mappoint>();
        //    Mappoint position = _mappointDAO.GetMappointById(inputPosition);
        //    Mappoint destination = _mappointDAO.GetMappointById(inputDestination);
        //    if (position.FloorId == destination.FloorId) 
        //    {
        //      result = Dijkstra(inputPosition, inputDestination, multi);
        //    }
        //    else
        //    {
        //        int stairAtPosition = NearestStairOrElevator(position) ;
        //        result = Dijkstra(inputPosition, stairAtPosition, multi);
        //        int stairAtDestination = NextStair(stairAtPosition, destination.FloorId);
        //        resultHalf = Dijkstra(stairAtDestination, inputDestination, multi);
        //        foreach(Mappoint m in resultHalf)
        //        {
        //             result.Add(m);
        //        }
        //    }
        //    return result; 
        //}
        //public int NextStair(int currentStairId, int destinationFloorId)
        //{
        //    int nextStair = -1;
        //    Mappoint mappoint = _mappointDAO.GetMappointById(currentStairId);
        //    String name = mappoint.MapPointName.Substring(0, mappoint.MapPointName.Length - 1) + destinationFloorId;
        //    Mappoint mappoint1 = _mappointDAO.SearchByName(name);
        //    nextStair = mappoint1.MapPointId;
        //    return nextStair;
        //}

        //public int NearestStairOrElevator(Mappoint position)
        //{
        //    List<Mappoint> mappoints = _mappointDAO.GetAllMappoints();

        //    var nearestMappoint = mappoints
        //        .Where(m => m.MapPointName.Contains("stair") || m.MapPointName.Contains("elevator")) // Lọc các điểm chứa "stair" hoặc "elevator"
        //        .GroupBy(m => m.Floor) // Nhóm các mappoint theo tầng
        //        .Where(group => group.Any()) // Loại bỏ các tầng không có stair hoặc elevator
        //        .SelectMany(group => group) // Giải nén các nhóm
        //        .OrderBy(m => Math.Sqrt(Math.Pow(m.LocationWeb.X - position.LocationWeb.Y, 2) + Math.Pow(m.LocationWeb.Y - position.LocationWeb.Y, 2))) // Sắp xếp các điểm theo khoảng cách Euclide
        //        .FirstOrDefault(); // Chọn điểm gần nhất

        //    if (nearestMappoint != null)
        //    {
        //        return nearestMappoint.MapPointId;
        //    }
        //    else
        //    {
        //        // Trả về -1 hoặc giá trị mặc định khác nếu không tìm thấy điểm gần nhất
        //        return -1;
        //    }
        //}

        public List<Mappoint> Dijkstra(int inputPosition, int inputDestination,int multi)
        {
            
            List <Mappoint> pointInAreaList = PointInArea(_mappointDAO.GetMappointById(inputPosition),_mappointDAO.GetMappointById(inputDestination),multi);
            int position = getIndex(inputPosition, pointInAreaList);
            int destination = getIndex(inputDestination, pointInAreaList);
            int numMapPoint = pointInAreaList.Count;
            double[] shortestDistances = new double[numMapPoint];
            int[] previousMapPoint = new int[numMapPoint];
            bool[] visited = new bool[numMapPoint];
            int currentMapPoint;
            Array.Fill(shortestDistances, double.MaxValue);
            Array.Fill(previousMapPoint, -1);
            shortestDistances[position] = 0;

            for (int count = 0; count < numMapPoint - 1; count++)
            {
                currentMapPoint = FindMinDistance(shortestDistances, visited);
                visited[currentMapPoint] = true;

                if (currentMapPoint == destination)
                {
                    break;
                }
                int currentMapPointName = pointInAreaList[currentMapPoint].MapPointId;
                List<Mappoint> adjacentMapPoints = GetAdjacentMapPoint(currentMapPointName, pointInAreaList);
                foreach (Mappoint mapPoint in adjacentMapPoints)
                {
                    int mapPointIndex = getIndex(mapPoint.MapPointId, pointInAreaList);
                    double edgeWeight = GetEdge(currentMapPointName, mapPoint.MapPointId).Distance;
                    if (!visited[mapPointIndex] && edgeWeight != -1.0
                            && shortestDistances[currentMapPoint] != double.MaxValue
                            && shortestDistances[currentMapPoint] + edgeWeight < shortestDistances[mapPointIndex])
                    {
                        shortestDistances[mapPointIndex] = shortestDistances[currentMapPoint] + edgeWeight;
                        previousMapPoint[mapPointIndex] = currentMapPoint;
                    }
                }
            }

            List<Mappoint> ans = new List<Mappoint>();
            currentMapPoint = destination;
            while (currentMapPoint != -1)
            {
                ans.Insert(0, pointInAreaList[currentMapPoint]);
                currentMapPoint = previousMapPoint[currentMapPoint];
            }
            return ans;
        }

    }
    

}
 