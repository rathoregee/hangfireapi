using g2v.core.clinetsync.common.Classes.Models;
namespace g2v.core.clinetsync.dataaccess.Interfaces
{
    public interface IDataContext
    {
        IQueryable<Application> Applications { get; }
        Task SaveChangesAsync();
        void Add(object role);
        void Attach(object role);
        void Remove(object role);
    }
}
