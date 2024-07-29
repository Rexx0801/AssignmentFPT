using NWEC.P.L001_Task3.DataAccessLayer.Models;
using NWEC.P.L001_Task3.DataAccessLayer.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Quiz> QuizRepository { get; }
    IGenericRepository<Question> QuestionRepository { get; }
    IGenericRepository<Answer> AnswerRepository { get; }
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<Role> RoleRepository { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}