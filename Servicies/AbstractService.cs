using Data_Access.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Servicies
{
    public abstract class AbstractService
    {
        protected readonly IBookRepository _bookRepository;
        protected readonly ILogErrorRepository _logErrorRepository;

        public AbstractService(IBookRepository repository, ILogErrorRepository logErrorRepository)
        {
            _bookRepository = repository;
            _logErrorRepository = logErrorRepository;
        }

        protected ResponseDTO TryExecute<T>(Action action, string errorMessage) where T : Exception
        {
            var response = new Common.ResponseDTO();

            try
            {
                action();
                response.IsSuccess = true;
            }
            catch (T ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;

            }
            return response;
        }

        public virtual Book GetBookById(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID."); //////////////////

            var entity = _bookRepository.GetBookById(id);
            if (entity == null) throw new Exception("Entity not found."); ////////////////
            return entity;
        }

        public List<Book> GetAllBooks() // Provide list of all NOT deleted books
        {
            var result = _bookRepository.GetAllBooks().
                Where(x => x.IsDeleted == false)
                .ToList();

            if (result.Count == 0)
            {
                throw new Exception("No hay libros registrados.");
            }
            else
            {
                return result;
            }
        }
        public List<Book> GetAllDeletedBooks() // Provide list of all deleted books
        {
            var result = _bookRepository.GetAllBooks().
                Where(x => x.IsDeleted == true)
                .ToList();

            if (result.Count == 0)
            {
                throw new Exception("No hay libros eliminados.");
            }
            else
            {
                return result;
            }
        }
    }
}
