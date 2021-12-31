﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Common;
using OngProject.Core.DTOs;
using OngProject.Core.Helper.Pagination;
using OngProject.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        #region Objects and Constructor

        private readonly IMemberServices _memberServices;

        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }

        #endregion Objects and Constructor

        #region Documentation

        /// <summary>
        /// Endpoint para listar los miembros.
        /// </summary>
        /// <response code="200">Tarea ejecutada con exito devuelve la lista de miembros.</response>
        /// <response code="404">No se encontraron datos para mostrar.</response>

        #endregion Documentation

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _memberServices.GetAllAsync());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        #region Documentation

        /// <summary>
        /// Endpoint para crear un miembro.
        /// </summary>
        /// <response code="200">Tarea ejecutada con exito devuelve un mensaje satisfactorio.</response>
        /// <response code="400">Errores de validacion o excepciones.</response>

        #endregion Documentation

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MemberInsertDTO member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(await _memberServices.CreateAsync(member));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Result().Fail($"Ocurrio un error al momento de intentar ingresar los datos - {ex.Message}"));
                }
            }

            return BadRequest(new Result().Fail("Algo salio mal."));
        }
    }
}