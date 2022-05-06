namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Transact")]
    public partial class Transact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTransaction { get; set; }

        [StringLength(50)]
        public string TypeTransaction { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateTransact { get; set; }

        public decimal? Montant { get; set; }

        public int IdCompte { get; set; }

        public virtual Compte Compte { get; set; }
    }
}
