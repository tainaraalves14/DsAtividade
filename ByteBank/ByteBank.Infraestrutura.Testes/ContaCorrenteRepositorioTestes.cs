using ByteBank.Dados.Repositorio;
using ByteBank.Dominio.Entidades;
using ByteBank.Dominio.Interfaces.Repositorios;
using ByteBank.Infraestrutura.Testes.DTO;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ByteBank.Infraestrutura.Testes
{
    public class ContaCorrenteRepositorioTestes
    {
        private readonly IContaCorrenteRepositorio _repositorio;

        public ContaCorrenteRepositorioTestes()
        {
            //Injetando dependências no construtor;
            var servico = new ServiceCollection();
            servico.AddTransient<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IContaCorrenteRepositorio>();

        }
        [Fact]
        public void TestaObterTodasContasCorrentes()
        {
            //Arrange
            //Act
            List<ContaCorrente> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);
        }

        [Fact]
        public void TestaObterContaCorrentePorId()
        {
            //Arrange
            //Act
            var conta = _repositorio.ObterPorId(1);

            //Assert
            Assert.NotNull(conta);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterContasCorrentesPorVariosId(int id)
        {
            //Arrange
            //Act
            var conta = _repositorio.ObterPorId(id);

            //Assert
            Assert.NotNull(conta);
        }


        [Fact]
        public void TestaAtualizaSaldoDeterminadaConta()
        {
            //Arrange

            var conta = _repositorio.ObterPorId(1);
            double saldoNovo = 15;
            conta.Saldo = saldoNovo;

            //Act
            var atualizado = _repositorio.Atualizar(1, conta);

            //Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaInsereUmaNovaContaCorrenteNoBancoDeDados()
        {
            //Arrange           
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Kent Nelson",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Bancário",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Id = 1,
                    Endereco = "Rua das Flores,25",
                    Numero = 147
                }
            };

            //Act
            var retorno = _repositorio.Adicionar(conta);

            //Assert
            Assert.True(retorno);

        }

        // Testes com Mock
        [Fact]
        public void TestaObterContasMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarContasCorrentes();

            //Assert - Verificando o comportamento
            bytebankRepositorioMock.Verify(b => b.BuscarContasCorrentes());
        }

        [Fact]
        public void TestaConsultaPixMock()
        {
            //Arange
            var pixRepositorioMock = new Mock<IPixRepositorio>();
            var mock = pixRepositorioMock.Object;

            //Act
            var lista = mock.consultaPix(new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a"));

            //Assert - Verificando o comportamento
            pixRepositorioMock.Verify(b => b.consultaPix(new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a")));
        }

        [Fact]
        public void TestaConsultaTodosPixStub()
        {

            //Arange
            var guid = new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a");
            var pix = new PixDTO() { Chave = guid, Saldo = 10 };

            var pixRepositorioMock = new Mock<IPixRepositorio>();
            pixRepositorioMock.Setup(x => x.consultaPix(It.IsAny<Guid>())).Returns(pix);

            var mock = pixRepositorioMock.Object;

            //Act
            var saldo = mock.consultaPix(guid).Saldo;

            //Assert
            Assert.Equal(10, saldo);
        }

        [Fact]
        public void TestaConsultaPix()
        {

            //Arange
            var guid = new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a");
            var pix = new PixDTO() { Chave = guid, Saldo = 10 };

            var pixRepositorioMock = new Mock<IPixRepositorio>();
            pixRepositorioMock.Setup(x => x.consultaPix(It.IsAny<Guid>())).Returns(pix);

            var mock = pixRepositorioMock.Object;

            //Act
            var saldo = mock.consultaPix(guid).Saldo;

            //Assert
            Assert.Equal(10, saldo);
        }


        // public void TestarAdicionarContaCorrente()
        [Fact]
        public void TestarAdicionarContaCorrente()
        {
            //Arrange           
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Kent Nelson",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Bancário",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Id = 1,
                    Endereco = "Rua das Flores,25",
                    Numero = 147
                }
            };

            var repositorioMock = new Mock<IContaCorrenteRepositorio>();

            repositorioMock.Setup(rep => rep.Adicionar(conta)).Returns(true).Verifiable();

            //Act
            var mock = repositorioMock.Object;
            var adicionado = mock.Adicionar(conta);

            //Assert
            Assert.True(adicionado);
        }

        //public void TestarAtualizarContaCorrente()
        [Fact]
        public void TestarAtualizarContaCorrente()
        {
            //Arrange           
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Kent Nelson",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Bancário",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Id = 1,
                    Endereco = "Rua das Flores,25",
                    Numero = 147
                }
            };

            var repositorioMock = new Mock<IContaCorrenteRepositorio>();

            repositorioMock.Setup(rep => rep.Atualizar(conta)).Returns(true).Verifiable();

            //Act
            var mock = repositorioMock.Object;
            var atualizado = mock.Atualizar(conta);

            //Assert
            Assert.True(atualizado);
        }

        //public void TestarExclusaoContaCorrente()
        [Fact]
        public void TestarExcluirContaCorrente()
        {
            //Arrange           
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Kent Nelson",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Bancário",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Id = 1,
                    Endereco = "Rua das Flores,25",
                    Numero = 147
                }
            };

            var repositorioMock = new Mock<IContaCorrenteRepositorio>();

            repositorioMock.Setup(rep => rep.Excluir(conta)).Returns(true).Verifiable();

            //Act
            var mock = repositorioMock.Object;
            var excluido = mock.Excluir(conta);

            //Assert
            Assert.True(excluido);
        }

        //public void ObterContaCorrenteID()
        [Fact]
        public void TestarObterContaCorrenteID()
        {
            //Arrange           
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Kent Nelson",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Bancário",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Id = 1,
                    Endereco = "Rua das Flores,25",
                    Numero = 147
                }
            };

            var repositorioMock = new Mock<IContaCorrenteRepositorio>();

            repositorioMock.Setup(rep => rep.ObterPorId(conta)).Returns(true).Verifiable();

            //Act
            var mock = repositorioMock.Object;
            var obtendo = mock.ObterPorId(conta);

            //Assert
            Assert.True(obtendo);
        } 

    }
}
