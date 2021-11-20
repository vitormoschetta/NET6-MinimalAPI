public class Todo
{
    // constructor
    public Todo(string? title, bool done = false)
    {
        Id = Guid.NewGuid();
        Title = title;
        Done = done;
    }

    public Guid Id { get; private set; }
    public string? Title { get; private set; }
    public bool Done { get; private set; }
    
    public void Update(string title, bool done = false)
    {
        Title = title;
        Done = done;
    }
}