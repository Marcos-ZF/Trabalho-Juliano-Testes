namespace umfgcloud.loja.dominio.service.Interfaces.Estrategias
{
    /// <summary>
    /// Strategy: interface que define o contrato para os algoritmos de desconto.
    /// Cada implementação concreta representa uma estratégia diferente de desconto.
    /// </summary>
    public interface IDescontoStrategy
    {
        /// <summary>
        /// Calcula o valor de venda após a aplicação do desconto.
        /// </summary>
        /// <param name="valorVenda">Valor de venda original do produto.</param>
        /// <returns>Valor de venda com desconto aplicado.</returns>
        decimal Calcular(decimal valorVenda);
    }
}
