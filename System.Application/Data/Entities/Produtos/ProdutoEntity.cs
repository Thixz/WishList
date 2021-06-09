using System;
using System.Application.Contracts.Produtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Data.Entities.Produtos
{
    [Table("usuarios")]
    public class ProdutoEntity
    {
        public ProdutoEntity(ProdutoPostRequest _postRequest)
        {
            this.Id = Guid.NewGuid();
            this.tituloProduto = _postRequest.tituloProduto;
            this.Descricao = _postRequest.Descricao;
        }
        public ProdutoEntity(ProdutoPutRequest _putRequest)
        {
            this.Id = _putRequest.Id;
            this.tituloProduto = _putRequest.tituloProduto;
            this.Descricao = _putRequest.Descricao;
        }
        public ProdutoEntity()
        {

        }

        ///<summary>
        ///Id Produto
        ///</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }
        ///<summary>
        ///Título Produto
        ///</summary>
        [Column("tituloProduto")]
        public string tituloProduto { get; set; }
        ///<summary>
        ///Descrição Produto
        ///</summary>
        [Column("descricao")]
        public string Descricao { get; set; }
        ///<summary>
        ///Data Criação
        ///</summary>
        [Column("dataCriacao")]
        public DateTime dataCriacao { get; set; }
        ///<summary>
        ///Data Atualizacao
        ///</summary>
        [Column("dataAtualizacao")]
        public DateTime dataAtualizacao { get; set; }
    }
}
