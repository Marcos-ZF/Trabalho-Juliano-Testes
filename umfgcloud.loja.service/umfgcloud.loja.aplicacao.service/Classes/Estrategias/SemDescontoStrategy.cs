using umfgcloud.loja.dominio.service.Interfaces.Estrategias;

namespace umfgcloud.loja.aplicacao.service.Classes.Estrategias
{
    /// <summary>
    /// Strategy Concreto: não aplica nenhum desconto.
    /// Retorna o valor de venda original.
    /// </summary>
    public sealed class SemDescontoStrategy : IDescontoStrategy
    {
        public decimal Calcular(decimal valorVenda)
            => valorVenda;
    }
}
