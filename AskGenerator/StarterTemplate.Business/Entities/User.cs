using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class User : IdentityUser
    {
        #region ctors
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        public User(string groupId, string studentId)
            : this()
        {
            GroupId = groupId;
            StudentId = studentId;
        }
        #endregion

        [Column(Order = 1)]
        [StringLength(64)]
        public string GroupId { get; set; }

        [ForeignKey("Student")]
        public string StudentId { get; set; }

        [MaxLength(8)]
        [Index(IsUnique=true)]
        public string LoginKey { get; set; }

        public Student Student { get; set; }

        public override string UserName
        {
            get
            {
                var name = base.UserName;
                if (string.IsNullOrEmpty(name))
                    base.UserName = name = Email.IsEmpty() ? Guid.NewGuid().ToString("N") : Email.Split('@').FirstOrDefault();
                return name;
            }
            set
            {
                base.UserName = value;
            }
        }

        #region GenerateLoginKey
        private const short additionalElements = 4;
        private static readonly char[] chars = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public void GenerateLoginKey(int number)
        {
            // To suttisfy max length validation
            number %= 1000;

            var rnd = new Random();
            var additionalChars = new List<char>(4);

            for (var i = 0; i < additionalElements; i++)
            {
                additionalChars.Add(chars[rnd.Next(chars.Length)]);
            }

            var sb = new StringBuilder();
            for (var i = 0; i < 3; i++)
            {
                // Add additional character in random place
                for (var j = 0; j < additionalChars.Count; j++)
                {
                    if (rnd.Next(additionalElements) + 1 == additionalElements)
                    {
                        sb.Append(additionalChars[j]);
                        additionalChars.RemoveAt(j);
                        j--;
                    }
                }

                // Add digit from param
                sb.Append(number % 10);
                number /= 10;
            }
            // Add lefted additional characters
            foreach (var c in additionalChars)
                sb.Append(c);

            LoginKey = sb.ToString();
        }
        #endregion
    }

    public class Role : IdentityRole
    {
        public const string Admin = "admin";
        public const string User = "user";
        public const string FacultyAdmin = "fadmin";
    }
}
