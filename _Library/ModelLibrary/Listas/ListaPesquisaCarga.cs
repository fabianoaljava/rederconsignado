using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class ListaPesquisaCarga
    {
        public int Id { get; set; }
        public int PracaId { get; set; }
        public string Praca { get; set; }
        public int RepresentanteId { get; set; }
        public string Representante { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public Nullable<DateTime> DataAbertura { get; set; }
        public Nullable<DateTime> DataExportacao { get; set; }
        public Nullable<DateTime> DataRetorno { get; set; }
        public Nullable<DateTime> DataConferencia { get; set; }
        public Nullable<DateTime> DataFinalizacao { get; set; }
        public string Status { get; set; }
        public int AnoMes { get; set; }
    }
}
