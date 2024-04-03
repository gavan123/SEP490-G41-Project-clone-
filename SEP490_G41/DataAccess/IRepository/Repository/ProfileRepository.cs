using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;


namespace DataAccess.IRepository.Repository
{
    public class ProfileRepository: IProfileRepository
    {
        private readonly ProfileDAO _profileDAO;
        private readonly IMapper _mapper;

        public ProfileRepository(ProfileDAO profileDAO, IMapper mapper)
        {
            _profileDAO = profileDAO;
            _mapper = mapper;
        }
        public MemberDTO GetMemberById(int memberid)
        {
            var profile = _profileDAO.GetMemberById(memberid);
            if (profile == null)
            {
                throw new Exception("members not found");
            }
            return _mapper.Map<MemberDTO>(profile);
        }

        public void UpdateProfile(MemberUpdateDTO profile)
        {
            try
            {
                _profileDAO.UpdateProfile(_mapper.Map<Member>(profile));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating member.", ex);
            }
        }
    }
}
