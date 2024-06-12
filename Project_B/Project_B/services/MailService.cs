using DataAccessLibrary.logic;
using DataAccessLibrary;
using Models;
using DataAccessLibrary.models;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
public static class MailService
{
    public static void SendEmail(string toAdress, string movieName, string customerName, int? reservationID, string startTime, string endTime, List<SeatModel> reservedSeats)
    {
        try
        {
            // Set up the email
            string seats = string.Join(", ", reservedSeats.Select(seat => seat.Name));
            MailMessage mail = new MailMessage();
            string fromAdress = "youreyeshr@outlook.com";
            mail.From = new MailAddress(fromAdress);
            mail.To.Add(toAdress.ToLower());
            mail.Subject = "Reservation details YourEyes";
            mail.Body = $"Dear {customerName},\n\n" + 
            $"Thank you for booking at YourEyes!\n" +
            $"You booked {reservedSeats.Count} seats.\n" +
            $"Here are the details of your reservation:\n" +
            $"Movie: {movieName}\n" +
            $"Starts at: {startTime.Split(" ")[1]} - {endTime.Split(" ")[1]}\n" +
            $"Seats: {seats}\n" +
            $"Confirmation number: {reservationID}\n\n" +
            "See you soon!\n" +
            "YourEyes";
            mail.IsBodyHtml = false;
            // Set up the Smtp
            string smtpHost = "smtp-mail.outlook.com";
            int stmpPort = 587;
            SmtpClient client = new SmtpClient(smtpHost, stmpPort);
            client.Credentials = new NetworkCredential("youreyeshr@outlook.com", "Marcel2024!");
            client.EnableSsl = true;
            client.Send(mail);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending E-mail" + ex.Message);
        }
    }
}