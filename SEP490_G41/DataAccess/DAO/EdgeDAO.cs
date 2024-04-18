﻿using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAO
{
    public class EdgeDAO
    {
        private readonly finsContext _context;

        public EdgeDAO(finsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public EdgeDAO()
        {
            _context = new finsContext(); // Initialize with a new instance for testing
        }

        // Thêm mới edge
        public void AddEdge(Edge edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException(nameof(edge));
            }

            _context.Edges.Add(edge);
            _context.SaveChanges();
        }

        // Đọc thông tin edge bằng Id
        public Edge GetEdgeById(int edgeId)
        {
            if (edgeId <= 0)
            {
                throw new ArgumentException("Invalid edge ID", nameof(edgeId));
            }

            return _context.Edges.FirstOrDefault(e => e.EdgeId == edgeId);
        }

        // Cập nhật thông tin edge
        public void UpdateEdge(Edge edge)
        {
            if (edge == null)
            {
                throw new ArgumentNullException(nameof(edge));
            }

            var existingEdge = _context.Edges.FirstOrDefault(e => e.EdgeId == edge.EdgeId);

            if (existingEdge != null)
            {
                existingEdge.MapPointA = edge.MapPointA;
                existingEdge.MapPointB = edge.MapPointB;
                existingEdge.Direction = edge.Direction;
                existingEdge.Distance = edge.Distance;

                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Edge not found", nameof(edge));
            }
        }

        // Xóa edge bằng Id
        public void DeleteEdge(int edgeId)
        {
            if (edgeId <= 0)
            {
                throw new ArgumentException("Invalid edge ID", nameof(edgeId));
            }

            var edge = _context.Edges.FirstOrDefault(e => e.EdgeId == edgeId);

            if (edge != null)
            {
                _context.Edges.Remove(edge);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Edge not found", nameof(edgeId));
            }
        }

        // Lấy danh sách tất cả các edges
        public List<Edge> GetAllEdges()
        {
            return _context.Edges.ToList();
        }
    }
}