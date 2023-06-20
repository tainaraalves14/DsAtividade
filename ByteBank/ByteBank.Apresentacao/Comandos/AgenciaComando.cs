using ByteBank.Aplicacao.AplicacaoServico;
using ByteBank.Aplicacao.DTO;
using ByteBank.Dados.Repositorio;
using ByteBank.Dominio.Interfaces.Repositorios;
using ByteBank.Dominio.Interfaces.Servicos;
using ByteBank.Dominio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Apresentacao.Comandos
{
    internal class AgenciaComando
    {
        IAgenciaRepositorio _repositorio;
        IAgenciaServico _servico;
        AgenciaServicoApp agenciaServicoApp;
        public AgenciaComando()
        {
            _repositorio = new AgenciaRepositorio();
            _servico = new AgenciaServico(_repositorio);
            agenciaServicoApp = new AgenciaServicoApp(_servico);
        }

        public bool Adicionar(AgenciaDTO agencia)
        {
            return agenciaServicoApp.Adicionar(agencia);
        }
        public bool Atualizar(int id, AgenciaDTO agencia)
        {
            return agenciaServicoApp.Atualizar(id,agencia);
        }

        public bool Excluir(int id)
        {
            return agenciaServicoApp.Excluir(id);
        }

        public AgenciaDTO ObterPorId(int id)
        {
            return agenciaServicoApp.ObterPorId(id);
        }

        public List<AgenciaDTO> ObterTodos()
        {
           return agenciaServicoApp.ObterTodos();
        }

    }
}
