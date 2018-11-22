using System.Collections.Generic;
using Senai.Financas.Web.Mvc.Models;

namespace Senai_Financas_Web_Mvc_Manha_master.Interfaces
{
    public interface IUsuario
    {
         List <UsuarioModel> Listar();

         UsuarioModel Cadastrar (UsuarioModel usuario);

         UsuarioModel Editar (UsuarioModel usuario);

         void Excluir (int id);

         UsuarioModel Login (string email, string senha);
    }

}