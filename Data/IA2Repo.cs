using A2Template.Models;
using System.Threading.Tasks;

namespace A2Template.Data
{
    public interface IA2Repo
    {
        Task<User> GetUserByUserNameAsync(string UserName);
        Task<User> RegisterUserAsync(User user);
        Task<bool> UserValidLoginAsync(string UserName, string Password);
        Task<bool> StaffValidLoginAsync(string UserName, string Password);
        Task<Staff> GetStaffByUserNameAsync(string userName);
        Task<Event> AddEventAsync(EventInput newEvent);
        Task<List<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
    }
}