using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopOnline.Model.Dao
{
    public class UserDao
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        /// <summary>
        /// Function check login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string userName, string password, bool isLoginAdmin = false)
        {
            var result = db.User.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupId == CommonConstants.ADMIN_GROUP || result.GroupId == CommonConstants.MOD_GROUP)
                    {
                        if (result.IsDeleted == true)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == password)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.GroupId == CommonConstants.MEMBER_GROUP)
                    {
                        if (result.IsDeleted == true)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == password)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
            }
        }

        public User GetById(string userName)
        {
            return db.User.OrderByDescending(x => x.CreatedOn).SingleOrDefault(x => x.UserName == userName);
        }

        ///Phan quyen credential
        ///public List<string> GetListCredential(string userName)
        public List<string> GetListCredential(string userName)
        {
            var user = db.User.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroup on a.UserGroupId equals b.Id
                        join c in db.Role on a.RoleId equals c.Id
                        where b.Id == user.GroupId
                        select new
                        {
                            RoleId = a.RoleId,
                            UserGroupId = a.UserGroupId
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleId = x.RoleId,
                            UserGroupId = x.UserGroupId
                        });
            return data.Select(x => x.RoleId).ToList();
        }

        /// <summary>
        /// Check duplicate user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {
            return db.User.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.User.Count(x => x.Email == email) > 0;
        }
        /// <summary>
        /// Insert for facebook
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public Guid? InsertForFacebook(User entity)
        //{
        //    var user = db.User.SingleOrDefault(x => x.UserName == entity.UserName);
        //    if (user == null)
        //    {
        //        entity.Id = Guid.NewGuid();
        //        db.User.Add(entity);
        //        db.SaveChanges();
        //        return entity.Id;
        //    }
        //    else
        //    {
        //        return user.Id;
        //    }
        //}
        //Change user status
        public bool ChangeStatus(Guid? id)
        {
            var user = db.User.Find(id);
            user.IsDeleted = !user.IsDeleted;
            db.SaveChanges();
            return user.IsDeleted;
        }
    }
}
