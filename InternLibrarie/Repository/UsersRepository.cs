using InternshipLibrarie.Data;
using InternshipLibrarie.Dto;
using InternshipLibrarie.Interfaces;
using InternshipLibrarie.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


public class UsersRepository : IUsersRepository
{
    private readonly DataContext _context;

    public UsersRepository(DataContext context)
    {
        _context = context;
    }

    public bool IsUserExists(string username)
    {
        return _context.users.Any(u => u.username == username);
    }

    public Users AuthenticateUser(string username, string password)
    {
        
        var user = _context.users.FirstOrDefault(u => u.username == username && u.password == password);
        return user;
    }

    public Users AddUser(UserDto userdto)
    {
        if (IsUserExists(userdto.username))
        {
            throw new InvalidOperationException("Username is already taken.");
        }

        var User = new Users
        {
            username = userdto.username,
            is_admin = false,
            Email = userdto.email,
            password = userdto.password,
            total_books_borrowed = 0,
            total_fees_due = 0
        };

        _context.users.Add(User);
        _context.SaveChanges(); 

        int subscriptionFees = userdto.subscriptionType.ToLower() == "normal" ? 10 : 20;
        int overdueFees = _context.borrowedbooks.Where(b => b.User.id == User.id).Sum(b => b.overdue_fees);

        User.total_fees_due = subscriptionFees + overdueFees;

        var subscription = new Subscriptions
        {
            subscription_name = userdto.subscriptionType,
            subscription_expiry_date = DateTime.Now.AddMonths(1),
            user_id = User.id 
        };

        _context.subscriptions.Add(subscription);
        _context.SaveChanges();
        return User;
    }





    public async Task DeleteUser(string username)
    {
        var user = await _context.users.FirstOrDefaultAsync(u => u.username == username);

        if (user != null)
        {
            _context.users.Remove(user);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Book not found");
        }
    }



}
