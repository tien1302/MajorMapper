﻿using BAL.Authentications;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Universities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private IUniversityDAO _universityDAO;

        public UniversityController(IUniversityDAO universityDAO)
        {
            _universityDAO = universityDAO;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<GetUniversity> listUni = _universityDAO.GetAll();
                return Ok(new
                {
                    Data = listUni
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                GetUniversity university = _universityDAO.Get(id);
                return Ok(new
                {
                    Data = university
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUniversity createUni)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _universityDAO.Create(createUni);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateUniversity updateUni)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _universityDAO.Update(id, updateUni);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromForm] int id)
        {
            try
            {
                _universityDAO.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
