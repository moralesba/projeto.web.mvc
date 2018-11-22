using System;
using System.Collections.Generic;
using System.IO;
using Senai.Financas.Web.Mvc.Models;
using Senai_Financas_Web_Mvc_Manha_master.Interfaces;

namespace Senai_Financas_Web_Mvc_Manha_master.Repositorio {
    public class UsuarioRepositorio : IUsuario {
        public UsuarioModel Cadastrar (UsuarioModel usuario) {
            if (File.Exists ("usuario.csv")) {
                usuario.Id = File.ReadAllLines ("usuario.csv").Length + 1;
            } else {
                usuario.Id = 1;
            }

            using (StreamWriter sw = new StreamWriter ("usuarios.csv", true)) {
                sw.WriteLine ($"{usuario.Id};{usuario.Nome};{usuario.Email};{usuario.Senha};{usuario.DataNascimento}");
            }

            return usuario;
        }

        public UsuarioModel Editar (UsuarioModel usuario) {
            string[] linhas = System.IO.File.ReadAllLines ("usuario.csv");

            for (int i = 0; i < linhas.Length; i++) {
                if (string.IsNullOrEmpty (linhas[i])) {
                    continue;
                }
                string[] dados = linhas[i].Split (';');

                if (usuario.Id.ToString () == dados[0]) {
                    linhas[i] = $"{usuario.Id}, {usuario.Nome}, {usuario.Email}, {usuario.Senha}, {usuario.DataNascimento}";
                    break;
                }
            }
            System.IO.File.WriteAllLines ("usuario.csv", linhas);
            return usuario;
        }

        public void Excluir (int id) {
            string[] linhas = System.IO.File.ReadAllLines ("usuarios.csv");

            //Percorre as linhas do arquivo
            for (int i = 0; i < linhas.Length; i++) {
                //Separa as colunas da linha
                string[] linha = linhas[i].Split (';');

                //Verifica se o id da linha é o id passado
                if (id.ToString () == linha[0]) {
                    //Defino a linha como vazia
                    linhas[i] = "";
                    break;
                }
            }
            //Armazeno no arquivo csv todas as linhas
            System.IO.File.WriteAllLines ("usuarios.csv", linhas);
        }

        public List<UsuarioModel> Listar () {
            List<UsuarioModel> lsUsuarios = new List<UsuarioModel> ();

            string[] linhas = System.IO.File.ReadAllLines ("usuarios.csv");

            UsuarioModel usuario;

            foreach (var item in linhas) {

                //Verifica se a linha é vazia
                if (string.IsNullOrEmpty (item)) {
                    //Retorna para o foreach
                    continue;
                }

                string[] linha = item.Split (';');

                usuario = new UsuarioModel ();

                usuario.Id = int.Parse (linha[0]);
                usuario.Nome = linha[1];
                usuario.Email = linha[2];
                usuario.Senha = linha[3];
                usuario.DataNascimento = DateTime.Parse (linha[4]);

                lsUsuarios.Add (usuario);
            }
            return lsUsuarios;
        }

        public UsuarioModel Login (string email, string senha) {
            using (StreamReader sr = new StreamReader ("usuarios.csv")) {
                while (!sr.EndOfStream) {
                    var linha = sr.ReadLine ();

                    if (string.IsNullOrEmpty (linha)) {
                        continue;
                    }

                    string[] dados = linha.Split (";");

                    if (dados[2] == email && dados[3] == senha) {
                        UsuarioModel usuario = new UsuarioModel ();
                        usuario.Id = int.Parse (dados[0]);
                        usuario.Nome = dados[1];
                        usuario.Email = dados[2];
                        usuario.Senha = dados[3];
                        usuario.DataNascimento = DateTime.Parse (dados[4]);

                        return usuario;
                    }
                }
            }
            return null;
        }

        public UsuarioModel BuscarPorId (int id) {
            string[] linhas = System.IO.File.ReadAllLines ("usuarios.csv");

            foreach (var item in linhas) {
                string[] dados = item.Split (';');

                if (id.ToString () == dados[0]) {
                    UsuarioModel usuario = new UsuarioModel ();
                    usuario.Id = int.Parse (dados[0]);
                    usuario.Nome = dados[1];
                    usuario.Email = dados[2];
                    usuario.Senha = dados[3];
                    usuario.DataNascimento = DateTime.Parse (dados[4]);

                    return usuario;
                }
            }
            return null;
        }
    }
}