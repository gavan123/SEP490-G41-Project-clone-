﻿using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IMapRepository
    {
        MapDTO GetMapById(int mapId);
        List<MapDTO> GetAllMaps();
        void AddMap(MapAddDTO map, Member member);
        void UpdateMap(MapUpdateDTO map);
        void DeleteMap(int mapId);

        
    }
}
