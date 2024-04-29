namespace NgoProject.Models
{
	public class Ngo
	{
		public int NgoId { get; set; }
		public string EventName { get; set; }
		public string EventDescription { get; set; }
		public string EventCategory { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public string EventLocation { get; set; }
		public bool Registration { get; set; }
		public string EventImage { get; set; }
		public int AdultTicket { get; set; }
		public int ChildTicket { get; set; }
	}
}
