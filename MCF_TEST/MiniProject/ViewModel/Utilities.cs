using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class Utilities
    {
        public static List<ListValue> GetListRow()
        {
            return new List<ListValue>()
            {
                new ListValue(){Id = 1, Value="5"},
                new ListValue(){Id = 2, Value="10"},
                new ListValue(){Id = 3, Value="25"},
            };
        }

        public static List<ListValue> GetSortList()
        {
            return new List<ListValue>()
            {
                new ListValue(){Id=1, Value="Ascending"},
                new ListValue(){Id=2, Value="Descending"}
            };

        }
    }

    public class ListValue
    {
        public long Id { get; set; }
        public string Value { get; set; }
    }
}

