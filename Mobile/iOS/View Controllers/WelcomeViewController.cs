using System;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;
using BeerDrinkin.Service;
using Color = BeerDrinkin.Helpers.Colours;
using Splat;
using Strings = BeerDrinkin.Core.Helpers.Strings;
using System.Collections.Generic;
using Xamarin;
using BeerDrinkin.iOS.ViewControllers;

namespace BeerDrinkin.iOS
{
    partial class WelcomeViewController : UIViewController
    {
        public WelcomeViewController(IntPtr handle): base(handle)
        {
        }

        ITrackHandle trackerHandle;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            trackerHandle = Insights.TrackTime("Time spent on welcome screen");
            trackerHandle.Start();
           
            lblTitle.Text = Strings.WelcomeTitle;
            lblPromise.Text = Strings.WelcomePromise;
            btnFacebookConnect.SetTitle(Strings.WelcomeFacebookButton, UIControlState.Normal);
            View.BackgroundColor = Color.Blue.ToNative();


        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

           
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            btnFacebookConnect.Alpha = 0;
            lblTitle.Alpha = 0;
            imgLogo.Alpha = 0;
            lblPromise.Alpha = 0;

            if (BeerDrinkin.Core.Helpers.Settings.FirstRun == true)
            {
                BeerDrinkin.Core.Helpers.Settings.FirstRun = false;

                var tinderBeer = Storyboard.InstantiateViewController("welcomeMapView");
                PresentViewControllerAsync(tinderBeer, false);
            }

            UIView.Animate(0.4f, 0.3f, UIViewAnimationOptions.TransitionCurlUp,
                () =>
                {

                    btnFacebookConnect.Alpha = 1;
                    lblTitle.Alpha = 1;
                    imgLogo.Alpha = 1;
                    lblPromise.Alpha = 1;

                }, () =>
                {                    
                });    
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            if (trackerHandle != null)
            {
                trackerHandle.Stop();
                trackerHandle = null;
            }
        }

        async partial void BtnFacebookConnect_TouchUpInside(UIButton sender)
        {
            try
            {
                var user = await Client.Instance.BeerDrinkinClient.ServiceClient.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);

                var userService = new UserService();
                await userService.SaveUser(user);
                await Client.Instance.BeerDrinkinClient.RefreshAll();

                if(BeerDrinkin.Core.Helpers.Settings.UserTrackingEnabled)
                {
                    var account = BeerDrinkin.Client.Instance.BeerDrinkinClient.CurrentAccount;
                    var dateOfBirth = Convert.ToDateTime(account.DateOfBirth);
                    DateTime today = DateTime.Today;
                    int age = today.Year - dateOfBirth.Year;
                    string gender = account.IsMale ? "Male" : "Female";

                    var traits = new Dictionary<string, string> {
                        {Insights.Traits.Email, account.Email},
                        {Insights.Traits.FirstName, account.FirstName},
                        {Insights.Traits.LastName, account.LastName},
                        {Insights.Traits.Age, age.ToString()},
                        {Insights.Traits.Gender, gender},
                    };
                    Insights.Identify(account.Id, traits);
                }

                var vc = Storyboard.InstantiateViewController("tabBarController");
                await PresentViewControllerAsync(vc, false);

            }
            catch
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowError(Strings.WelcomeAuthError);
            }
        }
    }
}
