using Flunt.Notifications;
using Flunt.Validations;

namespace MiniTodo.ViewModels
{
    public class UpdateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }
        public bool Done { get; set; }

        public void MapTo(Todo todo)
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o título da tarefa")
                .IsGreaterThan(Title, 5, "O título deve conter mais de 5 caracteres"));

            todo.Update(Title, Done);
        }
    }
}