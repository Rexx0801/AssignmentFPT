
using NWEC.P.L001_Task3.DataAccessLayer.Models;
using NWEC.P.L001_Task3.DataAccessLayer.Repositories;
using System.Threading.Tasks;

public class UnitOfWork : IUnitOfWork
{
    private readonly QuizAppDbContext _context;
    private IGenericRepository<Quiz> _quizRepository;
    private IGenericRepository<Question> _questionRepository;
    private IGenericRepository<Answer> _answerRepository;
    private IGenericRepository<User> _userRepository;
    private IGenericRepository<Role> _roleRepository;

    public UnitOfWork(QuizAppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Quiz> QuizRepository => _quizRepository ??= new GenericRepository<Quiz>(_context);
    public IGenericRepository<Question> QuestionRepository => _questionRepository ??= new GenericRepository<Question>(_context);
    public IGenericRepository<Answer> AnswerRepository => _answerRepository ??= new GenericRepository<Answer>(_context);
    public IGenericRepository<User> UserRepository => _userRepository ??= new GenericRepository<User>(_context);
    public IGenericRepository<Role> RoleRepository => _roleRepository ??= new GenericRepository<Role>(_context);

    public QuizAppDbContext Context => _context;

    public IGenericRepository<UserQuiz> UserQuizRepository => throw new NotImplementedException();

    public IGenericRepository<QuizQuestion> QuizQuestionRepository => throw new NotImplementedException();

    public IGenericRepository<UserAnswer> UserAnswerRepository => throw new NotImplementedException();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

    public int SaveChanges() => _context.SaveChanges();

    public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync() => await _context.Database.CommitTransactionAsync();

    public async Task RollbackTransactionAsync() => await _context.Database.RollbackTransactionAsync();

    public void Dispose() => _context.Dispose();
}

