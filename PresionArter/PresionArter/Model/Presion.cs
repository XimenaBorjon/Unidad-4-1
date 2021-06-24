using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PresionArter.Model
{
    [Table("PresionA")]
    public class Presion
    {
        [PrimaryKey]
        [AutoIncrement]
        [NotNull]
        public int Id { get; set; }
        [NotNull]
        public int Pulso { get; set; }
        [NotNull]
        public int Diastolica { get; set; }
        [NotNull]
        public int Sistolica { get; set; }
        [NotNull]
        public DateTime Fecha { get; set; } = DateTime.Now;

    }
}
