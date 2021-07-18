using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversity.Models.ViewModels
{
    public class IndexViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PageViewModel PageViewModel { get; set; }
        
        string searchData;
        public string SearchData 
        { 
            get
            {
                return searchData;

            }
            set
            {
                searchData = value;
                searching = !string.IsNullOrEmpty(searchData);
            }
        }

        bool searching = false;
        public bool Searching
        {
            get
            {
                return searching;
            }
        }
    }
}
