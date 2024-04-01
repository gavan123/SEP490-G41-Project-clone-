using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class FacilityDAO
    {
        private readonly finsContext _context;

        public FacilityDAO(finsContext context)
        {
            _context = context;
        }

        // Thêm mới tòa nhà
        public void AddFacility(Facility facility)
        {
            _context.Facilities.Add(facility);
            _context.SaveChanges();
        }

        // Đọc thông tin tòa nhà bằng Id
        public Facility GetFacilityById(int facilityId)
        {
            return _context.Facilities.FirstOrDefault(f => f.FacilityId == facilityId);
        }

        public void UpdateFacility(Facility facility)
        {
            var existingFacility = _context.Facilities.FirstOrDefault(f => f.FacilityId == facility.FacilityId);

            if (existingFacility != null)
            {
                existingFacility.FacilityName = facility.FacilityName;
                existingFacility.Address = facility.Address;
                existingFacility.Status = facility.Status;

                _context.SaveChanges();
            }
        }

        public void DeleteFacility(int facilityId)
        {
            var facility = _context.Facilities.FirstOrDefault(f => f.FacilityId == facilityId);
            if (facility != null)
            {
                _context.Facilities.Remove(facility);
                _context.SaveChanges();
            }
        }

        public List<Facility> GetAllFacilities()
        {
            return _context.Facilities.ToList();
        }

        public List<Facility> SearchFacilitiesByName(string keyword)
        {
            var facilities = _context.Facilities
                .Where(f => f.FacilityName.Contains(keyword))
                .ToList();

            return facilities;
        }
    }
}
