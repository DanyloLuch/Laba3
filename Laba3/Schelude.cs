using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Laba3
{
    internal class Schelude
    {
        private static Schelude _instance;

        public string FilePath { get; set; }
        public string FileContent { get; set; }

        public event Action LessonUpdated;

        public ObservableCollection<Student> Data { get; set; }
        public ObservableCollection<Student> CurrentBuffer { get; set; }
        public int index { get; set; }

        public delegate ObservableCollection<Student> SearchDelegate(string value);

        Dictionary<string, SearchDelegate> SearchHelper;
        private Schelude()
        {
            this.index = 0;
            this.Data = new ObservableCollection<Student>();
            SearchHelper = new Dictionary<string, SearchDelegate>();
            SearchHelper.Add("FullName", findByFullName);
            SearchHelper.Add("Faculty", findByFaculty);
            SearchHelper.Add("Subject", findBySubject);

        }

        public static Schelude GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Schelude();
            }
            return _instance;
        }
      
        public ObservableCollection<Student> Search(string searchCriterion, string searchText)
            {
            this.CurrentBuffer = this.Data;
            var resultLessons = SearchHelper[searchCriterion](searchText);
            this.Data = CurrentBuffer;
            return resultLessons;
            }

        public void UpdateDataToBuffer()
        {
            this.Data = this.CurrentBuffer;
        }

        public void AddLesson(string fullName, string faculty, string department, string educationLevel, string institution, string subject)
        {
            this.Data.Add(new Student(fullName, faculty, department, educationLevel, institution, subject));
        }

        public void EditLesson(string fullName, string faculty, string department, string educationLevel, string institution, string subject)
        {
            this.Data[this.index] = new Student(fullName, faculty, department, educationLevel, institution, subject);
            LessonUpdated?.Invoke();
        }



        public void DeleteArticle()
        {
            this.FindIndex();
            Debug.WriteLine(this.index);
            if (this.index >= 0 && this.index < this.Data.Count)
            {
                this.Data.Remove(this.Data[this.index]);
            }
        }

        public bool IsOpened()
        {
            return this.FilePath != null;
        }

        public void FindIndex()
        {
            for (int i = 0; i < this.Data.Count; i++)       
            {
                if (this.Data[i].IsSelected)
                {
                    this.index = i;
                    break;
                }
            }
        }

        private ObservableCollection<Student> findByFullName(string value)
        {
            var file = Schelude.GetInstance();
            var filteredData = new ObservableCollection<Student>(file.Data.Where(lesson => lesson.FullName.StartsWith(value, StringComparison.OrdinalIgnoreCase)).ToList());
            return filteredData;
        }

        private ObservableCollection<Student> findByFaculty(string value)
        {
            var file = Schelude.GetInstance();
            var filteredData = new ObservableCollection<Student>(file.Data.Where(lesson => lesson.Faculty.StartsWith(value, StringComparison.OrdinalIgnoreCase)).ToList());
            return filteredData;
        }

        private ObservableCollection<Student> findBySubject(string value)
        {
            var file = Schelude.GetInstance();
            var filteredData = new ObservableCollection<Student>(file.Data.Where(lesson => lesson.Subject.StartsWith(value, StringComparison.OrdinalIgnoreCase)).ToList());
            return filteredData;
        }


    }
}