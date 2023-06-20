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
    internal class ContaCorrenteComando
    {
        IContaCorrenteRepositorio _repositorio;
        IContaCorrenteServico _servico;
        ContaCorrenteServicoApp contaCorrenteServicoApp;
        public ContaCorrenteComando()
        {
            _repositorio = new ContaCorrenteRepositorio();
            _servico = new ContaCorrenteServico(_repositorio);
            contaCorrenteServicoApp = new ContaCorrenteServicoApp(_servico);
        }

        public bool Adicionar(ContaCorrenteDTO conta)
        {
            return contaCorrenteServicoApp.Adicionar(conta);
        }
        public bool Atualizar(int id, ContaCorrenteDTO conta)
        {
            return contaCorrenteServicoApp.Atualizar(id,conta);
        }

        public bool Excluir(int id)
        {
            return contaCorrenteServicoApp.Excluir(id);
        }

        public ContaCorrenteDTO ObterPorId(int id)
        {
            return contaCorrenteServicoApp.ObterPorId(id);
        }

        public List<ContaCorrenteDTO> ObterTodos()
        {
           return contaCorrenteServicoApp.ObterTodos();
        }

    }
}
