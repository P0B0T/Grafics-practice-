namespace Grafics.Classes
{
    public class Student
    {
        public string FullName { get; set; }

        public int CountWorksDone {  get; set; }

        public decimal Sum { get; set; }

        public Student(string fullName, int countWorkDone, decimal sum) 
        {
            FullName = fullName;
            CountWorksDone = countWorkDone;
            Sum = sum;
        }
    }
}
