using System;
using System.Application.Contracts.Listas;
using System.Application.Data.Entities.ListaItens;
using System.Application.Views;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Data.Entities.Listas
{
    [Table("listaDesejos")]
    public class ListaEntity
    {
        public ListaEntity(ListaPostRequest _postRequest)
        {
            this.Id = Guid.NewGuid();
            this.usuarioId = _postRequest.usuarioId;
            this.listaNome = _postRequest.listaNome;
            this.Itens = new List<ListaItemViewEntity>();
        }
        public ListaEntity()
        {

        }

        ///<summary>
        ///Id Lista
        ///</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }
        ///<summary>
        ///Id Usuário
        ///</summary>
        [Column("usuarioId")]
        public Guid usuarioId { get; set; }
        ///<summary>
        ///Lista Nome
        ///</summary>
        [Column("listaNome")]
        public string listaNome { get; set; }
        ///<summary>
        ///Lista Itens
        ///</summary>
        [Column("itens")]
        public List<ListaItemViewEntity> Itens { get; set; }
        ///<summary>
        ///Data Criação
        ///</summary>
        [Column("dataCriacao")]
        public DateTime dataCriacao { get; set; }
    }
}
