﻿using Cooperchip.ITDeveloper.Domain.Entities;

namespace Cooperchip.ITDeveloper.Domain.Models
{
    public class Medicamento : EntityBase
    {
        public Medicamento() { }

        //MedicamentoId;Descricao;Generico;IdGenerico

        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Generico { get; set; }
        public int CodigoGenerico { get; set; }


    }
}
