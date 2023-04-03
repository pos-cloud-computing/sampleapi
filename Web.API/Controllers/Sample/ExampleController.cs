using Application.Facade.Sample;
using Domain.Common.Exception;
using Domain.Sample.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Sample
{
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class ExampleController : Controller
	{
		private readonly ILogger<ExampleController> _logger;
		private readonly ExampleFacade _exampleFacade;

		public ExampleController(ILogger<ExampleController> logger, IExampleRepository exampleRepository)
		{
			_logger = logger;
			_exampleFacade = new ExampleFacade(exampleRepository);

		}
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpPost()]
		public ActionResult Post(Application.DTO.Sample.Example example)
		{
			try
			{
				_exampleFacade.Create(example);
				return StatusCode(201);
			}
			catch (NotFoundException nf)
			{
				_logger.LogWarning(nf.Message);
				return NotFound(nf.Message);
			}
			catch (BusinessException pe)
			{
				_logger.LogWarning(pe.Message, pe);
				return BadRequest(pe.Message);
			}
			catch (System.Exception e)
			{
				_logger.LogError(e.Message, e);
				return StatusCode(500, e.Message);
			}
		}

		[ProducesResponseType(typeof(Application.DTO.Sample.Example), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet("{id}")]
		public ActionResult Get(long id)
		{
			try
			{
				var example = _exampleFacade.Get(id);
				return Ok(example);
			}
			catch (NotFoundException nf)
			{
				_logger.LogWarning(nf.Message);
				return NotFound(nf.Message);
			}
			catch (BusinessException pe)
			{
				_logger.LogWarning(pe.Message, pe);
				return BadRequest(pe.Message);
			}
			catch (System.Exception e)
			{
				_logger.LogError(e.Message, e);
				return StatusCode(500, e.Message);
			}
		}

		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpPut()]
		public ActionResult Put(Application.DTO.Sample.Example example)
		{
			try
			{
				_exampleFacade.Update(example);
				return Ok();
			}
			catch (NotFoundException nf)
			{
				_logger.LogWarning(nf.Message);
				return NotFound(nf.Message);
			}
			catch (BusinessException pe)
			{
				_logger.LogWarning(pe.Message, pe);
				return BadRequest(pe.Message);
			}
			catch (System.Exception e)
			{
				_logger.LogError(e.Message, e);
				return StatusCode(500, e.Message);
			}
		}

		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpPatch("{id}")]
		public ActionResult Patch(long id, Application.DTO.Sample.Example example)
		{
			try
			{
				_exampleFacade.Patch(id, example);
				return Ok();
			}
			catch (NotFoundException nf)
			{
				_logger.LogWarning(nf.Message);
				return NotFound(nf.Message);
			}
			catch (BusinessException pe)
			{
				_logger.LogWarning(pe.Message, pe);
				return BadRequest(pe.Message);
			}
			catch (System.Exception e)
			{
				_logger.LogError(e.Message, e);
				return StatusCode(500, e.Message);
			}
		}

		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpDelete("{id}")]
		public ActionResult Delete(long id)
		{
			try
			{
				_exampleFacade.Inactivate(id);
				return StatusCode(204);
			}
			catch (NotFoundException nf)
			{
				_logger.LogWarning(nf.Message);
				return NotFound(nf.Message);
			}
			catch (BusinessException pe)
			{
				_logger.LogWarning(pe.Message, pe);
				return BadRequest(pe.Message);
			}
			catch (System.Exception e)
			{
				_logger.LogError(e.Message, e);
				return StatusCode(500, e.Message);
			}
		}
	}
}
