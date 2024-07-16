using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50; //maximum of 50 rows per page
        public int PageNumber { get; set; } = 1; //if not set by the caller, default to 1
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize; //if not set by the caller, default to 10
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string? OrderBy { get; set; } // for sorting
    }
}
