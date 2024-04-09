using NetTopologySuite.Geometries;

public class MapPointDTO
{
    public int MapPointId { get; set; }
    public int MapId { get; set; }
    public NetTopologySuite.Geometries.Point Location { get; set; }
}

public class MapPointAddDTO
{
    public int MapId { get; set; }
    public NetTopologySuite.Geometries.Point Location { get; set; }
}

public class MapPointUpdateDTO
{
    public int MapPointId { get; set; }
    public int MapId { get; set; }
    public NetTopologySuite.Geometries.Point Location { get; set; }
}
