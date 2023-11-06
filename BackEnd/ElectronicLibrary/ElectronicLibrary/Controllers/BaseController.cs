﻿using ElectronicLibrary.Core.Entity;
using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Interface.Service;
using ElectronicLibrary.Core.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;
using System.ComponentModel.DataAnnotations;
using ElectronicLibrary.Core.Parameters;

namespace ElectronicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity> : Controller
    {
        #region field
        IBaseRepo<Entity> _repo;
        IBaseService<Entity> _service;
        #endregion

        #region constructor
        public BaseController(IBaseRepo<Entity> repo, IBaseService<Entity> service)
        {
            _repo = repo;
            _service = service;
        }

        #endregion

        /// <summary>
        /// Lấy tất cả bản ghi trong db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _service.GetAll();
                return Ok(res);

            }
            catch (Exception ex)
            {
                var res = new ErrorEntity(MessegeResources.ErrorMessage, ex.Message);
                StatusCode(500, res);
                throw;
            }
        }

        [HttpPost("GetPaging")]
        [Authorize]
        public async Task<IActionResult> GetPaging ([FromBody] PagingParameter entity)
        {
            try
            {
                var res = await _service.GetPaging(entity);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var res = new ErrorEntity(MessegeResources.ErrorMessage, ex.Message);
                StatusCode(500, res);
                throw;
            }
        }

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] Entity entity)
        {
            try
            {
                var res = _service.Insert(entity);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var res = new ErrorEntity(MessegeResources.ErrorMessage, ex.Message);
                StatusCode(500, res);
                throw;
            }
        }

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Update")]
        public IActionResult Update([FromBody] Entity entity)
        {
            try
            {
                var res = _service.Update(entity);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var res = new ErrorEntity(MessegeResources.ErrorMessage, ex.Message);
                StatusCode(500, res);
                throw;
            }
        }

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult Delete([FromBody] Entity entity)
        {
            try
            {
                var res = _service.Delete(entity);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var res = new ErrorEntity(MessegeResources.ErrorMessage, ex.Message);
                StatusCode(500, res);
                throw;
            }
        }

        [Authorize]
        [HttpPost("CheckDuplicate")]
        public IActionResult CheckDuplicate([FromBody] Entity entity)
        {
            try
            {
                var res = _service.CheckDuplicate(entity);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var res = new ErrorEntity(MessegeResources.ErrorMessage, ex.Message);
                StatusCode(500, res);
                throw;
            }
        }
    }
}
