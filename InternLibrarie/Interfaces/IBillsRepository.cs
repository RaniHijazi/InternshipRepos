using InternshipLibrarie.Dto;
using InternshipLibrarie.Models;
using System.Collections.Generic;

public interface IBillsRepository
{
    void AddBill(Bills bill);
    Task<BillDto> CreateBillAsync(int borrowId);
    Task<List<BillDto>> GetBillsForUserAsync(int userId);
}
