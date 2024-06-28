using Microsoft.EntityFrameworkCore;
using webtimesheet.DAL.Repositories;
using webtimesheet.Models;

namespace webtimesheet.DAL.Services
{
    public class TimesheetsService : ITimesheetRepository
    {
        private readonly TimesheetsDbContext _db;
        private readonly TimesheetsService _timesheetsService;

        public TimesheetsService(TimesheetsDbContext db, TimesheetsService timesheetsService)
        {
            _db = db;
            _timesheetsService = timesheetsService;
        }

        public async Task Add(Timesheet entity)
        {
            try
            {
                await _db.Timesheets.AddAsync(entity);
                await _timesheetsService.Add(entity.TimesheetsNavigation);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var user = await GetById(id);

                _db.Timesheets.Remove(user);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<Timesheet>> GetAll()
        {
            var timesheet = await _db.Timesheets.ToListAsync();
            return timesheet;
        }

        public async Task<Timesheet> GetById(int id)
        {
            try
            {
                var timesheet = await _db.Timesheets
                        .Include(x => x.TimesheetId)
                        .FirstOrDefaultAsync(x => x.TimesheetId == id);

                if (timesheet == null)
                {
                    throw new Exception("Timesheet tidak ditemukan");
                }

                return timesheet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Timesheet> Update(Timesheet entity)
        {
            throw new NotImplementedException();
        }
    }
}
