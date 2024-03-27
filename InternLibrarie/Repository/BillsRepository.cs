using InternshipLibrarie.Data;
using InternshipLibrarie.Dto;
using InternshipLibrarie.Interfaces;
using InternshipLibrarie.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class BillRepository : IBillsRepository
{
    private readonly DataContext _context;

    public BillRepository(DataContext context)
    {
        _context = context;
    }

    public void AddBill(Bills bill)
    {
        _context.bills.Add(bill);
        _context.SaveChanges();
    }

    public async Task<BillDto> CreateBillAsync(int borrowId)
    {
        var borrowedBook = await _context.borrowedbooks
            .Include(bb => bb.User)
            .Include(bb => bb.Book)
            .FirstOrDefaultAsync(bb => bb.id == borrowId);

        if (borrowedBook == null)
        {
            return null;
        }

        var user = borrowedBook.User;
        if (user == null)
        {
            throw new InvalidOperationException("User not found for the borrowed book.");
        }

        var totalFeesDue = user.total_fees_due; // Retrieve total fees due from the user

        var newBill = new Bills
        {
            borrow_id = borrowId,
            BorrowedBook = borrowedBook
        };
        _context.bills.Add(newBill);
        await _context.SaveChangesAsync();

        var billDto = new BillDto
        {
            BorrowId = newBill.borrow_id,
            UserName = borrowedBook.User.username,
            BookTitle = borrowedBook.Book.title,
            BorrowDate = borrowedBook.borrow_date,
            ReturnDate = borrowedBook.return_date,
            TotalFeesDue = (int)totalFeesDue 
        };

        return billDto;
    }


    public async Task<List<BillDto>> GetBillsForUserAsync(int userId)
    {
        var bills = await _context.bills
            .Include(b => b.BorrowedBook)
            .ThenInclude(bb => bb.User)
            .Include(b => b.BorrowedBook.Book)
            .Where(b => b.BorrowedBook.user_id == userId)
            .ToListAsync();

        if (bills == null || !bills.Any())
        {
            return null; 
        }

        var billDtos = new List<BillDto>();

        foreach (var bill in bills)
        {
            var billDto = new BillDto
            {
                BorrowId = bill.borrow_id,
                UserName = bill.BorrowedBook.User.username,
                BookTitle = bill.BorrowedBook.Book.title,
                BorrowDate = bill.BorrowedBook.borrow_date,
                ReturnDate = bill.BorrowedBook.return_date,
                TotalFeesDue = (int)bill.BorrowedBook.User.total_fees_due
            };

            billDtos.Add(billDto);
        }

        return billDtos;
    }




}
