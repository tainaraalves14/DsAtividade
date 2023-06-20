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
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _repositorio;
        public ClienteRepositorioTestes()
        {
            //Injetando dependências no construtor;
            var servico = new ServiceCollection();
            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IClienteRepositorio>();

        }

        [Fact]
        public void TestaObterTodosClientes()
        {
            //Arrange
            //Act
            List<Cliente> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);

        }

        [Fact]
        public void TestaObterClientesPorId()
        {
            //Arrange
            //Act
            var cliente = _repositorio.ObterPorId(1);

            //Assert
            Assert.NotNull(cliente);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterClientesPorVariosId(int id)
        {
            //Arrange           
            //Act
            var cliente = _repositorio.ObterPorId(id);

            //Assert
            Assert.NotNull(cliente);

        }

        [Fact]
        public void TesteInsereUmNovoClienteNaBaseDeDados()
        {
            //Arrange            
            string nome = "Alberto Roberto";
            string cpf = "088.157.930-03";
            Guid identificador = Guid.NewGuid();
            string profissao = "Administrador de Empresas";

            var cliente = new Cliente()
            {
                Nome = nome,
                CPF = cpf,
                Identificador = identificador,
                Profissao = profissao
            };

            //Act
            var retorno = _repositorio.Adicionar(cliente);

            //Assert
            Assert.True(retorno);


        }

        [Fact]
        public void TestaAtualizacaoInformacaoDeterminadoCliente()
        {
            //Arrange        
            var cliente = _repositorio.ObterPorId(2);
            var nomeNovo = "João Pedro";
            cliente.Nome = nomeNovo;

            //Act
            var atualizado = _repositorio.Atualizar(2, cliente);

            //Assert
            Assert.True(atualizado);
        }

        // Testes com Mock
        [Fact]
        public void TestaObterClientesMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarClientes();

            //Assert
            bytebankRepositorioMock.Verify(b => b.BuscarClientes());
        }


    }
}
