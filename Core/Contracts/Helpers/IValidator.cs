namespace Recommender.Core.Helpers
{
    public interface IValidator<T>
    {
        bool ValidateModel(T model);
    }
}
