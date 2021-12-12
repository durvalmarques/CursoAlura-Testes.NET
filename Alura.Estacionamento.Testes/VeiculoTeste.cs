using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

//Arrange: � a prepara��o do ambiente, do cen�rio que eu preciso para evocar o m�todo que eu quero testar.
//Por exemplo, para eu testar o acelerar, eu preciso ter um ve�culo, ent�o eu preciso instanciar um objeto do tipo ve�culo ou inicializar algumas vari�veis. Essa prepara��o do cen�rio fica no arrange.
//Act: � o m�todo que eu quero necessariamente testar, no caso, o acelerar ou o frear.
//Assert: � a verifica��o do resultado obtido da execu��o daquele m�todo, testado e executado.

namespace Alura.Estacionamento.Testes
{
  public class VeiculoTeste : IDisposable
  {
    private Veiculo veiculo;
    public ITestOutputHelper SaidaConsoleTeste;

    public VeiculoTeste(ITestOutputHelper _saidaConsoleTeste)
    {
      SaidaConsoleTeste = _saidaConsoleTeste;
      SaidaConsoleTeste.WriteLine("Construtor invocado.");
      veiculo = new Veiculo();
    }

    [Fact]
    public void TestaVeiculoAcelerarComParamtro10()
    {
      //Arrange:
      //var veiculo = new Veiculo();
      //Act:
      veiculo.Acelerar(10);
      //Assert:
      Assert.Equal(100, veiculo.VelocidadeAtual);
    }

    [Fact]
    public void TestaVeiculoFrearComParamtro10()
    {
      //Arrange:
      //var veiculo = new Veiculo();
      //Act:
      veiculo.Frear(10);
      //Assert:
      Assert.Equal(-150, veiculo.VelocidadeAtual);
    }

    [Fact(Skip = "Teste ainda n�o implementado. Ignorar")]
    public void ValidaNomeProprietarioDoVeiculo()
    {

    }

    [Fact]
    public void FichaDeInformacaodoVeiculo()
    {
      //Arrange
      //var veiculo = new Veiculo();
      veiculo.Proprietario = "Durval Ferreira";
      veiculo.Tipo = TipoVeiculo.Automovel;
      veiculo.Placa = "ZAP-7434";
      veiculo.Cor = "Verde";
      veiculo.Modelo = "Variant";

      //Act
      string dados = veiculo.ToString();

      //Assert
      Assert.Contains("Ficha do Ve�culo:", dados);

    }

    [Fact]
    public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
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
    public void TestaMensagemDeExcecaoDoQuartoCaractereDaPlaca()
    {
      //Arrange
      string placa = "ASDF8888";

      //Act
      var mensagem = Assert.Throws<System.FormatException>(        
        () => new Veiculo().Placa = placa
        );

      //Assert
      Assert.Equal("O 4� caractere deve ser um h�fen", mensagem.Message);

    }

    public void Dispose()
    {
      SaidaConsoleTeste.WriteLine("Dispose invocado.");
    }
  }
}
