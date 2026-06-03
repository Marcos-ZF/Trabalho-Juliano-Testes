using System;
using System.Linq;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.DTO;

namespace umfgcloud.aplicacao.service.testes.Classes
{
    [TestClass]
    public sealed class ProdutoServicoTestes : AbstractServicoTestes
    {
        private const string C_OWNER = "Marcos Zaro";
        private const string C_CATEGORY = "produto";

        // ─────────────────────────────────────────────────────────────
        // Instanciar
        // ─────────────────────────────────────────────────────────────

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public void ProdutoServico_Instanciar_Falha()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                // Act & Assert
                Assert.ThrowsException<InvalidDataException>(() => GetProdutoServicoInvalidJWT(context));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public void ProdutoServico_Instanciar_Sucesso()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                // Act
                var servico = GetProdutoServicoValidJWT(context);

                // Assert
                Assert.IsNotNull(servico);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────
        // AdicionarAsync
        // ─────────────────────────────────────────────────────────────

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_Sucesso()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO TESTE",
                    EAN = "7891234560001",
                    ValorCompra = 10.00m,
                    ValorVenda = 19.90m,
                };

                // Act
                await servico.AdicionarAsync(dto);
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();

                // Assert
                Assert.IsNotNull(produto);
                Assert.AreNotEqual(Guid.Empty, produto.Id);
                Assert.AreEqual("PRODUTO TESTE", produto.Descricao);
                Assert.AreEqual("7891234560001", produto.EAN);
                Assert.AreEqual(10.00m, produto.ValorCompra);
                Assert.AreEqual(19.90m, produto.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorCompraNegativo()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO TESTE",
                    EAN = "7891234560001",
                    ValorCompra = -10.00m,
                    ValorVenda = 19.90m,
                };

                // Act & Assert
                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorVendaNegativo()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO TESTE",
                    EAN = "7891234560001",
                    ValorCompra = 10.00m,
                    ValorVenda = -19.90m,
                };

                // Act & Assert
                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorCompraZero()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO TESTE",
                    EAN = "7891234560001",
                    ValorCompra = 0m,
                    ValorVenda = 19.90m,
                };

                // Act & Assert
                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorVendaZero()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO TESTE",
                    EAN = "7891234560001",
                    ValorCompra = 10.00m,
                    ValorVenda = 0m,
                };

                // Act & Assert
                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────
        // ObterTodosAsync
        // ─────────────────────────────────────────────────────────────

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterTodosAsync_SucessoListaVazia()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                // Act
                var produtos = await servico.ObterTodosAsync();

                // Assert
                Assert.IsNotNull(produtos);
                Assert.AreEqual(0, produtos.Count());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterTodosAsync_SucessoComRegistros()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO A",
                    EAN = "1111111111111",
                    ValorCompra = 5.00m,
                    ValorVenda = 10.00m,
                });
                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO B",
                    EAN = "2222222222222",
                    ValorCompra = 15.00m,
                    ValorVenda = 29.90m,
                });

                // Act
                var produtos = await servico.ObterTodosAsync();

                // Assert
                Assert.IsNotNull(produtos);
                Assert.AreEqual(2, produtos.Count());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────
        // ObterPorIdAsync
        // ─────────────────────────────────────────────────────────────

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterPorIdAsync_Sucesso()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO ID TESTE",
                    EAN = "9999999999999",
                    ValorCompra = 20.00m,
                    ValorVenda = 39.90m,
                };

                await servico.AdicionarAsync(dto);
                var produtoAdicionado = (await servico.ObterTodosAsync()).First();

                // Act
                var produto = await servico.ObterPorIdAsync(produtoAdicionado.Id);

                // Assert
                Assert.IsNotNull(produto);
                Assert.AreEqual(produtoAdicionado.Id, produto.Id);
                Assert.AreEqual("PRODUTO ID TESTE", produto.Descricao);
                Assert.AreEqual("9999999999999", produto.EAN);
                Assert.AreEqual(20.00m, produto.ValorCompra);
                Assert.AreEqual(39.90m, produto.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterPorIdAsync_FalhaIdInexistente()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var idInexistente = Guid.NewGuid();

                // Act & Assert
                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.ObterPorIdAsync(idInexistente));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────
        // AtualizarAsync
        // ─────────────────────────────────────────────────────────────

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_Sucesso()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO ORIGINAL",
                    EAN = "0000000000001",
                    ValorCompra = 10.00m,
                    ValorVenda = 20.00m,
                });

                var produtoAdicionado = (await servico.ObterTodosAsync()).First();

                var dtoAtualizado = new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = produtoAdicionado.Id,
                    Descricao = "PRODUTO ATUALIZADO",
                    EAN = "0000000000002",
                    ValorCompra = 15.00m,
                    ValorVenda = 30.00m,
                };

                // Act
                await servico.AtualizarAsync(dtoAtualizado);
                var produtoAtualizado = await servico.ObterPorIdAsync(produtoAdicionado.Id);

                // Assert
                Assert.IsNotNull(produtoAtualizado);
                Assert.AreEqual(produtoAdicionado.Id, produtoAtualizado.Id);
                Assert.AreEqual("PRODUTO ATUALIZADO", produtoAtualizado.Descricao);
                Assert.AreEqual("0000000000002", produtoAtualizado.EAN);
                Assert.AreEqual(15.00m, produtoAtualizado.ValorCompra);
                Assert.AreEqual(30.00m, produtoAtualizado.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaValorCompraNegativo()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO ORIGINAL",
                    EAN = "0000000000001",
                    ValorCompra = 10.00m,
                    ValorVenda = 20.00m,
                });

                var produtoAdicionado = (await servico.ObterTodosAsync()).First();

                var dtoAtualizado = new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = produtoAdicionado.Id,
                    Descricao = "PRODUTO ATUALIZADO",
                    EAN = "0000000000002",
                    ValorCompra = -15.00m,
                    ValorVenda = 30.00m,
                };

                // Act & Assert
                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AtualizarAsync(dtoAtualizado));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaValorVendaNegativo()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO ORIGINAL",
                    EAN = "0000000000001",
                    ValorCompra = 10.00m,
                    ValorVenda = 20.00m,
                });

                var produtoAdicionado = (await servico.ObterTodosAsync()).First();

                var dtoAtualizado = new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = produtoAdicionado.Id,
                    Descricao = "PRODUTO ATUALIZADO",
                    EAN = "0000000000002",
                    ValorCompra = 15.00m,
                    ValorVenda = -30.00m,
                };

                // Act & Assert
                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AtualizarAsync(dtoAtualizado));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        // ─────────────────────────────────────────────────────────────
        // RemoverAsync
        // ─────────────────────────────────────────────────────────────

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_RemoverAsync_Sucesso()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "PRODUTO PARA REMOVER",
                    EAN = "5555555555555",
                    ValorCompra = 8.00m,
                    ValorVenda = 16.00m,
                });

                var produtoAdicionado = (await servico.ObterTodosAsync()).First();

                // Act
                await servico.RemoverAsync(produtoAdicionado.Id);
                var produtos = await servico.ObterTodosAsync();

                // Assert
                Assert.AreEqual(0, produtos.Count());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_RemoverAsync_FalhaIdInexistente()
        {
            try
            {
                // Arrange
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var idInexistente = Guid.NewGuid();

                // Act & Assert
                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.RemoverAsync(idInexistente));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
