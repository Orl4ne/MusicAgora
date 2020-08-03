using Identity.DAL;
using MusicAgora.Common.Library.TransferObjects;
using SQLitePCL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAgora.WebUx.MVC.Models
{
    public class GlobalUserVM
    {
        public ApplicationUser IdentityUser { get; set; }
        private List<AccessRight> _roles;
        public LibUserTO LibraryUser { get; set; }
        public bool[] SelectedRoles { get; set; }

        public List<AccessRight> Roles {
            get => _roles;
            set
            {
                _roles = value;
                SelectedRoles = new bool[_roles.Count];
                if (UserRoles != null) SetSelectedRoles();
            } 
        }

        // Just for collecting checkbox post results.
        // bool arrays initialize with false values everywhere.
        private List<String> _userRoles;
        public List<String> UserRoles {
            get => _userRoles; 
            set
            {
                _userRoles = value;
                if (Roles != null) SetSelectedRoles();
            } 
        }
        private void SetSelectedRoles()
        {
            for (var i = 0; i < Roles.Count; i++)
            {
                if (UserRoles.Contains(Roles[i].Name))
                {
                    // Set the checkbox initial value:
                    SelectedRoles[i] = true;
                }
            }
        }
    }
}
