using Dapper;
using System;
using System.Application.Data.Entities.ListaItens;
using System.Application.Data.MySql;
using System.Application.Views;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Data.Repositories.ListaItens
{
    public class ListaItemRepository
    {
        private readonly MySqlContext sqlContext;
        public ListaItemRepository(MySqlContext _context)
        {
            this.sqlContext = _context;
        }

        public virtual async Task<ListaItemEntity> Create(ListaItemEntity _listaItemEntity)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = @"insert into listaItens (id, produtoId, listaId, comprado)
                                        values (@Pid, @PprodutoId,@PlistaId, @Pcomprado)";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _listaItemEntity.Id,
                        PprodutoId = _listaItemEntity.produtoId,
                        PlistaId = _listaItemEntity.listaId,
                        Pcomprado = _listaItemEntity.Comprado,
                    });

                    return _listaItemEntity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ListaItemRepository][Create] Erro ao tentar criar item. " + ex);
                    return new ListaItemEntity();
                }
            }
        }
        public virtual async Task<ListaItemEntity> Update(ListaItemEntity _listaItemEntity)
        {
            try
            {
                using (var cnx = sqlContext.Conectar())
                {
                    string sqlQuery = @"update listaItens set produtoId = @PprodutoId,
                                                         listaId = @PlistaId,
                                                         comprado = @Pcomprado   
                                                         where id = @Pid";
                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _listaItemEntity.Id,
                        PprodutoId = _listaItemEntity.produtoId,
                        PlistaId = _listaItemEntity.listaId,
                        Pcomprado = _listaItemEntity.Comprado,
                    });

                    return _listaItemEntity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ListaItemRepository][Update] Erro ao tentar atualizar item. " + ex);
                return new ListaItemEntity();
            }
        }
        public virtual async Task<bool> Delete(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = $"delete from listaItens where id = '{id}'";
                    await cnx.ExecuteAsync(sqlQuery);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ListaItemRepository][Delete] Erro ao tentar excluir item. " + ex);
                    return false;
                }
            }
        }
        public virtual async Task<ListaItemEntity> Get(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = $@"select id, produtoId, listaId, comprado,
                                                dataCriacao, dataAtualizacao from listaItens 
                                                where id = '{id}'";
                    return await cnx.QueryFirstOrDefaultAsync<ListaItemEntity>(sqlQuery);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ListaItemRepository][Get] Erro ao tentar consultar item. " + ex);
                    return new ListaItemEntity();
                }
            }
        }
        public virtual async Task<ListaItemEntity> GetListItemByListID(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {

                    string sqlQuery = $@"select id, produtoId, listaId, comprado,
                                                dataCriacao,dataAtualizacao from listaItens 
                                                where listaId = '{id}'";
                    return await cnx.QueryFirstOrDefaultAsync<ListaItemEntity>(sqlQuery);
            }
        }
        public virtual async Task<ListaItemEntity> GetListItemByProductID(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {

                string sqlQuery = $@"select id, produtoId, listaId, comprado,
                                                dataCriacao,dataAtualizacao from listaItens 
                                                where produtoId = '{id}'";
                return await cnx.QueryFirstOrDefaultAsync<ListaItemEntity>(sqlQuery);
            }
        }
        public virtual async Task<List<ListaItemViewEntity>> GetListItemViewByListID(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {

                string sqlQuery = $@"select produtos.tituloProduto, produtos.descricao, listaItens.comprado 
                                                from listaItens inner join produtos on produtos.id = listaItens.produtoId
                                                where listaitens.listaId = '{id}'";
                var query = await cnx.QueryAsync<ListaItemViewEntity>(sqlQuery);
                return query.AsList();
            }
        }
    }
}
