using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NgoProject.Models;

namespace NgoProject.Controllers
{
	[EnableCors]
	[Route("api/[controller]")]
	[ApiController]
	public class NgoController : ControllerBase
	{
		private readonly AppDbContext context;
		public NgoController(AppDbContext context)
		{
			this.context = context;
		}

		//private static List<Ngo> ngoEvents = new List<Ngo>
		//{
		//	new Ngo
		//		{
		//			NgoId= 1,
		//			EventName="Museum",
		//			EventDescription="Coming Soon",
		//			EventCategory="Conference",
		//			StartDate="2/23/2023",
		//			EndDate="3/23/2023",
		//			StartTime="2:30",
		//			EndTime="9:30",
		//			EventLocation="Chicago",
		//			Registration=true,
		//			EventImage="https://bronxmuseum.org/wp-content/uploads/2022/10/20221014_Argenis_Apolinario_BxMA_DeVille_3937_Panorama-crop-1-1024x721.jpeg",
		//			AdultTicket=20,
		//			ChildTicket=15
		//		}
		//};



		[HttpGet]
		[Authorize(Roles = "Admin,User")]

		public async Task<ActionResult<List<Ngo>>> Get()
		{
			return Ok(context.NgoEvents);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "Admin,User")]

		public async Task<ActionResult<Ngo>> Get(int id)
		{
			var findEvent = context.NgoEvents.Find(id);
			if (findEvent == null)
				return BadRequest("Event Not Found");
			return Ok(findEvent);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]


		public async Task<ActionResult<List<Ngo>>> AddNgo(Ngo addEvent)
		{
			context.NgoEvents.Add(addEvent);
			context.SaveChanges();
			return Ok(context.NgoEvents);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]

		public async Task<ActionResult<List<Ngo>>> UpdateEvent(Ngo request)
		{
			var findEvent = context.NgoEvents.Find (request.NgoId);
			if (findEvent == null)
				return BadRequest("Event Not Found");
			findEvent.EventName = request.EventName;
			findEvent.EventDescription = request.EventDescription;
			findEvent.EventCategory = request.EventCategory;
			findEvent.StartDate	= request.StartDate;
			findEvent.EndDate = request.EndDate;	
			findEvent.StartTime = request.StartTime;
			findEvent.EndTime = request.EndTime;	
			findEvent.EventLocation = request.EventLocation;
			findEvent.Registration = request.Registration;
			findEvent.EventImage = request.EventImage;	
			findEvent.AdultTicket = request.AdultTicket;	
			findEvent.ChildTicket = request.ChildTicket;
			context.SaveChanges();
			return Ok(context.NgoEvents);

		}




		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]

		public async Task<ActionResult<List<Ngo>>> DeleteEvent(int id)
		{
			var findEvent = context.NgoEvents.Find(id);
			if (findEvent == null)
				return BadRequest("Event Not Found");
			context.NgoEvents.Remove(findEvent);
			context.SaveChanges();
			return Ok(context.NgoEvents);
		}

	}	
}
 