﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace QuotesRotatorApp
{
    public partial class MainWindow : Window
    {
        private QuotesEngine engine;
        public ObservableCollection<QuotesGroup> QuoteGroups { get; private set; }
        public SwitchGroupCommand SwitchGroupCommand { get; private set; }
        
        public MainWindow()
        {
            engine = new QuotesEngine(UpdateQuoteLabelText);
            QuoteGroups = new ObservableCollection<QuotesGroup>();
            SwitchGroupCommand = new SwitchGroupCommand(engine);

            InitializeComponent();

            DataContext = this;
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

        private void GlobalContextMenu_OnOpened(object sender, RoutedEventArgs e)
        {
            QuoteGroups.Clear();

            foreach (var group in engine.AvailableGroups)
            {
                QuoteGroups.Add(group);
            }
        }
    }
}