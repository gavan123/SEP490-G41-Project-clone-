using NetTopologySuite.Geometries;

public class MapPointDTO
{
    public int MapPointId { get; set; }
    public int MapId { get; set; }
    public string MappointName { get; set; }
    public string LocationWeb { get; set; }
    public string LocationApp { get; set; }
    public string LocationGps { get; set; }
    public int FloorId { get; set; }
    public int BuildingId { get; set; }
    public string Image { get; set; }
    public bool Destination { get; set; }

}

public class MapPointAddDTO
{
    public int MapId { get; set; }
    public string MappointName { get; set; }
    public string LocationWeb { get; set; }
    public string LocationApp { get; set; }
    public string LocationGps { get; set; }
    public int FloorId { get; set; }
    public int BuildingId { get; set; }
    public string Image { get; set; }
    public bool Destination { get; set; }

}

public class MapPointUpdateDTO
{
    public int MapPointId { get; set; }
    public int MapId { get; set; }
    public string MappointName { get; set; }
    public string LocationWeb { get; set; }
    public string LocationApp { get; set; }
    public string LocationGps { get; set; }
    public int FloorId { get; set; }
    public int BuildingId { get; set; }
    public string Image { get; set; }
    public bool Destination { get; set; }

}
