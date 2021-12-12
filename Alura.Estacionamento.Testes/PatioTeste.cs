using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
  public class PatioTeste : IDisposable
  {
    private Veiculo veiculo;
    private Operador operador;
    public ITestOutputHelper SaidaConsoleTeste;

    public PatioTeste(ITestOutputHelper _saidaConsoleTeste)
    {
      SaidaConsoleTeste = _saidaConsoleTeste;
      SaidaConsoleTeste.WriteLine("Construtor invocado.");      
      veiculo = new Veiculo();
      
      operador = new Operador();
      operador.Nome = "Jurema Claras";
    }

    [Fact]
    public void ValidaFaturamentoDoEstacionamentoComVeiculo()
    {
      //Arrange:
      var estacionamento = new Patio();
      estacionamento.OperadorPatio = operador;
      var veiculo = new Veiculo();
      veiculo.Proprietario = "Durval Ferreira";
      veiculo.Tipo = TipoVeiculo.Automovel;
      veiculo.Cor = "Verde";
      veiculo.Modelo = "Fusca";
      veiculo.Placa = "ABC-1234";

      estacionamento.RegistrarEntradaVeiculo(veiculo);
      estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

      //Act
      double faturamento = estacionamento.TotalFaturado();

      //Assert
      Assert.Equal(2, faturamento);
    }

    [Theory]
    [InlineData("André Silva", "ASD-1498", "Preto", "SW4" )]
    [InlineData("José Ramos", "QWE-3568", "Cinza", "Strada" )]
    [InlineData("Pedro Santos", "RTY-9898", "Branco", "718" )]
    [InlineData("Chico Novaes", "HTY-9898", "Azul", "RS7" )]
    public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
    {
      //Arrange
      var estacionamento = new Patio();
      //Operador operador = new Operador();
      //operador.Nome = "Jurema Clara";
      estacionamento.OperadorPatio = operador;

      //var veiculo = new Veiculo();
      veiculo.Proprietario = proprietario;
      veiculo.Tipo = TipoVeiculo.Automovel;
      veiculo.Placa = placa;
      veiculo.Cor = cor;
      veiculo.Modelo = modelo;
      estacionamento.RegistrarEntradaVeiculo(veiculo);
      estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

      //Act
      double faturamento = estacionamento.TotalFaturado();

      //Assert
      Assert.Equal(2, faturamento);

    }

    [Theory]
    [InlineData("André Silva", "ASD-1498", "Preto", "SW4")]
    public void LocalizaVeiculoNoPatioComBaseNoIdTicket(string proprietario, string placa, string cor, string modelo)
    {
      //Arrange
      Patio estacionamento = new Patio();
      estacionamento.OperadorPatio = operador;
      //var veiculo = new Veiculo();
      veiculo.Proprietario = proprietario;
      veiculo.Tipo = TipoVeiculo.Automovel;
      veiculo.Placa = placa;
      veiculo.Cor = cor;
      veiculo.Modelo = modelo;
      estacionamento.RegistrarEntradaVeiculo(veiculo);

      //Act
      var consultado = estacionamento.PesquisaVeiculo(veiculo.IdTicket);

      //Assert
      Assert.Contains("### Ticket Estacionamento Alura ###", consultado.Ticket);

    }

    [Fact]
    public void AlterarDadosDoProprioVeiculo() {
      //Arrange:
      var estacionamento = new Patio();
      estacionamento.OperadorPatio = operador;
      //var veiculo = new Veiculo();
      veiculo.Proprietario = "Durval Ferreira";
      veiculo.Placa = "ABC-1234";
      veiculo.Cor = "Verde";
      veiculo.Modelo = "Fusca";
      estacionamento.RegistrarEntradaVeiculo(veiculo);


      var veiculoAlterado = new Veiculo();
      veiculoAlterado.Proprietario = "Durval Ferreira";
      veiculoAlterado.Placa = "ABC-1234";
      veiculoAlterado.Cor = "Verde";
      veiculoAlterado.Modelo = "Fusca";


      //Act
      Veiculo alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

      //Assert
      Assert.Equal(alterado.Cor, veiculoAlterado.Cor);

    }
    public void Dispose()
    {
      SaidaConsoleTeste.WriteLine("Dispose invocado.");
    }
  }
}
