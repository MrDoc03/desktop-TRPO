using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Десктоп_РПМ
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            
        }
        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            using (UserContext context = new UserContext()) {
                string email = txtEmail.Text;
                string username = txtUsername.Text;
                string password = txtPassword.Password;

                // Проверяем существование пользователя в базе данных
                var existingUser = context.Users.FirstOrDefault(u => u.Email == email && u.Login == username && u.Password == password);
                if (existingUser != null)
                {
                    MessageBox.Show("Авторизация успешна!");
                    Account.Email = email;
                    Account.Name = username;
                    Account.Password = password;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден. Пожалуйста, зарегистрируйтесь.");
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            using (UserContext context = new UserContext())
            {
                string email = txtEmail.Text;
                string username = txtUsername.Text;
                string password = txtPassword.Password;

                // Проверяем, что пользователь с таким же email или логином уже не существует
                var existingUser = context.Users.FirstOrDefault(u => u.Email == email || u.Login == username);
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким email или логином уже существует.");
                    return;
                }

                // Создаем нового пользователя и добавляем его в базу данных
                var newUser = new User
                {
                    Email = email,
                    Login = username,
                    Password = password
                };
                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show("Пользователь успешно зарегистрирован!");
            }
        }
    }
}
