using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace WPF_HW15
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            docViewer.ClearValue(FlowDocumentReader.DocumentProperty);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "Документ (*.xaml)|*.xaml", FileName = "Когорта.xaml" };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var range = new TextRange(docViewer.Document.ContentStart, docViewer.Document.ContentEnd);
                using (FileStream fs = File.Create(dlg.FileName))
                    range.Save(fs, DataFormats.XamlPackage);
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "Документ (*.xaml)|*.xaml" };
            if (dlg.ShowDialog() != true) return;

            try
            {
                var range = new TextRange(docViewer.Document.ContentStart, docViewer.Document.ContentEnd);
                using (FileStream fs = File.OpenRead(dlg.FileName))
                    range.Load(fs, DataFormats.XamlPackage);
            }
            catch
            {
                MessageBox.Show("Ошибка открытия");
            }
        }
    }
}
