using BusinessObject.Models;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
=======
>>>>>>> 70fbe8c830cbcd9629ef3db8b99e93c81e4afb56
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using BusinessObject.Validate;
using MySqlX.XDevAPI;
>>>>>>> 70fbe8c830cbcd9629ef3db8b99e93c81e4afb56

namespace DataAccess.DAO
{
    public class MemberDAO
    {
<<<<<<< HEAD
            private readonly finsContext _context;
            Validate validate = new Validate();

            public MemberDAO(finsContext context)
            {
                _context = context;
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

            //

            public bool Login(string username, string password)
            {
                var member = _context.Members.FirstOrDefault(m => m.Username.Equals(username) && m.Password.Equals(password));
                if (member != null)
                {
                    return true;
                }
                return false;
            }

            public bool SendEmail(string toEmail)
            {
                // Thông tin tài khoản email gửi
                string emailFrom = "catminh2k1@gmail.com";
                string password = "xtgf kmfb bgtv rkqe";
                string emailTo = "minhpche153232@fpt.edu.vn";
                try
                {
                    // Tạo một đối tượng MailMessage
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.IsBodyHtml = true;
                    mail.Subject = "Test Email";
                    mail.Body = "This is a test email";

                    // Cấu hình SMTP client
                    SmtpClient smtpServer = new SmtpClient("smtp.example.com", 587);
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
=======
        private readonly finsContext _context;
        Validate validate = new Validate();

        public MemberDAO(finsContext context)
        {
            _context = context;
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
        public bool DeleteMember(string name) {
            bool result = false;
            var member = _context.Members.FirstOrDefault( m => m.FullName.Contains(name));
            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
                result = true;
            }
            return result;
        }

        //

        public bool Login(string username, string password) {
            var member = _context.Members.FirstOrDefault(m => m.Username.Equals(username) && m.Password.Equals(password));
            if (member != null)
            {
                return true;
            }
            return false;
        }

        public bool SendEmail(string toEmail)
        {
            // Thông tin tài khoản email gửi
            string emailFrom = "catminh2k1@gmail.com";
            string password = "xtgf kmfb bgtv rkqe";
            string emailTo = "minhpche153232@fpt.edu.vn";
            try
            {
                // Tạo một đối tượng MailMessage
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.IsBodyHtml = true;
                mail.Subject = "Test Email";
                mail.Body = "This is a test email";

                // Cấu hình SMTP client
                SmtpClient smtpServer = new SmtpClient("smtp.example.com", 587);
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
>>>>>>> 70fbe8c830cbcd9629ef3db8b99e93c81e4afb56
