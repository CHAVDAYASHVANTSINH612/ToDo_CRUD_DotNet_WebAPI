namespace TodoList_CRUD_Practice.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
