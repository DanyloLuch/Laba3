using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Laba3;

public partial class EditView : ContentPage
{
    public string FullName { get; set; }
    public string Faculty { get; set; }
    public string Department { get; set; }
    public string EducationLevel { get; set; }
    public string Institution { get; set; }
    public string Subject { get; set; }

    public EditView()
    {
        InitializeComponent();

        Schelude file = Schelude.GetInstance();

        file.FindIndex();

        if (file.Data.Count > 0 && file.index < file.Data.Count)
        {
            this.FullName = file.Data[file.index].FullName;
            this.Faculty = file.Data[file.index].Faculty;
            this.Department = file.Data[file.index].Department;
            this.EducationLevel = file.Data[file.index].EducationLevel;
            this.Institution = file.Data[file.index].Institution;
            this.Subject = file.Data[file.index].Subject;
        }

        BindingContext = this;
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Application.Current.CloseWindow(this.Window);
    }

    private void OkClicked(object sender, EventArgs e)
    {
        Schelude file = Schelude.GetInstance();
        file.EditLesson(FullName, Faculty, Department, EducationLevel, Institution, Subject);
        Application.Current.CloseWindow(this.Window);
    }
}
