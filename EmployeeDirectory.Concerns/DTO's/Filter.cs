namespace EmployeeDirectory.Concerns
{
    public class Filter
    {
        public List<string> Alphabets { get; set; }
        public List<int> StatusIds { get; set; }
        public List<int> LocationIds { get; set; }
        public List<int> DepartmentIds { get; set; }
    }
}
