using System;
using System.Application.Contracts.Usuarios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Application.Data.Entities.Usuarios
{
    [Table("usuarios")]
    public class UsuarioEntity
    {
        public UsuarioEntity(UsuarioPostRequest _postRequest)
        {
            this.Id = Guid.NewGuid();
            this.Nome = _postRequest.Nome;
            this.Documento = _postRequest.Documento;
            this.Telefone = _postRequest.Telefone;
            this.Email = _postRequest.Email;
        }
        public UsuarioEntity(UsuarioPutRequest _putRequest)
        {
            this.Id = _putRequest.Id;
            this.Nome = _putRequest.Nome;
            this.Documento = _putRequest.Documento;
            this.Telefone = _putRequest.Telefone;
            this.Email = _putRequest.Email;
        }
        public UsuarioEntity()
        {

        }

        ///<summary>
        ///Id Usuário
        ///</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }
        ///<summary>
        ///Nome Usuário
        ///</summary>
        [Column("nome")]
        public string Nome { get; set; }
        ///<summary>
        ///Documento Usuário
        ///</summary>
        [Column("documento")]
        public string Documento { get; set; }
        ///<summary>
        ///Telefone Usuário
        ///</summary>
        [Column("telefone")]
        public string Telefone { get; set; }
        ///<summary>
        ///Email Usuário
        ///</summary>
        [Column("email")]
        public string Email { get; set; }
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
