using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Tests
{
    //Padr�o de teste - AAA
    //Arrange (instanciar, prepara��o do cen�rio)
    //Act (m�todo que quer testar)
    //Assert (verifica��o do resultado)

    public class VeiculoTests
    {
        private Veiculo veiculo;
        public ITestOutputHelper output;

        //setup = prepara��o do cen�rio
        public VeiculoTests(
            ITestOutputHelper _output)
        {
            output = _output;
            output.WriteLine("Construtor invocado.");
            veiculo = new Veiculo();
        }

        [Fact(DisplayName = "Teste n�1")]
        [Trait("Funcionalidade", "Acelerar")]
        public void TestarVeiculoAcelerarComParametro10()
        {
            //Arrange
            //var veiculo = new Veiculo();
            //Act
            veiculo.Acelerar(10);
            //Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact(DisplayName = "Teste n�2")]
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

        [Fact(DisplayName = "Teste n�3", Skip = "Teste ainda n�o implementado. Ignorar.")]
        public void ValidarNomeProprietario()
        {

        }

        [Fact]
        public void ImprimirFichaVeiculo()
        {
            //Arrange
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "Fulano de Tal";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = "ZAP-7419";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";

            //Act
            string dados = veiculo.ToString();

            //Assert
            Assert.Contains("Ficha do ve�culo:", dados);
        }

        [Fact]
        public void TestarNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            //Arrange
            string nomeProprietario = "Ab";

            //Assert
            Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo(nomeProprietario) 
            );
        }

        [Fact]
        public void TestarMensagemDeExcecaoDoQuartoCaractereDaPlaca()
        {
            //Arrange
            string placa = "ASDF8888";

            //Act
            var mensagem = Assert.Throws<FormatException>(
                () => new Veiculo().Placa = placa    
            );

            //Assert
            Assert.Equal("O 4� caractere deve ser um h�fen", mensagem.Message);
        }
    }
}
