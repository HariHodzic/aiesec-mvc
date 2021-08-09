using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Aiesec.Data.Model.BusinessModel;

namespace Aiesec.Data.Model.IdentityModel
{
    public enum Gender
    {
        Male = 1,
        Female,
        Other
    }

    public static class SystemRoles
    {
        public static readonly string[] Roles =
        {
            "Alumni",
            "Team Member",
            "Team Leader",
            "Member Committee Vice President",
            "Local Committee President",
            "Admin",
            "Super Admin"
        };
    }

    public sealed class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }

        public DateTime MemberSince { get; set; }
        public ICollection<ChatUser> Chats { get; set; }
        //----
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Active { get; set; }
    }

    public class ApplicationRole : IdentityRole<int>
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Active { get; set; }

        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }

    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Active { get; set; }
    }

    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Active { get; set; }
    }

    public class ApplicationRoleClaim : IdentityRoleClaim<int>
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Active { get; set; }
    }

    public class ApplicationUserToken : IdentityUserToken<int>
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Active { get; set; }
    }

    public class ApplicationUserLogin : IdentityUserLogin<int>
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool Active { get; set; }
    }
}