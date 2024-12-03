using Microsoft.Maui.Controls;
using System.Text.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;



namespace Laba3
{
    public partial class MainPage : ContentPage
    {
        private FilesManager fileManager;
        private Schelude fileObject;
        public ObservableCollection<Student> LessonList { get; set; }

        private bool mainPageLocker = false;

        public MainPage()
        {
            InitializeComponent();

            fileManager = new FilesManager();
            fileObject = Schelude.GetInstance();

            UpdateArticlesView();
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            if (mainPageLocker)
            {
                fileManager.SaveFile();
            }
        }

        private void OpenWindow(Page view)
        {
            Window editWindow = new Window(view);
            Application.Current.OpenWindow(editWindow);
        }

        private void EditClicked(object sender, EventArgs e)
        {
            UpdateArticlesView();
            if (mainPageLocker)
            {
                OpenWindow(new EditView());
                UpdateArticlesView();
            }
            else
            {
                DisplayAlert("Error", "Cant edit empty file", "OK");
            }
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            if (mainPageLocker)
            {
                fileObject.DeleteArticle();
                UpdateArticlesView();
            }
            else
            {
                DisplayAlert("Error", "No elements left", "OK");
            }
        }

        private void UpdateArticlesView()
        {
            this.BindingContext = null; // Очистити попередній BindingContext
            this.BindingContext = fileObject.Data; // Оновити даними з JSON
            if (fileObject.Data.Count > 0)
            {
                mainPageLocker = true;
            }
            else
            {
                mainPageLocker = false;
            }
        }

        private void AddClicked(object sender, EventArgs e)
        {
            if (mainPageLocker || this.fileObject.IsOpened())
            {
                OpenWindow(new AddView());
                UpdateArticlesView();
            }
            else
            {
                DisplayAlert("Error", "Open file first", "OK");
            }
        }

        private void AboutClicked(object sender, EventArgs e)
        {
            OpenWindow(new AboutView());
        }

        private async void OpenJsonFileClicked(object sender, EventArgs e)
        {
            try
            {
                var customJsonType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.iOS, new[] { "public.json" } },
            { DevicePlatform.Android, new[] { "application/json" } },
            { DevicePlatform.WinUI, new[] { ".json" } },
            { DevicePlatform.MacCatalyst, new[] { "public.json" } }
        });

                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a JSON file",
                    FileTypes = customJsonType
                });

                if (result != null)
                {
                    if (fileManager.OpenFile(result.FullPath))
                    {
                        await DisplayAlert("Success", "File opened successfully", "OK");
                        UpdateArticlesView();  // Оновлення даних на сторінці
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error reading file. Choose another one", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Cancelled", "File selection was cancelled", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Cannot open file: " + ex.Message, "OK");
            }
        }



        private void SearchBackClicked(object sender, EventArgs e)
        {
            fileObject.UpdateDataToBuffer();
            UpdateArticlesView();
        }

        private void SearchClicked(object sender, EventArgs e)
        {
            
            this.BindingContext = fileObject.Search(searchPicker.SelectedItem?.ToString(), searchEntry.Text);
        }
       
    }
}