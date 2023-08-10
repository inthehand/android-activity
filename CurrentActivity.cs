//-----------------------------------------------------------------------
// <copyright file="CurrentActivity.cs" company="In The Hand Ltd">
//   Copyright (c) 2023 In The Hand Ltd, All rights reserved.
//   This source code is licensed under the MIT License
// </copyright>
//-----------------------------------------------------------------------

using Android.App;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace InTheHand
{
    /// <summary>
    /// Provides a central point to query the current Android activity.
    /// </summary>
    public static class AndroidActivity
    {
        private static ActivityLifecycleCallbacks _callbacks = new ActivityLifecycleCallbacks();

        static AndroidActivity()
        {
            ((Application)Application.Context).RegisterActivityLifecycleCallbacks(_callbacks);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Init()
        {
        }

        private static void TryGetActivity()
        {
            // when used by a cross-platform UI framework like MAUI or Uno we need to get the current Activity in order to launch the picker UI
            // for a "native" app you can use the Android specific RequestDevice overload which accepts a Context

            // This is a backup if Init wasn't passed an Activity or lifecycle callbacks didn't pickup the activity.

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
                try
                {
                    var task = (Task<Activity>)t.GetMethod("WaitForActivityAsync", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).Invoke(null, new object[] { null});
                    task.Wait();

                    CurrentActivity = (Activity)t.GetProperty("CurrentActivity", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null);
                }
                catch { }
            }
#endif

            if (currentActivity == null)
                System.Diagnostics.Debug.WriteLine("CurrentActivity:Unknown");
            else
                System.Diagnostics.Debug.WriteLine($"CurrentActivity:{CurrentActivity.GetType().FullName}");
        }

        private static Activity currentActivity;
        /// <summary>
        /// The current Android activity, or null if not set.
        /// </summary>
        /// <remarks>
        /// Set this property from your MainActivity OnCreate method to ensure that all libraries which depend on a reference to the activity will work as expected.
        /// </remarks>
        /// <value>The main activity for your application. This will be the MainActivity in your Xamarin/.NET application.</value>
        public static Activity CurrentActivity
        {
            get
            {
                if (currentActivity == null)
                    TryGetActivity();

                return currentActivity;
            }
            set
            {
                if (value != null && currentActivity == null)
                {
                    currentActivity = value;
                }
            }
        }
    }
}