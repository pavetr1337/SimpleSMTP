using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleSMTP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SmtpClient client;
        MailAddress from;
        MailAddress to;
        MailMessage mail;


        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            window.WindowState = WindowState.Minimized;
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client = new SmtpClient(smtp_server.Text);
                client.Credentials = new NetworkCredential(sender_email.Text, sender_pass.Password);
                from = new MailAddress(sender_email.Text);
                preview_sender.Content = "From: "+sender_email.Text;
                MessageBox.Show("Login succesful","Simple SMTP",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch {
                MessageBox.Show("Unknown Error", "Simple SMTP", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void send_mail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                to = new MailAddress(recipient_email.Text);
                mail = new MailMessage(from, to);
                mail.Subject = mail_subject.Text;
                mail.Body = new TextRange(mail_text.Document.ContentStart, mail_text.Document.ContentEnd).Text;
                client.EnableSsl = (bool)useSSL.IsChecked;
                client.Send(mail);
                MessageBox.Show("Message Sent", "Simple SMTP", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex, "Simple SMTP", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void smtp_list_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.arclab.com/en/kb/email/list-of-smtp-and-pop3-servers-mailserver-list.html");
        }

        private void change_text_preview(object sender, RoutedEventArgs e)
        {
            preview_mailtext.Text = new TextRange(mail_text.Document.ContentStart, mail_text.Document.ContentEnd).Text;
        }

        private void changeRecieverMail(object sender, TextChangedEventArgs e)
        {
            preview_reciever.Content = "To: "+recipient_email.Text;
        }

        private void changeSubject(object sender, TextChangedEventArgs e)
        {
            preview_subject.Content = "Subject: "+mail_subject.Text;
        }
    }
}
