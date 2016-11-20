using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace QuotesRotatorApp
{
    public partial class MainWindow : Window
    {
        private QuotesEngine engine;

        public MainWindow()
        {
            InitializeComponent();

            engine = new QuotesEngine(UpdateQuoteLabelText);
            
            engine.Start();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        private void UpdateQuoteLabelText(string text)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action<string>((x) =>
            {
                quotePresenter.Text = x;
            }), text);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MenuItemExit_OnClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MenuItemReloadQuotes_OnClick(object sender, RoutedEventArgs e)
        {
            engine.ReloadQuotes();
        }
    }
}