using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BussinessLayer
{
    internal class Programs
    {
        internal static int UpdatePrograms()
        {

            return Data.Programs.UpdatePrograms();
        }

    }

    internal class Courses
    {
        internal static int UpdateCourses()
        {
            DataTable dt = Data.Courses.GetCourses().GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string courseId = row["CId"].ToString();

                    if (string.IsNullOrEmpty(courseId) || !CourseExists(courseId))
                    {
                        FinalProject.Form1.msgBLL1();
                        Data.Courses.GetCourses().RejectChanges();
                        return -1;
                    }
                }
            }


            return Data.Courses.UpdateCourses();
        }

        private static bool CourseExists(string courseId)
        {
            DataTable coursesTable = Data.Courses.GetCourses();
            return coursesTable.AsEnumerable().Any(row => row.Field<string>("CId") == courseId);
        }

    }
    internal class Students
    {
        internal static int UpdateStudents()
        {
            DataTable dt = Data.Students.GetStudents().GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dt != null)
            {
                
                var invalidStudents = dt.AsEnumerable().Select(row => row["ProgId"].ToString()).Where(progId => string.IsNullOrEmpty(progId) || IsStudentAssociated(progId)).ToList();

                
                if (invalidStudents.Any())
                {
                    FinalProject.Form1.msgBLL2();
                    Data.Students.GetStudents().RejectChanges();
                    return -1;
                }
            }

            return Data.Students.UpdateStudents();
        }


        private static bool IsStudentAssociated(string progId)
        {
            DataTable studentsTable = Data.Students.GetStudents();

           
            return studentsTable.AsEnumerable().Any(studentRow => !string.IsNullOrEmpty(studentRow["ProgId"].ToString()) && studentRow["ProgId"].ToString() == progId);
        }

    }


    internal class Enrollment
    {
        internal static int UpdateEnrollment()
        {
            DataTable dt = Data.Enrollments.GetEnrollments()
                                          .GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["FinalGrade"] != DBNull.Value)
                    {
                        int finalGrade = Convert.ToInt32(row["FinalGrade"]);
                        if (finalGrade < 0 || finalGrade > 100)
                        {
                            FinalProject.Form1.msgBLL4();
                            Data.Enrollments.GetEnrollments().RejectChanges();
                            return -1;
                        }
                    }

                    if (row.RowState == DataRowState.Added && row["FinalGrade"] != DBNull.Value)
                    {
                        FinalProject.Form1.msgBLL5();
                        Data.Enrollments.GetEnrollments().RejectChanges();
                        return -1;
                    }

                    if (row["FinalGrade", DataRowVersion.Original] != DBNull.Value &&
                        row["FinalGrade"] == DBNull.Value)
                    {

                        Data.Enrollments.GetEnrollments().RejectChanges();
                        return -1;
                    }

                    if (row.RowState == DataRowState.Modified && row["FinalGrade", DataRowVersion.Original] != DBNull.Value &&
                        row["FinalGrade"] != DBNull.Value)
                    {
                        FinalProject.Form1.msgBLL7();
                        Data.Enrollments.GetEnrollments().RejectChanges();
                        return -1;
                    }

                    if (row.RowState == DataRowState.Deleted || (row.RowState == DataRowState.Modified && row["FinalGrade"] == DBNull.Value))
                    {
                        continue;
                    }
                    else
                    {

                        Data.Enrollments.GetEnrollments().RejectChanges();
                        return -1;
                    }
                }
            }

            return Data.Enrollments.UpdateEnrollments();
        }
    }
}
