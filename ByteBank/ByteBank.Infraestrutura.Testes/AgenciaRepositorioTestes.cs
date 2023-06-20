﻿using ByteBank.Dados.Repositorio;
using ByteBank.Dominio.Entidades;
using ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repositorio;

        public AgenciaRepositorioTestes()
        {
            //Injetando dependências no construtor;
            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IAgenciaRepositorio>();

        }

        [Fact]
        public void TestaObterTodasAgencias()
        {
            //Arrange        

            //Act
            List<Agencia> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            //Arrange

            //Act
            var agencia = _repositorio.ObterPorId(1);

            //Assert
            Assert.NotNull(agencia);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterAgenciasPorVariosId(int id)
        {
            //Arrange            

            //Act
            var agencia = _repositorio.ObterPorId(id);

            //Assert
            Assert.NotNull(agencia);

        }

        [Fact]
        public void TesteInsereUmaNovaAgenciaNaBaseDeDados()
        {
            //Arrange            
            string nome = "Agencia Guarapari";
            int numero = 125982;
            Guid identificador = Guid.NewGuid();
            string endereco = "Rua: 7 de Setembro - Centro";

            var agencia = new Agencia()
            {
                Nome = nome,
                Identificador = identificador,
                Endereco = endereco,
                Numero = numero
            };

            //Act
            var retorno = _repositorio.Adicionar(agencia);

            //Assert
            Assert.True(retorno);
        }
        [Fact]
        public void TestaAtualizacaoInformacaoDeterminadaAgencia()
        {
            //Arrange      
            var agencia = _repositorio.ObterPorId(2);
            var nomeNovo = "Agencia Nova";
            agencia.Nome = nomeNovo;

            //Act
            var atualizado = _repositorio.Atualizar(2, agencia);

            //Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaRemoverInformacaoDeterminadaAgencia()
        {
            //Arrange
            //Act
            var atualizado = _repositorio.Excluir(3);

            //Assert
            Assert.True(atualizado);
        }


        //Exceções
        [Fact]
        public void TestaExcecaoConsultaPorAgenciaPorId()
        {

            //Act     
            //Assert
            Assert.Throws<Exception>(
                () => _repositorio.ObterPorId(33)
             );

        }


        // Testes com Mock
        [Fact]
        public void TestaObterAgenciasMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarAgencias();

            //Assert
            bytebankRepositorioMock.Verify(b => b.BuscarAgencias());
        }

        [Fact]
        public void TestaAdiconarAgenciaMock()
        {
            // Arrange
            var agencia = new Agencia()
            {
                Nome = "Agência Amaral",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Arthur Costa",
                Numero = 6497
            };

            var repositorioMock = new ByteBankRepositorio();

            //Act
            var adicionado = repositorioMock.AdicionarAgencia(agencia);

            //Assert
            Assert.True(adicionado);
        }

        [Fact]
        public void TestaAdiconarAgenciaMockII()
        {
            // Arrange
            var agencia = new Agencia()
            {
                Nome = "Agência Amaral Gurgel",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Arthur Costa",
                Numero = 6497
            };

            var repositorioMock = new Mock<IAgenciaRepositorio>();

            repositorioMock.Setup(rep => rep.Adicionar(agencia)).Returns(true).Verifiable();

            //Act
            var mock = repositorioMock.Object;
            var adicionado = mock.Adicionar(agencia);

            //Assert
            Assert.True(adicionado);
        }

    }
}
