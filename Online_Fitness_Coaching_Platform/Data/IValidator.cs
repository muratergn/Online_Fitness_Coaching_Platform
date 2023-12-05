namespace Online_Fitness_Coaching_Platform.Data
{
    public interface IValidator<T>
    {
        string ErrorMessage { get; set; }
        bool Validation(T entity);
    }
}
