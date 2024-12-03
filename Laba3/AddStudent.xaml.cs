using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Laba3
{
    public partial class AddView : ContentPage
    {
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public string EducationLevel { get; set; }
        public string Institution { get; set; }
        public string Subject { get; set; }

        public ObservableCollection<Student> Lessons { get; set; }

        public event EventHandler LessonAdded;

        public AddView()
        {
            InitializeComponent();
            BindingContext = this;
            Lessons = new ObservableCollection<Student>();
        }

        private void SubmitClicked(object sender, EventArgs e)
        {
            StudentManager file = StudentManager.GetInstance();
            file.AddLesson(FullName, Faculty, Department, EducationLevel, Institution, Subject);
            file.index = file.Data.Count - 1;
            LessonAdded?.Invoke(this, EventArgs.Empty);
            Application.Current.CloseWindow(this.Window);
        }
    }
}
