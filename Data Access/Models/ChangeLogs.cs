using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access.Models
{
    [Table("ChangeLogs")]
    public class ChangeLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ChangeLogId { get; set; } = Guid.NewGuid();

        public Guid? BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string FieldName { get; set; } // Name of the field that was changed (Title, Author, etc)

        [Column(TypeName = "varchar(max)")]
        public string OldValue { get; set; } // Value (it can be null for insertions)

        [Column(TypeName = "varchar(max)")]
        public string NewValue { get; set; } // Value (it can be null for eliminations)

        [Required]
        public DateTime ChangeDate { get; set; }
    }
}
