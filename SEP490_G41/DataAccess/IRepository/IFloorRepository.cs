﻿using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IFloorRepository
    {
        FloorDTO GetFloorById(int floorId);
        List<FloorDTO> GetAllFloors();
        void AddFloor(FloorAddDTO floor);
        void UpdateFloor(FloorDTO floor);
        void DeleteFloor(int floorId);

        /* List<FloorDTO> SearchFloorsByName(string keyword);
           List<FloorDTO> SearchFloorsByStatus(string status); */
    }
}