﻿namespace Product_Display.Models.View_Model
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }  
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }

}
