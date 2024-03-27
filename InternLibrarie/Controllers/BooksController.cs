using InternLibrarie.Dto;
using InternshipLibrarie.Data;
using InternshipLibrarie.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace InternshipLibrarie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public readonly DataContext context;

        public BookController(IBookRepository bookRepository,DataContext context)
        {
            _bookRepository = bookRepository;
            this.context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooks(int userId)
        {
            try
            {
                var books = await _bookRepository.GetAllBooks(userId);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Books> GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            try
            {
                await _bookRepository.AddBook(bookDto);
                return Ok("Book added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("edit/{titlee}")]
        public async Task<IActionResult> EditBook(string titlee, [FromBody] BookDto bookDto)
        {
            try
            {
                await _bookRepository.EditBook(titlee, bookDto);
                return Ok("Book edited successfully");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteBook(string title)
        {
            try
            {
                await _bookRepository.DeleteBook(title);
                return Ok("Book deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting book: {ex.Message}");
            }
        }



        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks(int userId, string searchTerm)
        {
            try
            {
                var searchResults = await _bookRepository.SearchBooks(userId, searchTerm);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook(int userId, int bookId)
        {
            try
            {
                await _bookRepository.BorrowBook(userId, bookId);
                return Ok("Book borrowed successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(403, ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook(int userId, int bookId)
        {
            try
            {
                await _bookRepository.ReturnBook(userId, bookId);
                return Ok("Book returned successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("calculate-fees")]
        public async Task<IActionResult> CalculateFees(int userId)
        {
            try
            {
                await _bookRepository.CalculateFees(userId);
                return Ok("Fees calculated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("subscription-type/{userId}")]
        public async Task<IActionResult> GetUserSubscriptionType(int userId)
        {
            try
            {
                var subscriptionType = await _bookRepository.GetUserSubscriptionType(userId);
                if (subscriptionType != null)
                {
                    return Ok(subscriptionType);
                }
                else
                {
                    return NotFound("User subscription not found or expired.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}



