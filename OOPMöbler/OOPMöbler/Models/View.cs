using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOPMöbler.Models
{
    public class ViewModel
    {
        // Möbellistan och Userdatan som vi sparar på Viewmodel som vi skickar med i varje ny action, se homecontroller
        public List<Möbel> MöbelList { get; set; }
        public UserData UserData { get; set; }
        public static ViewModel viewmodel(List<Möbel> möbellist, UserData userdata)
        {
            ViewModel VM = new ViewModel();
            VM.MöbelList = möbellist;
            VM.UserData = userdata;
            return VM;
        }
    }
}