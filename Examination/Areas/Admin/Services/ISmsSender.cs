using System.Threading.Tasks;

namespace Examination.Areas.Admin.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
