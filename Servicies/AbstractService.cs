using Data_Access.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Exceptions;
using Helper;

namespace Servicies
{
    public abstract class AbstractService
    {
        protected readonly IBookRepository _bookRepository;
        protected readonly ILogErrorRepository _logErrorRepository;
        protected readonly IChangeLogRepository _changeLogRepository;

        public AbstractService(IBookRepository repository, ILogErrorRepository logErrorRepository, IChangeLogRepository changeLogRepository)
        {
            _bookRepository = repository;
            _logErrorRepository = logErrorRepository;
            _changeLogRepository = changeLogRepository;
        }

        protected ResponseDTO TryExecute<T>(Action action, ResponseDTO response) where T : Exception
            // General method for try-catch block
            // T is the type of exception that is expected
            // The method will return a ResponseDTO object (Message and IsSuccess), but the Result will be NULL
            // Action should assign the result when relevant
        {
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
                response.Message = Common.Exceptions.GlobalErrorMessages.errorExceptionNotExpected;
                _logErrorRepository.AddLogError(ex.ToString());
            }
            return response;
        }

        public virtual ResponseDTO SearchBookByTitle(string title) // Provide a book by Title as a ReponseDTO object
        {
            var response = new Common.ResponseDTO();
            response = TryExecute<InvalidInputException>(() => //There is no specific exception to catch
            {
                response.Result = _bookRepository.GetBookByTitle(title);
                if (response.Result == null) { throw new InvalidInputException(); }
            }, response);
            return response;
        }
        public ResponseDTO GetAllBooks() // Provide list of all NOT deleted books
        {
            var response = new Common.ResponseDTO();

            response = TryExecute<Exception>(() => //There is no specific exception to catch
            {
                var result = _bookRepository.GetAllBooks()
                    .Where(x => x.IsDeleted == false)
                    .ToList();
                response.Result = result.Any() ? result : new List<Book>(); // If there are no books, return an empty list
            }, response);

            return response;
        }

        public ResponseDTO GetAllDeletedBooks() // Provide list of all deleted books
        {
            var response = new Common.ResponseDTO();

            response = TryExecute<Exception>(() => //There is no specific exception to catch
            {
                var result = _bookRepository.GetAllBooks()
                    .Where(x => x.IsDeleted == true)
                    .ToList();
                response.Result = result.Any() ? result : new List<Book>(); // If there are no books, return an empty list
            }, response);

            return response;
        }
    }
}
