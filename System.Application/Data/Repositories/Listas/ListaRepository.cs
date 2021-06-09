using Dapper;
using System;
using System.Application.Data.Entities.Listas;
using System.Application.Data.MySql;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Data.Repositories.Listas
{
    public class ListaRepository
    {
        private readonly MySqlContext sqlContext;
        public ListaRepository(MySqlContext _context)
        {
            this.sqlContext = _context;
        }

        public virtual async Task<ListaEntity> Create(ListaEntity _listaEntity)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = @"insert into listaDesejos (id, usuarioId, listaNome)
                                        values (@Pid, @PusuarioId, @PlistaNome)";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _listaEntity.Id,
                        PusuarioId = _listaEntity.usuarioId,
                        PlistaNome = _listaEntity.listaNome,
                    });

                    return _listaEntity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ListaRepository][Create] Erro ao tentar criar lista. " + ex);
                    return new ListaEntity();
                }
            }
        }
        public virtual async Task<bool> Delete(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = $"delete from listaDesejos where id = '{id}'";
                    await cnx.ExecuteAsync(sqlQuery);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ListaRepository][Delete] Erro ao tentar excluir lista. " + ex);
                    return false;
                }
            }
        }
        public virtual async Task<ListaEntity> Get(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = $@"select id, usuarioId, listaNome, dataCriacao from listaDesejos 
                                                where id = '{id}'";
                    return await cnx.QueryFirstOrDefaultAsync<ListaEntity>(sqlQuery);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ListaRepository][Get] Erro ao tentar consultar lista. " + ex);
                    return new ListaEntity();
                }
            }
        }
        public virtual async Task<ListaEntity> GetListByUserId(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {
                string sqlQuery = $@"select id, usuarioId, listaNome, dataCriacao from listaDesejos 
                                                where usuarioId = '{id}'";
                return await cnx.QueryFirstOrDefaultAsync<ListaEntity>(sqlQuery);
            }
        }
    }
}
