using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectOfUnittest.Models
{
    public class ViewModel
    {
        public SelectList selectList1 { get; set; }
        public Project project1 { get; set; }
        public int noticeCount { get; set; }
        public List<string> fileNames { get; set; }
    
        public Ticket ticket { get; set; }
        public ViewModel(SelectList s, Project p)
        {
            selectList1 = s;
            project1 = p;
        }

        public ViewModel(List<string> ls, Ticket t)
        {
            fileNames = ls;
            
            ticket = t;
        }

        public ViewModel(int num)
        {
           noticeCount = num;
        }

    }
}
