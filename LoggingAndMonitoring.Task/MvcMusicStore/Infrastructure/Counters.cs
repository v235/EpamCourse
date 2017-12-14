using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerformanceCounterHelper;
using System.Diagnostics;

namespace MvcMusicStore.Infrastructure
{
    [PerformanceCounterCategory("MVC MusicStore",
        PerformanceCounterCategoryType.MultiInstance,
        "MVC MusicStore Counter")]
    public enum Counters
    {
        [PerformanceCounter("Success Login count",
            "Counts the number of successful login",
            PerformanceCounterType.NumberOfItems32)]
        SuccessLogin,

                    [PerformanceCounter("Success Logoff count",
            "Counts the number of successful logoff",
            PerformanceCounterType.NumberOfItems32)]
        SuccessLogoff,
                                [PerformanceCounter("Success AddsToCart count",
            "Counts the number of successful AddsToCart",
            PerformanceCounterType.NumberOfItems32)]
        AddsToCart
    }
}