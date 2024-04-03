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

        public List<Member> GetAllMembers()
        {
            var list = _context.Members.ToList();
            return list;
        }

        //Thêm nhân viên mới
        public void AddNewMember(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }

        //Sửa thông tin cá nhân
        public void UpdateProfile(Member member)
        {
            var mem = _context.Members.FirstOrDefault(m => m.MemberId == member.MemberId);
            if (mem != null)
            {
                mem.FullName = member.FullName;
                mem.DoB = member.DoB;

            }
        }
        //Xóa bỏ 1 member
        public bool DeleteMember(string name)
        {
            bool result = false;
            var member = _context.Members.FirstOrDefault(m => m.FullName.Contains(name));
            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
                result = true;
            }
            return result;
        }

        //Login
        public bool Login(string username, string password)
        {
            var member = _context.Members.FirstOrDefault(m => m.Username.Equals(username) && m.Password.Equals(password));
            if (member != null)
            {
                return true;
            }
            return false;
        }
        public List<Member> SearchMemberByDoB(string date)
        {
            var list = _context.Members.Where(m => m.DoB.ToString() == date).ToList();
            return null;
        }
        public List<Member> SearchMemberByName(string name)
        {
            var list = _context.Members.Where(m => m.FullName.Contains(name)).ToList();
            return list;
        }
        public List<Member> SearchMemberByStatus(string status)
        {
            var list = _context.Members.Where(m => m.Status.Equals(status)).ToList();
            return list;
        }
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
    }
}

