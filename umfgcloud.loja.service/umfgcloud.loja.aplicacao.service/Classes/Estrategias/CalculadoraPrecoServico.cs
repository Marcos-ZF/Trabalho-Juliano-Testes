using umfgcloud.loja.dominio.service.Interfaces.Estrategias;

namespace umfgcloud.loja.aplicacao.service.Classes.Estrategias
{
    /// <summary>
    /// Contexto do padrão Strategy.
    /// Mantém uma referência para uma IDescontoStrategy e delega o cálculo a ela.
    /// A estratégia pode ser trocada em tempo de execução via SetStrategy.
    /// </summary>
    public sealed class CalculadoraPrecoServico
    {
        private IDescontoStrategy _strategy;

        /// <param name="strategy">Estratégia inicial de desconto.</param>
        public CalculadoraPrecoServico(IDescontoStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        /// <summary>
        /// Permite trocar a estratégia de desconto em tempo de execução.
        /// </summary>
        public void SetStrategy(IDescontoStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        /// <summary>
        /// Calcula o valor de venda final aplicando a estratégia configurada.
        /// </summary>
        public decimal CalcularPrecoFinal(decimal valorVenda)
        {
            if (valorVenda <= decimal.Zero)
                throw new InvalidDataException($"Valor de venda informado {valorVenda} inválido.");

            return _strategy.Calcular(valorVenda);
        }
    }
}
