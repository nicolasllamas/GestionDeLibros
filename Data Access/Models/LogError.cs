using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Models
{
    public class LogError
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LogErrorId { get; set; } = Guid.NewGuid();

        [Column(TypeName = "datetime")]
        public DateTime Datetime { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string ErrorMessage { get; set; }

    }
}
