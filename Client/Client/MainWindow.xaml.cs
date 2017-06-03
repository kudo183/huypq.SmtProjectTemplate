using huypq.SmtWpfClient;
using huypq.SmtWpfClient.Abstraction;
using huypq.SmtWpfClient.View;
using huypq.SmtWpfClient.ViewModel;
using System.Windows;
using Client.View.Smt;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDataService _dataService;

        public MainWindow()
        {
            Init();

            InitializeComponent();

#if DEBUG
            (loginView.DataContext as LoginViewModel).IsLoggedIn = true;
            (loginView.DataContext as LoginViewModel).IsTenant = true;
#endif
        }

        private void Init()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes();
            ServiceLocator.AddTypeMapping(typeof(IViewModelFactory), typeof(ViewModelFactory), true, new ViewModelFactory.Options()
            {
                ViewModelNamespace = "Client.ViewModel",
                ViewModelAssembly = System.Reflection.Assembly.GetExecutingAssembly()
            });

            ServiceLocator.AddTypeMapping(typeof(IDataService), typeof(ProtobufDataService), true, new ProtobufDataService.Options()
            {
                RootUri = "http://localhost:5000",
#if DEBUG
                Token = "CfDJ8J0CZcxInRhFvSirfZanTFyQ4M2ZsTDZkFjZTHoh7pxESJq9aruiUhaRYY6_JYuhzEpa0RnG_Xo4bTYJIZag6v9IBXpEDk9LlCysPdzBkCcgBjPRBpk0Vw4WrYqTM4kS16GZ24UaVdSvSKGSnMISSUlQnyVY8CNWTDAuxqngrW0i"
#endif
            });

            _dataService = ServiceLocator.Get<IDataService>();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _dataService.Logout();
            (loginView.DataContext as LoginViewModel).ClearData();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new ChangePasswordWindow();
            w.ShowDialog();
        }

        private void ManageUserButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new Window()
            {
                Content = new ManageUserView()
            };
            w.Show();
        }

        private void AllViewButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new Window()
            {
                Content = new AllView()
            };
            w.Show();
        }
    }
}
