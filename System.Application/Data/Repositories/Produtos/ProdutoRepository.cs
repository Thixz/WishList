using Dapper;
using System;
using System.Application.Data.Entities.Produtos;
using System.Application.Data.MySql;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Data.Repositories.Produtos
{
    public class ProdutoRepository
    {
        private readonly MySqlContext sqlContext;
        public ProdutoRepository(MySqlContext _context)
        {
            this.sqlContext = _context;
        }

        public virtual async Task<ProdutoEntity> Create(ProdutoEntity _produtoEntity)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = @"insert into produtos (id, tituloProduto, descricao)
                                        values (@Pid, @PtituloProduto, @Pdescricao)";

                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _produtoEntity.Id,
                        PtituloProduto = _produtoEntity.tituloProduto,
                        Pdescricao = _produtoEntity.Descricao,
                    });

                    return _produtoEntity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ProdutoRepository][Create] Erro ao tentar criar produto. " + ex);
                    return new ProdutoEntity();
                }
            }
        }
        public virtual async Task<ProdutoEntity> Update(ProdutoEntity _produtoEntity)
        {
            try
            {
                using (var cnx = sqlContext.Conectar())
                {
                    string sqlQuery = @"update produtos set tituloProduto = @PtituloProduto,
                                                         descricao = @Pdescricao   
                                                         where id = @Pid";
                    await cnx.ExecuteAsync(sqlQuery, new
                    {
                        Pid = _produtoEntity.Id,
                        PtituloProduto = _produtoEntity.tituloProduto,
                        Pdescricao = _produtoEntity.Descricao,
                    });

                    return _produtoEntity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ProdutoRepository][Update] Erro ao tentar atualizar produto. " + ex);
                return new ProdutoEntity();
            }
        }
        public virtual async Task<bool> Delete(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = $"delete from produtos where id = '{id}'";
                    await cnx.ExecuteAsync(sqlQuery);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ProdutoRepository][Delete] Erro ao tentar excluir produto. " + ex);
                    return false;
                }
            }
        }
        public virtual async Task<ProdutoEntity> Get(Guid id)
        {
            using (var cnx = sqlContext.Conectar())
            {
                try
                {
                    string sqlQuery = $@"select id,tituloProduto,descricao,
                                                dataCriacao,dataAtualizacao from produtos 
                                                where id = '{id}'";
                    return await cnx.QueryFirstOrDefaultAsync<ProdutoEntity>(sqlQuery);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ProdutoRepository][Get] Erro ao tentar consultar produto. " + ex);
                    return new ProdutoEntity();
                }
            }
        }
    }
}
