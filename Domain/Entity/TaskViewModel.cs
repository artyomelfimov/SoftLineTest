using Domain.Enums;

namespace Domain.Entity
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public StatusName StatusName { get; set; }
    }
}
