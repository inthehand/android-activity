//-----------------------------------------------------------------------
// <copyright file="ActivityLifecycleCallbacks.cs" company="In The Hand Ltd">
//   Copyright (c) 2023 In The Hand Ltd, All rights reserved.
//   This source code is licensed under the MIT License
// </copyright>
//-----------------------------------------------------------------------

using Android.App;
using Android.OS;
using System;

namespace InTheHand
{
    internal sealed class ActivityLifecycleCallbacks : Java.Lang.Object, Application.IActivityLifecycleCallbacks, IDisposable
    {
        private void SetCurrentActivityIfRequired(Activity activity)
        {
            if (AndroidActivity.CurrentActivity == null)
            {
                System.Diagnostics.Debug.WriteLine("Setting CurrentActivity");
                AndroidActivity.CurrentActivity = activity;
            }
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            System.Diagnostics.Debug.WriteLine("OnActivityCreated");
            SetCurrentActivityIfRequired(activity);
        }

        public void OnActivityDestroyed(Activity activity)
        {
            System.Diagnostics.Debug.WriteLine("OnActivityDestroyed");
        }

        public void OnActivityPaused(Activity activity)
        {
            System.Diagnostics.Debug.WriteLine("OnActivityPaused");
            SetCurrentActivityIfRequired(activity);
        }

        public void OnActivityResumed(Activity activity)
        {
            System.Diagnostics.Debug.WriteLine("OnActivityResumed");
            SetCurrentActivityIfRequired(activity);
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            System.Diagnostics.Debug.WriteLine("OnActivitySaveInstanceState");
        }

        public void OnActivityStarted(Activity activity)
        {
            System.Diagnostics.Debug.WriteLine("OnActivityStarted");
            SetCurrentActivityIfRequired(activity);
        }

        public void OnActivityStopped(Activity activity)
        {
            System.Diagnostics.Debug.WriteLine("OnActivityStopped");
        }

        protected override void Dispose(bool disposing)
        {
            ((Application)Application.Context).UnregisterActivityLifecycleCallbacks(this);
            base.Dispose(disposing);
        }
    }
}