using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Views
{
    public class ListaItemViewEntity
    {
        [Column("tituloProduto")]
        public string tituloProduto { get; set; }
        [Column("descricao")]
        public string descricao { get; set; }
        [Column("comprado")]
        public bool comprado { get; set; }
    }
}
