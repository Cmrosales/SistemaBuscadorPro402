﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaBuscador.Entities;
using SistemaBuscador.Respositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBuscador.Test.PruebasUnitarias.Servicios
{
    [TestClass]
    class LoginRepositoryEFTest :TestBase
    {
        [TestMethod]        
        public async Task UsuarioNoExiste()
        {
            //Preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            context.Usuarios.Add(new Usuario() { NombreUsuario = "Usuario1", Password="Password1" });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);

            //Ejecucion

            var nombreUsuario = "Usuario2";
            var password = "Password2";
            var repo = new LoginRepositoryEF(context2);
            var respuesta = await repo.UserExist(nombreUsuario, password);

            //Verificacion

            Assert.IsFalse(respuesta);

        }

        [TestMethod]
        public async Task UsuarioExiste()
        {
            //Preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            context.Usuarios.Add(new Usuario() { NombreUsuario = "Usuario1", Password = "Password1" });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);

            //Ejecucion

            var nombreUsuario = "Usuario1";
            var password = "Password";
            var repo = new LoginRepositoryEF(context2);
            var respuesta = await repo.UserExist(nombreUsuario, password);

            //Verificacion

            Assert.IsTrue(respuesta);

        }
    }
}
