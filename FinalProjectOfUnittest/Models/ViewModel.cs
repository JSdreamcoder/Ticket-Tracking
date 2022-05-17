using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectOfUnittest.Models
{
    public class ViewModel
    {
        public SelectList selectList1 { get; set; }
        public Project project1 { get; set; }

        public ViewModel(SelectList s, Project p)
        {
            selectList1 = s;
            project1 = p;
        }
    }
}
