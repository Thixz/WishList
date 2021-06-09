using Dapper;
using System;
using System.Application.Data.Entities.Usuarios;
using System.Application.Data.MySql;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Data.Repositories.Usuarios
{
    public class UsuarioRepository
    {
        private readonly MySqlContext sqlContext;
        public UsuarioRepository(MySqlContext _context)
        {
            this.sqlContext = _context;
        }

        public virtual async Task<UsuarioEntity> Create(UsuarioEntity _usuarioEntity)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = @"insert into usuarios (id, nome, documento, telefone, email)
                                        values (@Pid, @Pnome, @Pdocumento, @Ptelefone, @Pemail)";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _usuarioEntity.Id,
                        Pnome = _usuarioEntity.Nome,
                        Pdocumento = _usuarioEntity.Documento,
                        Ptelefone = _usuarioEntity.Telefone,
                        Pemail = _usuarioEntity.Email,
                    });

                    return _usuarioEntity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[UsuarioRepository][Update] Erro ao tentar criar usuário. " + ex);
                    return new UsuarioEntity();
                }
            }
        }
        public virtual async Task<UsuarioEntity> Update(UsuarioEntity _usuarioEntity)
        {
            try
            {
                using (var cnx = sqlContext.Conectar())
                {
                    string sqlQuery = @"update usuarios set nome = @Pnome,
                                                         documento = @Pdocumento,
                                                         telefone = @Ptelefone, 
                                                         email = @Pemail   
                                                         where id = @Pid";
                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _usuarioEntity.Id,
                        Pnome = _usuarioEntity.Nome,
                        Pdocumento = _usuarioEntity.Documento,
                        Ptelefone = _usuarioEntity.Telefone,
                        Pemail = _usuarioEntity.Email,
                    });

                    return _usuarioEntity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UsuarioRepository][Update] Erro ao tentar atualizar usuário. " + ex);
                return new UsuarioEntity();
            }
        }
        public virtual async Task<bool> Delete(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = $"delete from usuarios where id = '{id}'";
                    await cnx.ExecuteAsync(sqlQuery);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[UsuarioRepository][Delete] Erro ao tentar excluir usuário. " + ex);
                    return false;
                }
            }
        }
        public virtual async Task<UsuarioEntity> Get(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = $@"select id, nome, documento, telefone, email,
                                                dataCriacao, dataAtualizacao from usuarios 
                                                where id = '{id}'";
                    return await cnx.QueryFirstOrDefaultAsync<UsuarioEntity>(sqlQuery);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[UsuarioRepository][Get] Erro ao tentar consultar usuário. " + ex);
                    return new UsuarioEntity();
                }
            }
        }
        public virtual async Task<UsuarioEntity> GetByDocument(string document)
        {
            using (var cnx = sqlContext.Conectar())
            {
                string sqlQuery = $@"select id, nome, documento, telefone, email,
                                                dataCriacao, dataAtualizacao from usuarios 
                                                where documento = '{document}'";
                return await cnx.QueryFirstOrDefaultAsync<UsuarioEntity>(sqlQuery);
            }
        }
        public virtual async Task<UsuarioEntity> GetByEmail(string email)
        {
            using (var cnx = sqlContext.Conectar())
            {
                string sqlQuery = $@"select id, nome, documento, telefone, email,
                                                dataCriacao, dataAtualizacao from usuarios 
                                                where email = '{email}'";
                return await cnx.QueryFirstOrDefaultAsync<UsuarioEntity>(sqlQuery);
            }
        }
    }
}
