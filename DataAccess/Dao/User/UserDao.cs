using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.User
{
    public class UserDao : IUserDao
    {
        public IList<UserDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.User> users = db.User.ToList();

                var dtos = ToDtos(users);

                return dtos;
            }
        }

        public UserDto GetById(int userId)
        {
            using (var db = new DbContext())
            {
                Entities.User user = db.User.FirstOrDefault(x => x.Id == userId);

                return user == null ? null : ToDto(user);
            }
        }

        public UserDto GetByEmail(string email)
        {
            using (var db = new DbContext())
            {
                Entities.User user = db.User.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                return user == null ? null : ToDto(user);
            }
        }

        public UserDto GetByActivationCode(string activationCode)
        {
            using (var db = new DbContext())
            {
                Entities.User existingUser = db.User.FirstOrDefault(x => String.Equals(x.ActivationCode, activationCode));

                return existingUser == null ? null : ToDto(existingUser);
            }
        }

        public UserDto Insert(string name, string lastName, string role, string email, string password)
        {
            using (var db = new DbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var huntsman = new Entities.Huntsman
                        {
                            Name = name,
                            LastName = lastName,
                            Role = role
                        };

                        Entities.Huntsman newHuntsman = db.Huntsman.Add(huntsman);
                        db.SaveChanges();

                        var user = new Entities.User
                        {
                            HuntsmanId = newHuntsman.Id,
                            Email = email,
                            Password = password,
                            EmailVerified = false,
                            ActivationCode = Guid.NewGuid().ToString(),
                            ActivationCodeTimeStamp = DateTime.Now,
                            CreationTimeStamp = DateTime.Now
                        };

                        db.User.Add(user);
                        db.SaveChanges();

                        transaction.Commit();

                        return ToDto(user);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
            }
        }

        public void Update(UserDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.User entity = db.User.Single(x => x.Id == dto.Id);

                entity.Id = dto.Id;
                entity.HuntsmanId = dto.HuntsmanId;
                entity.Email = dto.Email;
                entity.EmailVerified = dto.EmailVerified;
                entity.ActivationCode = dto.ActivationCode;
                entity.Password = dto.Password;
                entity.ActivationCodeTimeStamp = dto.ActivationCodeTimeStamp;
                entity.CreationTimeStamp = dto.CreationTimeStamp;

                db.SaveChanges();
            }
        }

        private UserDto ToDto(Entities.User user)
        {
            var dto = new UserDto
            {
                Id = user.Id,
                HuntsmanId = user.HuntsmanId,
                Email = user.Email,
                Password = user.Password,
                EmailVerified = user.EmailVerified,
                ActivationCode = user.ActivationCode,
                ActivationCodeTimeStamp = user.ActivationCodeTimeStamp,
                CreationTimeStamp = user.CreationTimeStamp
            };

            return dto;
        }

        private IList<UserDto> ToDtos(List<Entities.User> entityList)
        {
            var dtos = new List<UserDto>();
            foreach (Entities.User entity in entityList)
            {
                UserDto dto = ToDto(entity);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
