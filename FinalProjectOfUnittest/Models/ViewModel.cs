using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectOfUnittest.Models
{
    public class ViewModel
    {
        public SelectList selectList1 { get; set; }
        public Project project1 { get; set; }

        public List<string> stringList { get; set; }
        public List<string> stringList2 { get; set; }
        public Ticket ticket1 { get; set; }
        public ViewModel(SelectList s, Project p)
        {
            selectList1 = s;
            project1 = p;
        }

        public ViewModel(List<string> ls, List<string> ls2, Ticket t)
        {
            stringList = ls;
            stringList2 = ls2;
            ticket1 = t;
        }
    }
}
