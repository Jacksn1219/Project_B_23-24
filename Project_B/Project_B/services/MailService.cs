using DataAccessLibrary.logic;
using DataAccessLibrary;
using Models;
using DataAccessLibrary.models;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using Project_B.services;
public class MailService
{
    ReservationService resService;
    public MailService(ReservationService reservationService)
    {
        resService = reservationService;
    }
    public void SendEmail(ReservationModel res)
    {
        try
        {
            // Set up the email
            string seats = string.Join(", ", res.ReservedSeats.Select(seat => resService.GetSeatLocation(seat)));
            MailMessage mail = new MailMessage();
            string fromAdress = "youreyeshr@outlook.com";
            mail.From = new MailAddress(fromAdress);
            mail.To.Add(res.Customer.Email.ToLower());
            mail.Subject = "Reservation details YourEyes";
            mail.Body = $"Dear {res.Customer.Name},\n\n" + 
            $"Thank you for booking at YourEyes!\n" +
            $"You booked {res.ReservedSeats.Count} seats.\n" +
            $"Here are the details of your reservation:\n" +
            $"Movie: {res.TimeTable.Movie.Name}\n" +
            $"Date: {res.TimeTable.DateTimeStartDate.Date.ToString("dd-MM-yyyy")}\n" +
            $"Starts at: {res.TimeTable.DateTimeStartDate.ToString("HH:mm")} - {res.TimeTable.DateTimeEndDate.ToString("HH:mm")}\n" +
            $"Room: {res.TimeTable.Room.Name}" +
            $"Seats: {seats}\n" +
            $"Confirmation number: {res.ID}\n\n" +
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