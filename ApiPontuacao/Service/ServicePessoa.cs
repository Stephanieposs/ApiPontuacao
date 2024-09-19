﻿using ApiPontuacao.Models;
using ApiPontuacao.Repository;

namespace ApiPontuacao.Service
{
    public class ServicePessoa
    {
        private readonly RepoPessoa _repo;
        public ServicePessoa(RepoPessoa repo)
        {
            _repo = repo;
        }

        public Pessoa Save(Pessoa pessoa)
        {
            ValidaDados(pessoa);
            return _repo.Save(pessoa);
        }

        public IEnumerable<Pessoa> GetAll()
        {

            return _repo.GetAll();
        }

        public Pessoa Get(int id)
        {
            var pessoaExistente = _repo.Get(id);
            if (pessoaExistente == null)
            {
                throw new Exception("Pessoa não existe");
            }

            
            _repo.IncrementarPontos(id, 1);

            return pessoaExistente;
        }

        public void Delete(int id)
        {

            if (id.Equals(null))
            {
                throw new Exception("O id deve ser informado");
            }
            Get(id);
            _repo.Delete(id);
        }

        public Pessoa Update(Pessoa pessoa)
        {
            ValidaDados(pessoa);
            Get(pessoa.Id);

            return _repo.Update(pessoa);
        }

        private void ValidaDados(Pessoa pessoa)
        {
            if (pessoa.Nome.Equals(""))
            {
                throw new Exception("O nome deve ser informado");
            }
            if (pessoa.Cpf.Equals(""))
            {
                throw new Exception("O cpf deve ser informado");
            }

        }

        public void AdicionarPontos(int id, int pontos)
        {
            var pessoaExistente = Get(id); // Certifique-se de que a pessoa existe
            if (pessoaExistente != null)
            {
                _repo.IncrementarPontos(id, pontos);
            }
            else
            {
                throw new Exception("Pessoa não encontrada.");
            }
        }
    }
}
