using Application.Facade.Health;
using Domain.Common.Exception;
using Domain.Health.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Health
{
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class HealthController : ControllerBase
	{
		private readonly ILogger<HealthController> _logger;
		private readonly HealthFacade _healthFacade;

		public HealthController(ILogger<HealthController> logger, IHealthRepository healthRepository)
		{
			_logger = logger;
			_healthFacade = new HealthFacade(healthRepository);
		}

		[ProducesResponseType(typeof(string), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet]
		public ActionResult Get()
		{
			try
			{
				_healthFacade.IsReady();
				return Ok("Healthy");
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

		[ProducesResponseType(typeof(string), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet("Ready")]
		public ActionResult Ready()
		{
			try
			{
				return Ok("UP");
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
