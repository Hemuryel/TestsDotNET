using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Alura.Estacionamento.Tests
{
    //Padrão de teste - AAA
    //Arrange (instanciar, preparação do cenário)
    //Act (método que quer testar)
    //Assert (verificação do resultado)

    public class VeiculoTests
    {
        [Fact(DisplayName = "Teste nº1")]
        [Trait("Funcionalidade", "Acelerar")]
        public void TestarVeiculoAcelerarComParametro10()
        {
            //Arrange
            var veiculo = new Veiculo();
            //Act
            veiculo.Acelerar(10);
            //Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact(DisplayName = "Teste nº2")]
        [Trait("Funcionalidade", "Frear")]
        public void TestarVeiculoFrearComParametro10()
        {
            var veiculo = new Veiculo();
            veiculo.Frear(10);
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestarTipoVeiculo()
        {
            //Assert
            var veiculo = new Veiculo("Fulano");
            //Act
            //Assert
            Assert.Equal(TipoVeiculo.Automovel, veiculo.Tipo);
        }

        [Fact(DisplayName = "Teste nº3", Skip = "Teste ainda não implementado. Ignorar.")]
        public void ValidarNomeProprietario()
        {

        }

        [Fact]
        public void ImprimirFichaVeiculo()
        {
            //Arrange
            var veiculo = new Veiculo();
            veiculo.Proprietario = "Fulano de Tal";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = "ZAP-7419";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";

            //Act
            string dados = veiculo.ToString();

            //Assert
            Assert.Contains("Ficha do veículo:", dados);
        }
    }
}
