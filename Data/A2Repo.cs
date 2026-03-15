using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;
using A2Template.Models;

namespace A2Template.Data
{
    public class A2Repo : IA2Repo
    {
        private readonly A2DbContext _context;

        public A2Repo(A2DbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUserNameAsync(string UserName)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.UserName == UserName);
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            ValueTask<EntityEntry<User>> t = _context.Users.AddAsync(user);
            await t;
            User u = t.Result.Entity;
            await _context.SaveChangesAsync();
            return u;
        }

        public async Task<bool> UserValidLoginAsync(string UserName, string Password)
        {
            var u = await _context.Users.FirstOrDefaultAsync(e => e.UserName == UserName && e.Password == Password);
            return u != null;
        }

        public async Task<bool> StaffValidLoginAsync(string UserName, string Password)
        {
            var s = await _context.Staff.FirstOrDefaultAsync(e => e.Name == UserName && e.Password == Password);
            return s != null;
        }

        public async Task<Staff> GetStaffByUserNameAsync(string userName)
        {
            return await _context.Staff.FirstOrDefaultAsync(s => s.Name == userName);
        }
        public async Task<Event> AddEventAsync(EventInput newEvent)
        {
            var e = new Event
            {
                Start = newEvent.start,
                End = newEvent.end,
                Summary = newEvent.summary,
                Description = newEvent.description,
                Location = newEvent.location
            };
            ValueTask<EntityEntry<Event>> t = _context.Events.AddAsync(e);
            await t;
            Event ev = t.Result.Entity;
            await _context.SaveChangesAsync();
            return ev;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}