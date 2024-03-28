using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository.Repository
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly FacilityDAO _facilityDAO;
        private readonly IMapper _mapper;

        public FacilityRepository(FacilityDAO facilityDAO, IMapper mapper)
        {
            _facilityDAO = facilityDAO;
            _mapper = mapper;
        }

        public FacilityDTO GetFacilityById(int facilityId)
        {
            return _mapper.Map<FacilityDTO>(_facilityDAO.GetFacilityById(facilityId));
        }

        public List<FacilityDTO> GetAllFacilities()
        {
            var facilities = _facilityDAO.GetAllFacilities();
            var facilityDTOs = facilities.Select(facility => _mapper.Map<FacilityDTO>(facility)).ToList();
            return facilityDTOs;
        }

        public void AddFacility(FacilityAddDTO facility)
        {
            _facilityDAO.AddFacility(_mapper.Map<Facility>(facility));
        }

        public void UpdateFacility(FacilityUpdateDTO facility)
        {
            _facilityDAO.UpdateFacility(_mapper.Map<Facility>(facility));
        }

        public void DeleteFacility(int facilityId)
        {
            _facilityDAO.DeleteFacility(facilityId);
        }

    }
}
