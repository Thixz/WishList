using System;
using System.Application.Contracts.ListaItens;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Data.Entities.ListaItens
{
    [Table("listaItens")]
    public class ListaItemEntity
    {
        public ListaItemEntity(ListaItemPostRequest _postRequest)
        {
            this.Id = Guid.NewGuid();
            this.produtoId = _postRequest.produtoId;
            this.listaId = _postRequest.listaId;
            this.Comprado = _postRequest.Comprado;
        }
        public ListaItemEntity()
        {

        }

        ///<summary>
        ///Id Lista
        ///</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }
        ///<summary>
        ///Id Produto
        ///</summary>
        [Column("produtoId")]
        public Guid produtoId { get; set; }
        ///<summary>
        ///Id Lista
        ///</summary>
        [Column("listaId")]
        public Guid listaId { get; set; }
        ///<summary>
        ///Produto Comprado ou Ganho
        ///</summary>
        [Column("comprado")]
        public bool Comprado { get; set; }
        ///<summary>
        ///Data Criação
        ///</summary>
        [Column("dataCriacao")]
        public DateTime dataCriacao { get; set; }
    }
}
