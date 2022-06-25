using ExamProjectThird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.ViewModel
{
    public class HomeViewModel
    {
        public List<IntroSlider> IntroSlider { get; set; }
        public List<WhyChoose> whyChooses { get; set; }
        public List<EventSlider> EventSlider { get; set; }
        public List<Cheef> Cheefs { get; set; }
    }
}
