﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data_Access
{
    [Table("TableOfBooks")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Author { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false; // use for soft delete
    }
}
