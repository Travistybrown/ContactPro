﻿using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace ContactPro.Models
{
    public class Category
    {
        // Primary Key
        public int Id { get; set; }

        //Foreign Key
        [Required]
        public string? AppUserId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }

        //Navigation Properties
        public virtual AppUser? AppUser { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();

    }
}
