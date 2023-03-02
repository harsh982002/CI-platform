using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;

using CIPlatform.Repository.Interface;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CIPlatform.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CiplatformContext _db;

        public AccountRepository(CiplatformContext db)
        {
            _db = db;
        }

        public User Forgot(User obj)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == obj.Email && u.DeletedAt == null);


            #region Genrate Token
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            #endregion Genrate Token

            #region Update Password Reset Table
            PasswordReset entry = new PasswordReset();
            entry.Email = obj.Email;
            entry.Token = finalString;
            _db.PasswordResets.Add(entry);
            _db.SaveChanges();
            #endregion Update Password Reset Table

            #region Send Mail
            var mailBody = "<h1>Click link to reset password</h1><br><h2><a href='" + "https://localhost:7160/UserAccount/Resetpassword?token=" + finalString + "'>Reset Password</a></h2>";
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("parthvijaypatel@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Reset Your Password";
            email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("parthvijaypatel@gmail.com", "eykgtqanjxpquqfm");
            smtp.Send(email);
            smtp.Disconnect(true);
            #endregion Send Mail

            return user;
        }

        public User Login(User obj)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == obj.Email && u.Password == obj.Password);
            return user;
        }

        public User Register(User obj)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == obj.Email);
            if (user == null)
            {
                _db.Users.Add(obj);
                _db.SaveChanges();
            }
            return user;
        }

        public PasswordReset Reset(User obj, string token)
        {
            var validToken = _db.PasswordResets.FirstOrDefault(x => x.Token == token);
            if (validToken != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.Email == validToken.Email);
                user.Password = obj.Password;
                _db.Users.Update(user);
                _db.SaveChanges();

            }
            return validToken;
        }
    }
}
