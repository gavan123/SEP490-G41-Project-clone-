using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Validate;
using MySqlX.XDevAPI;
using System.Xml.Linq;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        private readonly finsContext _context;
        Validate validate = new Validate();

        public MemberDAO(finsContext context)
        {
            _context = context;
        }
        #region Get all members
        public List<Member> GetAllMembers()
        {
            var list = _context.Members.ToList();
            return list;
        }
        #endregion

        #region Add new member
        public void AddNewMember(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }
        #endregion

        #region Delete member
        public bool DeleteMember(int id)
        {
            bool result = false;
            var member = _context.Members.FirstOrDefault(m => m.MemberId == id);
            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
                result = true;
            }
            return result;
        }
        #endregion

        #region Login
        public Member Login(string username, string password)
        {
            var member = _context.Members.FirstOrDefault(m => m.Username.Equals(username) && m.Password.Equals(validate.EncodePassword(password)));
            if (member != null)
            {
                return member;
            }
            return null;
        }
        #endregion

        #region Search member by DoB
        public List<Member> SearchMemberByDoB(DateTime date)
        {
            var list = _context.Members.Where(m => m.DoB == date).ToList();
            return list;
        }
        #endregion

        #region Search member by name
        public List<Member> SearchMemberByName(string name)
        {
            if (name == null)
            {
                return _context.Members.ToList();
            }
            var list = _context.Members.Where(m => m.FullName.Contains(name)).ToList();
            return list;
        }
        #endregion

        public string SetMemberStatus()
        {
            return null;
        }
        #region Send email
        public bool SendEmail(string email)
        {
            // Thông tin tài khoản email gửi
            string emailFrom = "catminh2k1@gmail.com";
            string password = "xtgf kmfb bgtv rkqe";
            try
            {
                // Tạo một đối tượng MailMessage
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(email);
                mail.IsBodyHtml = true;
                mail.Subject = "Reset Code";
                mail.Body = @"This is your reset code: " + validate.RandomCode();

                // Cấu hình SMTP client
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587);
                smtpServer.Credentials = new NetworkCredential(emailFrom, password);
                // Sử dụng SSL/TLS
                smtpServer.EnableSsl = true;
                // Gửi email
                smtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
        #endregion

        #region Change password
        public string ChangePassword(int id, string oldpass, string newpass, string re_newpass)
        {
            var mem = _context.Members.FirstOrDefault(m => m.MemberId == id && m.Password.Equals(validate.EncodePassword(oldpass)));
            if (mem != null)
            {
                if (oldpass != newpass)
                {
                    if (newpass == re_newpass)
                    {
                        mem.Password = validate.EncodePassword(newpass);
                        _context.SaveChanges();
                        return "Success";
                    }
                    return "NotEqual";
                }
                return "Same";
            }
            return "Incorrect";
        }
        #endregion
    }
}

