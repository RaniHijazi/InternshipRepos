using InternshipLibrarie.Data;
using InternshipLibrarie.Interfaces;
using InternshipLibrarie.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly DataContext _context;

    public SubscriptionsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task RenewSubscriptionAsync(int userId)
    {
        var user = await _context.users.FirstOrDefaultAsync(u => u.id == userId);

        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }

        var subscription = await _context.subscriptions
            .Where(s => s.user_id == userId)
            .OrderByDescending(s => s.subscription_expiry_date)
            .FirstOrDefaultAsync();

        int subscriptionFees = subscription.subscription_name.ToLower() == "normal" ? 10 : 20;
        if (subscription != null)
        {
            user.total_fees_due = 0;
            user.total_books_borrowed = 0;
            var borrowedBooks = await _context.borrowedbooks
                .Where(b => b.user_id == userId)
                .ToListAsync();

            foreach (var book in borrowedBooks)
            {
                book.overdue_fees = 0;
            }
            subscription.subscription_expiry_date = DateTime.Now.AddMonths(1);
        }
        else
        {
            throw new InvalidOperationException("User does not have an active subscription.");
        }

        await _context.SaveChangesAsync();
    }


    public Subscriptions GetSubscriptionByUserId(int userId)
    {
        return _context.subscriptions.FirstOrDefault(s => s.user_id == userId);
    }




}
