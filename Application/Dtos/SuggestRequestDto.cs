using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class SuggestRequestDto
    {
        public string Text { get; set; }
        public string CurrentContent { get; set; }
    }

}
