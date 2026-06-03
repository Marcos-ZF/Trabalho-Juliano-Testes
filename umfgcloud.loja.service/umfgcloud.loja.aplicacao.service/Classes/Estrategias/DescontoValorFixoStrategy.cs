using umfgcloud.loja.dominio.service.Interfaces.Estrategias;

namespace umfgcloud.loja.aplicacao.service.Classes.Estrategias
{
    /// <summary>
    /// Strategy Concreto: aplica um desconto de valor fixo sobre o valor de venda.
    /// Exemplo: R$ 5,00 de desconto sobre R$ 30,00 resulta em R$ 25,00.
    /// </summary>
    public sealed class DescontoValorFixoStrategy : IDescontoStrategy
    {
        private readonly decimal _valorDesconto;

        /// <param name="valorDesconto">Valor fixo de desconto. Deve ser maior que zero.</param>
        public DescontoValorFixoStrategy(decimal valorDesconto)
        {
            if (valorDesconto <= decimal.Zero)
                throw new InvalidDataException($"Valor de desconto informado {valorDesconto} inválido. Deve ser maior que zero.");

            _valorDesconto = valorDesconto;
        }

        public decimal Calcular(decimal valorVenda)
        {
            var resultado = valorVenda - _valorDesconto;

            if (resultado <= decimal.Zero)
                throw new InvalidDataException("O desconto não pode ser maior ou igual ao valor de venda.");

            return Math.Round(resultado, 2);
        }
    }
}
