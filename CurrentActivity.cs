//-----------------------------------------------------------------------
// <copyright file="CurrentActivity.cs" company="In The Hand Ltd">
//   Copyright (c) 2023 In The Hand Ltd, All rights reserved.
//   This source code is licensed under the MIT License
// </copyright>
//-----------------------------------------------------------------------

using Android.App;
using System;

namespace InTheHand
{
    /// <summary>
    /// Provides a central point to query the current Android activity.
    /// </summary>
    public static class AndroidActivity
    {
        static AndroidActivity()
        {
            // when used by a cross-platform UI framework like MAUI or Uno we need to get the current Activity in order to launch the picker UI
            // for a "native" app you can use the Android specific RequestDevice overload which accepts a Context

#if NET6_0_OR_GREATER
            // check for Uno without taking a hard dependency
            var t = Type.GetType("Uno.UI.ContextHelper, Uno, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null", false, true);
            if (t != null)
            {
                CurrentActivity = (Activity)t.GetProperty("Current", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null);
            }
            else
            {
                // try Maui Essentials if not
                t = Type.GetType("Microsoft.Maui.ApplicationModel.Platform, Microsoft.Maui.Essentials, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", false, true);
                if (t != null)
                {
                    CurrentActivity = (Activity)t.GetProperty("CurrentActivity", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null);
                }
            }
#else
            // check for Xamarin.Essentials without taking a hard dependency
            var t = Type.GetType("Xamarin.Essentials.Platform, Xamarin.Essentials, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", false, true);
            if (t != null)
            {
                CurrentActivity = (Activity)t.GetProperty("CurrentActivity", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null);
            }
#endif

            if (CurrentActivity == null)
                System.Diagnostics.Debug.WriteLine("CurrentActivity:Unknown");
            else
                System.Diagnostics.Debug.WriteLine($"CurrentActivity:{CurrentActivity.GetType().FullName}");
        }

        /// <summary>
        /// The current Android activity, or null if not determined.
        /// </summary>
        public static Activity CurrentActivity { get; set; }
    }
}