using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace FileManager
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<FileInView> filesInView = new List<FileInView>();
        

        public MainPage()
        {
            this.InitializeComponent();

            foreach(var i in Directory.EnumerateFiles(@"C:\"))
            {
                filesInView.Add(new FileInView(i));
            }
            /*for (int i = 0; i < 50; i++)
                filesInView.Add(new FileInView("Файл", "File", "150G"));*/
            filesView.ItemsSource = filesInView;
        }
    }
}
