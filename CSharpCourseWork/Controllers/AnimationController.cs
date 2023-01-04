using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CSharpCourseWork.Controllers
{
    //Class that animates appearance and disappearance of elements
    internal class AnimationController
    {
        //Static class object of AnimationController
        private static AnimationController _animationController;

        //Private constructor
        private AnimationController() { }

        //Method that gives object of AnimationController
        public AnimationController GetInstance()
        {
            if (_animationController is null)
            {
                _animationController = new AnimationController();
            }

            return _animationController;
        }

        //Method that animates appearance of TextBlock
        private static void ShowToolTipTextBlock(TextBlock textBlockToShow, DependencyProperty opacityProperty, int animationTime)
        {
            DoubleAnimation opacityAnimationForTextBLock = new DoubleAnimation();
            opacityAnimationForTextBLock.From = 0;
            opacityAnimationForTextBLock.To = 1;

            opacityAnimationForTextBLock.Duration = TimeSpan.FromSeconds(animationTime);

            textBlockToShow.BeginAnimation(opacityProperty, opacityAnimationForTextBLock);
        }

        //Method that animates appearance of Image
        private static void ShowToolTipImage(Image imageToShow, DependencyProperty opacityProperty, int animationTime)
        {
            DoubleAnimation opacityAnimationForImage = new DoubleAnimation();
            opacityAnimationForImage.From = 0;
            opacityAnimationForImage.To = 1;

            opacityAnimationForImage.Duration = TimeSpan.FromSeconds(animationTime);

            imageToShow.BeginAnimation(opacityProperty, opacityAnimationForImage);
        }

        //Method that animates apppearance of TextBlock and Image
        public static void ShowToolTip(TextBlock textBlockToShow, Image imageToShow, DependencyProperty opacityProperty, string messageToShow, int animationTime)
        {
            textBlockToShow.Visibility = Visibility.Visible;
            textBlockToShow.Text = messageToShow;

            imageToShow.Visibility = Visibility.Visible;

            ShowToolTipTextBlock(textBlockToShow, opacityProperty, animationTime);
            ShowToolTipImage(imageToShow, opacityProperty, animationTime);
        }

        //Method that hides TextBlock and Image fast
        public static void HideToolTipFast(TextBlock textBlockToHide, Image imageToHide)
        {
            textBlockToHide.Visibility = Visibility.Hidden;
            textBlockToHide.Text = "";

            imageToHide.Visibility = Visibility.Hidden;
        }

        //Method that hides TextBlock slow
        private static void HideTextBlockSlow(TextBlock textBlockToHide, DependencyProperty opacityProperty, int animationTime)
        {
            DoubleAnimation opacityAnimationForTextBLock = new DoubleAnimation();
            opacityAnimationForTextBLock.From = 1;
            opacityAnimationForTextBLock.To = 0;

            opacityAnimationForTextBLock.Duration = TimeSpan.FromSeconds(animationTime);

            textBlockToHide.BeginAnimation(opacityProperty, opacityAnimationForTextBLock);
        }

        //Method that hides Image slow
        private static void HideImageSlow(Image imageToHide, DependencyProperty opacityProperty, int animationTime)
        {
            DoubleAnimation opacityAnimationForTImage = new DoubleAnimation();
            opacityAnimationForTImage.From = 1;
            opacityAnimationForTImage.To = 0;

            opacityAnimationForTImage.Duration = TimeSpan.FromSeconds(animationTime);

            imageToHide.BeginAnimation(opacityProperty, opacityAnimationForTImage);
        }

        //Method that hides TextBlock and Image slow
        public static void HideToolTipSlow(TextBlock textBlockToHide, Image imageToHide, DependencyProperty opacityProperty, int animationTime)
        {
            HideTextBlockSlow(textBlockToHide, opacityProperty, animationTime);
            HideImageSlow(imageToHide, opacityProperty, animationTime);
        }
    }
}
