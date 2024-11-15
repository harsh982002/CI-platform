﻿using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using CIPlatform.Entitites.ViewModel;
using CIPlatform.Repository.Interface;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly CiplatformContext _db;

        public ProfileRepository(CiplatformContext db)
        {
            _db = db;
        }
        public bool Change_Password(string oldpassword, string newpassword, long user_id)
        {
            User? user = _db.Users.FirstOrDefault(c => c.UserId == user_id);
            if (user is not null)
            {

                bool verify = BCrypt.Net.BCrypt.Verify(oldpassword, user.Password);
                if (verify)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(newpassword);
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        public bool contact_us(long user_id, string name, string email, string subject, string message)
        {
            _db.ContactUs.Add(new ContactU { UserId = user_id, Name = name, Email = email, Subject = subject, Message = message });

            _db.SaveChanges();
            return true;
        }
        public ProfileViewModel Get_details(int country, long user_Id)
        {

            User? user = _db.Users.Find(user_Id);
            ProfileViewModel profile = new ProfileViewModel();
            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.WhyIVolunteer = user.WhyIVolunteer;
            profile.ProfileText = user.ProfileText;
            profile.CountryId = user.CountryId;
            profile.CityId = user.CityId;
            profile.Avatar = user.Avatar;
            profile.Availability = user.Availability;
            profile.EmployeeId = user.EmployeeId;
            profile.Department = user.Department;
            profile.userSkills = _db.UserSkills.Where(x => x.UserId == user.UserId).ToList();

            if (country == 0)
            {
                List<Country> countries = _db.Countries.ToList();
                List<Skill> skills = _db.Skills.ToList();
                List<City> cities = _db.Cities.ToList();
                profile.countries = countries;
                profile.cities = cities;
                profile.skill = skills;
                return profile;
            }
            else
            {
                List<City> cities = _db.Cities.Where(x => x.CountryId == country).ToList();
                profile.cities = cities;
                return profile;

            }
        }
        public bool profile_update(ProfileViewModel userdetail, long user_id)
        {
            User? user = _db.Users.FirstOrDefault(x => x.UserId == user_id);

            if (user is not null)
            {
                user.FirstName = userdetail.FirstName;
                user.LastName = userdetail.LastName;
                user.Title = userdetail.Title;
                user.WhyIVolunteer = userdetail.WhyIVolunteer;
                user.ProfileText = userdetail.ProfileText;
                user.Availability = userdetail.Availability;
                user.LinkedInUrl = userdetail.LinkedInUrl;
                user.Department = userdetail.Department;
                user.CityId = userdetail.CityId;
                user.CountryId = userdetail.CountryId;
                user.Availability = userdetail.Availability;
                user.EmployeeId = userdetail.EmployeeId;
                user.UpdatedAt = DateTime.Now;

                if (userdetail.profile is not null)
                {
                    using (var stream = userdetail.profile?.OpenReadStream())
                    {
                        var bytes = new byte[userdetail.profile.Length];
                        stream.Read(bytes, 0, (int)userdetail.profile.Length);
                        var base64string = Convert.ToBase64String(bytes);
                        user.Avatar = "data:image/png;base64," + base64string;
                    }
                }
                if (userdetail.selected_skills is not null)
                {
                    List<UserSkill> userSkills = _db.UserSkills.Where(x => x.UserId == user_id).ToList();
                    if (userSkills.Count > 0)
                    {
                        _db.RemoveRange(userSkills);
                        string[] skills = userdetail.selected_skills.Split(',');
                        foreach (var skill in skills)
                        {
                            _db.UserSkills.Add(new UserSkill { SkillId = int.Parse(skill), UserId = user_id });
                        }
                    }
                    else
                    {
                        string[] skills = userdetail.selected_skills.Split(',');
                        foreach (var skill in skills)
                        {
                            _db.UserSkills.Add(new UserSkill { SkillId = int.Parse(skill), UserId = user_id });
                        }
                    }

                }
                _db.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }
    }
}
