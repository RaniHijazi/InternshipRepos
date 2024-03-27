using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using InternshipLibrarie.Models;
using InternshipLibrarie.Data;
using InternLibrarie.Dto;

public class BookRepository: IBookRepository
{
    private readonly DataContext _context;

    public BookRepository(DataContext context)
    {
        _context = context;
    }
    public bool IsBookExists(string title)
    {
        return _context.books.Any(b=>b.title == title);
    }



    public async Task AddBook(BookDto dtobook)
    {
        if (IsBookExists(dtobook.title))
            throw new InvalidOperationException("Book already exists");

        var book = new Books
        {
            author = dtobook.author,
            title = dtobook.title,
            genre = dtobook.genre,
            total_copies = dtobook.total_copies,
            
        };

        _context.books.Add(book);
        await _context.SaveChangesAsync();
    }



    public async Task DeleteBook(string title)
    {
        var book = await _context.books.FirstOrDefaultAsync(b => b.title == title);

        if (book != null)
        {
            _context.books.Remove(book);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Book not found");
        }
    }
    public async Task EditBook(string titlee, BookDto updatedBookDto)
    {
        var existingBook = await _context.books.FirstOrDefaultAsync(b => b.title == titlee);

        if (existingBook == null)
        {
            throw new InvalidOperationException("Book not found.");
        }
        existingBook.title = updatedBookDto.title;
        existingBook.author = updatedBookDto.author;
        existingBook.genre = updatedBookDto.genre;
        existingBook.total_copies = updatedBookDto.total_copies;

        await _context.SaveChangesAsync();
    }


    public Books GetBookById(int bookId)
    {
        return _context.books.Find(bookId);
    }

    public async Task<IEnumerable<Books>> GetAllBooks(int userId)
    {
        try
        {
            var subscriptionType = await GetUserSubscriptionType(userId);
            var allBooks = await _context.books.ToListAsync();

            if (subscriptionType == "premium"|| subscriptionType == null)
            {
                return allBooks;
            }
            else
            {
                var halfCount = allBooks.Count / 2;
                return allBooks.Take(halfCount).ToList();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching books", ex);
        }
    }


    public async Task < IEnumerable<Books>> SearchBooks(int userId, string searchTerm)
    {
        var books = await GetAllBooks(userId);

        return books.Where(book =>
        book.title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
        book.author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
        book.genre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
    );
    }


    public async Task<string> GetUserSubscriptionType(int userId)
    {
        var latestSubscription = await _context.subscriptions
            .Where(s => s.User.id == userId)
            .OrderByDescending(s => s.subscription_expiry_date)
            .FirstOrDefaultAsync();

        if (latestSubscription != null && latestSubscription.subscription_expiry_date >= DateTime.Now)
        {
            return latestSubscription.subscription_name;
        }

        return null;
    }

    public async Task BorrowBook(int userId, int bookId)
    {
        var user = await _context.users.FindAsync(userId);
        var book = await _context.books.FindAsync(bookId);

        if (user == null || book == null)
        {
            throw new ArgumentException("User or book not found.");
        }

        if (book.total_copies <= 0)
        {
            throw new InvalidOperationException("This book is not available for borrowing.");
        }

        string subscriptionType = await GetUserSubscriptionType(userId);
        if (string.IsNullOrEmpty(subscriptionType))
        {
            throw new InvalidOperationException("User does not have an active subscription and cannot borrow books.");
        }

        int maxBooksAllowed = subscriptionType == "premium" ? 10 : 5;

        if (user.total_books_borrowed >= maxBooksAllowed)
        {
            throw new InvalidOperationException("User has reached the maximum limit of borrowed books.");
        }

        var borrowedBook = new BorrowedBooks
        {
            user_id = userId,
            book_id = bookId,
            borrow_date = DateTime.Now,
            return_date = DateTime.Now.AddDays(30),
            overdue_fees = 0,
        };

        _context.borrowedbooks.Add(borrowedBook);
        user.total_books_borrowed++; // Increment total_books_borrowed directly from User entity
        _context.Entry(book).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }





    public async Task ReturnBook(int userId, int bookId)
    {
        var user = await _context.users.FindAsync(userId);
        var book = await _context.books.FindAsync(bookId);
        var borrowedBook = await _context.borrowedbooks
                                         .Where(b => b.user_id == userId && b.book_id == bookId)
                                         .FirstOrDefaultAsync();

        if (borrowedBook != null)
        {
            _context.borrowedbooks.Remove(borrowedBook);
            await _context.SaveChangesAsync();
        }
        book.total_copies++;
    }



    public async Task CalculateFees(int userId)
    {
        string subscriptionType =await GetUserSubscriptionType(userId);

        if (string.IsNullOrEmpty(subscriptionType))
        {
            var borrowedBooks = await _context.borrowedbooks.Where(b => b.user_id == userId).ToListAsync();

            if (borrowedBooks.Count > 0)
            {
                foreach (var borrowedBook in borrowedBooks)
                {
                    borrowedBook.overdue_fees += 5;
                }

                await _context.SaveChangesAsync();
            }
        }
    }

}
