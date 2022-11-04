using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Alura.Estacionamento.Tests
{
    // comando para executar no modo texto: dotnet test

    public class PatioTests : IEnumerable<object[]>, IDisposable
    //IEnumerable para usar ClassData
    {
        private Veiculo veiculo;
        public ITestOutputHelper output;

        //setup = preparação do cenário
        public PatioTests(ITestOutputHelper _output) : base()
        {
            output = _output;
            output.WriteLine("Construtor invocado.");
            veiculo = new Veiculo();
        }

        [Fact]
        public void ValidarFaturamentoDoEstacionamentoComUmVeiculo()
        {
            //Arrange
            var estacionamento = new Patio();

            //var veiculo = new Veiculo();
            veiculo.Proprietario = "Fulano de Tal";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "asd-9999";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        //Theory para testar com mais de um veículo e passar parâmetros
        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        [InlineData("José Silva", "POL-9242", "Cinza", "Fusca")]
        [InlineData("Maria Silva", "GRD-6524", "Azul", "Opala")]
        public void ValidarFaturamentoDoEstacionamentoComVariosVeiculos(
            string proprietario,
            string placa,
            string cor,
            string modelo)
        {
            //Arrange
            var estacionamento = new Patio();

            //var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Tipo = TipoVeiculo.Automovel;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        public ITestOutputHelper GetOutput()
        {
            return output;
        }

        //[Theory]
        //[ClassData(typeof(PatioTests))] //parou de funcionar, após add construtor com parâmetro
        //public void ValidarFaturamentoComVariosVeiculosOutraForma(
        //    Veiculo modelo)
        //{
        //    //Arrange
        //    var estacionamento = new Patio();

        //    //var veiculo = new Veiculo();
        //    veiculo.Proprietario = modelo.Proprietario;
        //    veiculo.Placa = modelo.Placa;
        //    veiculo.Cor = modelo.Cor;
        //    veiculo.Modelo = modelo.Modelo;
        //    veiculo.Tipo = TipoVeiculo.Automovel;

        //    estacionamento.RegistrarEntradaVeiculo(veiculo);
        //    estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

        //    //Act
        //    double faturamento = estacionamento.TotalFaturado();

        //    //Assert
        //    Assert.Equal(2, faturamento);
        //}

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Veiculo
                {
                    Proprietario = "André Silva 1",
                    Placa = "ASD-1111",
                    Cor="Verde",
                    Modelo="Fusca"
                }
            };

            yield return new object[]
            {
                new Veiculo
                {
                    Proprietario = "André Silva 2",
                    Placa = "ASD-2222",
                    Cor="Azul",
                    Modelo="Corsa"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        public void LocalizarVeiculoNoPatioComBaseNaPlaca(string proprietario,
            string placa,
            string cor,
            string modelo)
        {
            //Arrange
            var estacionamento = new Patio();

            //var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Placa = placa;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Tipo = TipoVeiculo.Automovel;

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisarVeiculo(placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);
        }

        [Fact]
        public void AlterarDadosVeiculoDoProprioVeiculo()
        {
            //Arrange
            var estacionamento = new Patio();

            //var veiculo = new Veiculo();
            veiculo.Proprietario = "José Silva";
            veiculo.Placa = "ZXC-8524";
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Opala";
            
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "José Silva";
            veiculoAlterado.Placa = "ZXC-8524";
            veiculoAlterado.Cor = "Preto"; //Alterado
            veiculoAlterado.Modelo = "Opala";

            //Act
            var alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);
        }

        public void Dispose()
        {
            output.WriteLine("Dispose invocado");
        }
    }
}
