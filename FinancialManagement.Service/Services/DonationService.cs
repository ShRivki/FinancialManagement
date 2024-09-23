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
    public class DonationService : IContributionService
    {
        private readonly IDonationRepository _ContributionRepository;
        public DonationService(IDonationRepository ContributionRepository)
        {
            _ContributionRepository = ContributionRepository;
        }

        public async Task<IEnumerable<Donation>> GetAllAsync()
        {
            return await _ContributionRepository.GetAsync();
        }

        public async Task<Donation> GetContributionByIdAsync(int id)
        {
            return await _ContributionRepository.GetAsync(id);
        }

        public async Task<Donation> PostContributionAsync(Donation value)
        {
            var addDonation = await _ContributionRepository.PostAsync(value);
            string body = $@"
<html>
  <body>
    <div style='width: 500px; padding: 40px; background-color: #f0f8ff; border-radius: 15px; font-family: Arial, sans-serif;'>
        <h2 style='color: #333;'>שלום {addDonation.Donor.FirstName},</h2>
        <p style='color: #333; font-size: 18px;'>אנו מבקשים להודות לך על תרומתך הנדיבה בסך {addDonation.Amount} {addDonation.Currency}.</p>
        <p style='color: #333; font-size: 16px;'>התרומה שלך מסייעת לנו להמשיך ולתמוך בקהילה ולעזור למשפחות שזקוקות לכך.</p>
        <p style='color: #333; font-size: 16px;'>תורמים כמוך הם הלב הפועם של הפעילות שלנו, ואנו מעריכים עמוקות את נדיבותך ורצונך לסייע. בזכותך, אנו מצליחים להשפיע לטובה על חיים של רבים.</p>
        <p style='color: #333; font-size: 16px;'>נשמח לעדכן אותך בהמשך כיצד תרומתך עזרה, ולקוות שתמשיך לעמוד לצידנו בעתיד.</p>
        <br/>
        <p style='color: #333; font-size: 14px;'>בברכה חמה,</p>
        <p style='color: #333; font-size: 14px;'><strong>גמ""ח רץ כצבי</strong></p>
    </div>
  </body>
</html>";
            await EmailService.SendEmailAsync(
             value.Donor.Email, // כתובת המייל של התורם
               "Thank You for Your Generous Donation", // נושא המייל
             body // גוף המייל המעוצב

         );
            return addDonation;

        }

        public async Task<Donation> PutContributionAsync(int id, Donation value)
        {
            return await _ContributionRepository.PutAsync(id, value);

        }
        public async Task<Donation> DeleteContributionAsync(int id)
        {
            return await _ContributionRepository.DeleteAsync(id);
        }

    }
}
