using huypq.SmtWpfClient;
using huypq.SmtWpfClient.Abstraction;
using huypq.SmtWpfClient.View;
using huypq.SmtWpfClient.ViewModel;
using System.Windows;
using Client.View;

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
                Token = "CfDJ8J0CZcxInRhFvSirfZanTFyE1hpyx7CXXn4481gOUW7vWFUUg48sSUDEChTx8riPcZwBfcTnO24j9_p8O9foxq5IJ-dZN3LPqtMQF1NKzS7uCt7QjedoTIPjky58FjaYdRIsBfoP1gnzgIUBV9c9x5OlnlJOtv3jCSm_9BUY03Ny"
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
