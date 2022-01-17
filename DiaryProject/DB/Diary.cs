using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryProject.DB
{
    public class Diary
    {
        public int DiaryId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public User User { get; set; }
        
    }
}
