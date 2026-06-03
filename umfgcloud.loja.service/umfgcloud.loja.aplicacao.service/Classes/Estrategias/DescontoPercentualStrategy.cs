using umfgcloud.loja.dominio.service.Interfaces.Estrategias;

namespace umfgcloud.loja.aplicacao.service.Classes.Estrategias
{
    /// <summary>
    /// Strategy Concreto: aplica um desconto percentual sobre o valor de venda.
    /// Exemplo: 10% de desconto sobre R$ 100,00 resulta em R$ 90,00.
    /// </summary>
    public sealed class DescontoPercentualStrategy : IDescontoStrategy
    {
        private readonly decimal _percentual;

        /// <param name="percentual">Percentual de desconto entre 0 e 100.</param>
        public DescontoPercentualStrategy(decimal percentual)
        {
            if (percentual < decimal.Zero || percentual > 100)
                throw new InvalidDataException($"Percentual de desconto informado {percentual} inválido. Deve ser entre 0 e 100.");

            _percentual = percentual;
        }

        public decimal Calcular(decimal valorVenda)
        {
            var desconto = valorVenda * (_percentual / 100);
            return Math.Round(valorVenda - desconto, 2);
        }
    }
}
