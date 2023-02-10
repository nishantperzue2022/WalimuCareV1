using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Interfaces
{
    //https://docs.microsoft.com/en-us/answers/questions/170690/xamarin-forms-how-to-check-if-gps-is-on-or-off-in.html

    public interface ILocSettings
    {
        void OpenSettings();
        bool isGpsAvailable();
    }
}
