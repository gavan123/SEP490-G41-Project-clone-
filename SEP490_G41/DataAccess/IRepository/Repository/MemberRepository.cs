using BusinessObject.Models;
﻿using AutoMapper;
using BusinessObject.DTO;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Execution;

namespace DataAccess.IRepository.Repository
{
    public class MemberRepository : IMemberRepository
    {
        
        private readonly IMapper _mapper;
        private readonly MemberDAO _memberDAO;

        public MemberRepository(IMapper mapper, MemberDAO memberDAO)
        {
            _mapper = mapper;
            _memberDAO = memberDAO;
        }
        public void AddNewMember(AddMemberDTO member)
        {
            try
            {

                /*var memberModel = new Member
                {
                    FullName = member.FullName,
                    DoB = member.DoB,
                    Address = member.Address,
                    Phone = member.Phone,
                    Email = member.Email,
                    Username = member.Username,
                    Password = member.Password,
                    Status = member.Status,
                    RoleId = member.RoleId
                };
                _memberDAO.AddNewMember(memberModel);*/
            }
            catch (Exception ex)
            {
                throw new Exception("Can't add new member");
            }
        }
        public string ChangePassword(int id, ChangePasswordModel changePassword)
        {
            try
            {
                var result = _memberDAO.ChangePassword(id, changePassword);
                return result;
            }catch (Exception ex)
            {
                throw new Exception("Error");
            }
            
        }

        public bool DeleteMember(int id)
        {
            throw new Exception();
        } 

        public List<MemberDTO> GetAllMembers()
        {
            try
            {
                var members = _memberDAO.GetAllMembers();
                var memberDTOs = members.Select(m => _mapper.Map<MemberDTO>(m)).ToList();
                return memberDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Something has wrong!");
            }
        }

        public MemberDTO Login(string username, string password)
        {
            try
            {
                var member = _memberDAO.Login(username, password);
                return _mapper.Map<MemberDTO>(member);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public List<MemberDTO> SearchMemberByDoB(DateTime date)
        {
            try
            {
                var members = _memberDAO.SearchMemberByDoB(date);
                var memberDTOs = members.Select(m => _mapper.Map<MemberDTO>(m)).ToList();
                return memberDTOs;
            }catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public List<MemberDTO> SearchMemberByName(string name)
        {
            try
            {
                var members = _memberDAO.SearchMemberByName(name);
                var memberDTOs = members.Select(m => _mapper.Map<MemberDTO>(m)).ToList();
                return memberDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Something has wrong!");
            }
        }

        /*public List<MemberDTO> SearchMemberByStatus(string status)
        {
            try
            {
                var members = _memberDAO.SearchMemberByStatus(status);
                var memberDTOs = members.Select(m => _mapper.Map<MemberDTO>(m)).ToList();
                return memberDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Something has wrong!");
            }
            throw new NotImplementedException();
        }*/
    }
}
