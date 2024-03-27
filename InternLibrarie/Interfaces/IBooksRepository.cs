using InternLibrarie.Dto;
using InternshipLibrarie.Models;
using System;
using System.Collections.Generic;

public interface IBookRepository
{
    bool IsBookExists(string title);
    Task AddBook(BookDto dtobook);
    Task DeleteBook(string title);
    Task EditBook(string titlee, BookDto updatedBookDto);
    Books GetBookById(int bookId);
    Task<IEnumerable<Books>> GetAllBooks(int userId);
    Task<IEnumerable<Books>> SearchBooks(int userId, string searchTerm);
    Task BorrowBook(int userId, int bookId);
    Task ReturnBook(int userId, int bookId);
    Task CalculateFees(int userId);
    Task<string> GetUserSubscriptionType(int userId);

}
