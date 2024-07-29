using Microsoft.EntityFrameworkCore;
using NWEC.P.L001_Task3.DataAccessLayer.Models;
using NWEC.P.L001_Task3.DataAccessLayer.Repositories;
using System;
using System.Threading.Tasks;

public class QuizService : IQuizService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuizService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<Quiz>> GetQuizzesAsync(int pageIndex, int pageSize)
    {
        var query = _unitOfWork.QuizRepository.GetQuery();
        return await PaginatedResult<Quiz>.CreateAsync(query, pageIndex, pageSize);
    }

    public async Task<Quiz> GetQuizByIdAsync(Guid id)
    {
        return await _unitOfWork.QuizRepository.GetByIdAsync(id);
    }

    public async Task<int> CreateQuizAsync(Quiz quiz)
    {
        _unitOfWork.QuizRepository.Add(quiz);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> UpdateQuizAsync(Quiz quiz)
    {
        _unitOfWork.QuizRepository.Update(quiz);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> DeleteQuizAsync(Guid id)
    {
        _unitOfWork.QuizRepository.Delete(id);
        return await _unitOfWork.SaveChangesAsync();
    }

    public Task<bool> AddAsync(Quiz quiz)
    {
        _unitOfWork.QuizRepository.Add(quiz);
        return _unitOfWork.SaveChangesAsync().ContinueWith(task => task.Result > 0);
    }

    public Task<bool> UpdateAsync(Quiz quiz)
    {
        _unitOfWork.QuizRepository.Update(quiz);
        return _unitOfWork.SaveChangesAsync().ContinueWith(task => task.Result > 0);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        _unitOfWork.QuizRepository.Delete(id);
        return _unitOfWork.SaveChangesAsync().ContinueWith(task => task.Result > 0);
    }
}
