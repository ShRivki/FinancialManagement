using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Core.Entities;
using FinancialManagement.Core.Repositories;
using FinancialManagement.Core.Services;

namespace FinancialManagement.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _UserRepository.GetAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _UserRepository.GetAsync(id);
        }

        //public async Task<User> PostUserAsync(User value)
        //{
        //    return await _UserRepository.PostAsync(value);
        //}

        public async Task<User> PutUserAsync(int id, User value)
        {
            return await _UserRepository.PutAsync(id, value);
        }
        public async Task<User> PostUserAsync(User user)
        {
            // הוסף את המשתמש למאגר הנתונים
            var addedUser = await _UserRepository.PostAsync(user);

            // בדוק אם המשתמש נוסף בהצלחה
            if (addedUser != null)
            {
                try
                {
                    // שלח מייל מעוצב למשתמש החדש
                    string body = $@"
    <html>
        <body>
            <div style='width: 500px; height: 300px; background-color: pink; text-align: center; padding: 40px; border-radius: 15px;'>
                <h2 style='color: black;'>ברוך הבא, {addedUser.FirstName}!</h2>
                <p style='color: black; font-size: 18px;'>שמחים לבשר שהצטרפת למערכת שלנו. מעתה תוכל ליהנות מכלל שירותי גמ""ח רץ כצבי.</p>
                <p style='color: black; font-size: 16px;'>אנו כאן לכל שאלה או בקשה.</p>
                <br/>
                <p style='color: black; font-size: 14px;'>בברכה,</p>
                <p style='color: black; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p>
            </div>
        </body>
    </html>";

                    await EmailService.SendEmailAsync(
                        addedUser.Email, // כתובת המייל של המשתמש
                        "Welcome to Our Service", // נושא המייל
                        body // גוף המייל המעוצב
                    );
                }
                catch (Exception ex)
                {
                    // טיפול בשגיאות שליחת מייל
                    Console.WriteLine("Error sending email: " + ex.Message);
                    // ניתן להוסיף טיפול נוסף, כגון לוג שגיאות או התראה למנהל המערכת
                }
            }

            return addedUser;
        }
        public async Task<User> PutReliabilityAsync(int id, bool reliability)
        {
            return await _UserRepository.PutReliabilityAsync(id, reliability);
        }
        public async Task<User> DeleteUserAsync(int id)
        {
            return await _UserRepository.DeleteAsync(id);
        }

    }
}
