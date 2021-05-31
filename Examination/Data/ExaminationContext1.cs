using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Data
{
    public class ExaminationContext1 : DbContext
    {
        public ExaminationContext1(DbContextOptions<ExaminationContext1> options)
        : base(options)
        { }
        public DbSet<Predmet> Predmets { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        
        public DbSet<Test> Tests { get; set; }
        public DbSet<AnsweredQuesttions> AnsweredQuesttions { get; set; }
        public DbSet<UserExams> UserExams { get; set; }
       



        
    }

}
