

using CIPlatform.Entities.ViewModel;
using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;

using CIPlatform.Repository.Interface;
using MailKit.Security;
using Microsoft.AspNetCore.DataProtection;
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

        private readonly CiplatformContext _context;
        private readonly IDataProtector protector;

        public AccountRepository(CiplatformContext ciPlatformContext)
        {
            _context = ciPlatformContext;
        }
        public string _Message { get; set; }
        public string Message()
        {
            return _Message;
        }



        public User LoginViewModel(LoginViewModel model)
        {
            User user = _context.Users.FirstOrDefault(c => c.Email.Equals(model.Email.ToLower()));
            return user;
        }



        public User RegistrationViewModel(RegistrationViewModel model)
        {
            string secpass = BCrypt.Net.BCrypt.HashPassword(model.Password);
            User user = new User();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.Password = secpass;
            var entry = _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;

        }

        public User ForgotPasswordViewModel(ForgotPasswordViewModel model)
        {

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.DeletedAt == null);


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
            entry.Email = model.Email;
            entry.Token = finalString;
            _context.PasswordResets.Add(entry);
            _context.SaveChanges();
            #endregion Update Password Reset Table

            #region Send Mail

            var PasswordReset = _context.PasswordResets.FirstOrDefault(u => u.Email == model.Email);


            var mailBody = "<h1>Click link to reset password</h1><br><h2><a href='" + "https://localhost:44335/UserAccount/Resetpassword?token=" + PasswordReset.Token + "'>Reset Password</a></h2>";
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("harshrathod982002@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Reset Your Password";
            email.Body = new TextPart(TextFormat.Html) { Text = mailBody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("harshrathod982002@gmail.com", "ejvjkaneyltiqawq");
            smtp.Send(email);
            smtp.Disconnect(true);
            #endregion Send Mail

            return user;
        }



        public Boolean IsValidUserEmail(RegistrationViewModel model)
        {
            try
            {
                User? user = _context.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                if (user == null)
                {
                    _Message += " Entered email is not registered";
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                _Message += ex.Message;
                return false;
            }
        }

        public int GetUserID(string Email)
        {
            try
            {
                User? user = _context.Users.Where(x => x.Email == Email).FirstOrDefault();
                if (user == null)
                {
                    _Message += " Invalid Email";
                    return -1;
                }
                else
                {
                    return (int)user.UserId;
                }
            }
            catch (Exception ex)
            {
                _Message += ex.Message;
                return -1;
            }
        }
        public User GetUser(int userID)
        {
            try
            {
                User? user = _context.Users.Where(x => x.UserId == userID).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    _Message += " Enter valid Email ";
                    return null;
                }
            }
            catch (Exception ex)
            {
                _Message += ex.Message;
                return null;
            }
        }

        public PasswordReset ResetPasswordViewModel(ResetPasswordViewModel model, string token)
        {
            string passtoken = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var validToken = _context.PasswordResets.FirstOrDefault(x => x.Token == token);
            if (validToken != null)
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == validToken.Email);
                user.Password = passtoken;
                _context.Users.Update(user);
                _context.SaveChanges();

            }
            return validToken;
        }


    }
}