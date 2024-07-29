using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NWEC.P.L001_Task3.DataAccessLayer.Models;

public interface IQuizService
{
    Task<PaginatedResult<Quiz>> GetQuizzesAsync(int pageIndex, int pageSize);
    Task<Quiz> GetQuizByIdAsync(Guid id);
    Task<int> CreateQuizAsync(Quiz quiz);
    Task<int> UpdateQuizAsync(Quiz quiz);
    Task<int> DeleteQuizAsync(Guid id);
    Task<bool> AddAsync(Quiz quiz);
    Task<bool> UpdateAsync(Quiz quiz);
    Task<bool> DeleteAsync(Guid id);
}
