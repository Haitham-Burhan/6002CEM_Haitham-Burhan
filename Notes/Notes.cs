using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Note.cs

namespace Notes
{
    public class Note
    {
        public string Subject { get; set; }
        public string Notes { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSelectionVisible { get; set; }

        public bool IsTop { get; set; }

        public DateTime date {  get; set; }
    }
}

