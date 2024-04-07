﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class FloorDAO
    {
        private readonly finsContext _context;

        public FloorDAO(finsContext context)
        {
            _context = context;
        }

        // Thêm mới tầng
        public void AddFloor(Floor floor)
        {
            _context.Floors.Add(floor);
            _context.SaveChanges();
        }

        // Đọc thông tin tầng bằng Id
        public Floor GetFloorById(int floorId)
        {
            return _context.Floors.FirstOrDefault(f => f.FloorId == floorId);
        }

        public void UpdateFloor(Floor floor)
        {
            var existingFloor = _context.Floors.FirstOrDefault(f => f.FloorId == floor.FloorId);

            if (existingFloor != null)
            {
                existingFloor.FloorName = floor.FloorName;
                existingFloor.Greeting = floor.Greeting;
                existingFloor.Status = floor.Status;
                existingFloor.BuildingId = floor.BuildingId;

                _context.SaveChanges();
            }
        }

        public void DeleteFloor(int floorId)
        {
            var floor = _context.Floors.FirstOrDefault(f => f.FloorId == floorId);
            if (floor != null)
            {
                _context.Floors.Remove(floor);
                _context.SaveChanges();
            }
        }

        public List<Floor> GetAllFloors()
        {
            return _context.Floors.ToList();
        }

        public List<Floor> SearchFloorsByName(string keyword)
        {
            var floors = _context.Floors
                .Where(f => f.FloorName.Contains(keyword))
                .ToList();

            return floors;
        }
    }
}