using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Komponenty
{
    /// <summary>
    /// Logika interakcji dla klasy ZegarBokserski.xaml
    /// </summary>
    public partial class ZegarBokserski : UserControl
    {
        private int time;
        private int breakTime;
        private int rounds;
        private bool working = false;
        int i = 1;
        int bufor;
        private DispatcherTimer timer = new DispatcherTimer();
        //private DispatcherTimer timer2 = new DispatcherTimer();

       // SoundPlayer player = new SoundPlayer("/gongSound.wav");
        //SoundPlayer player1 = new SoundPlayer("/tickTockSound.wav");

        public ZegarBokserski()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            
            //timer2.Interval = TimeSpan.FromSeconds(1);
            //timer2.Tick += Timer2_Tick;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var isNumber = int.TryParse(timeInput.Text, out bufor);
            var isNumberRounds = int.TryParse(RoundInput.Text, out rounds);
            var isBreakNumber = int.TryParse(breakInput.Text, out breakTime);

            WhichRoundTB.Text = string.Format("{0} z {1}", i, rounds);
            breakTimeTB.Text = string.Format("{0}", breakTime);

            if (isNumber && isNumberRounds && isBreakNumber && rounds > 0 && bufor > 0 && breakTime >= 0)
            {
                if (!working)
                {
                    time = bufor;
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("Zegar działa!");
                }
            }
            else
            {
                TimerTB.Text = "Błąd";
            }
        }



        //private void Timer2_Tick(object sender, EventArgs e)
        //{
        //    int bufor2 = breakTime;
        //    if (breakTime > 0) {
        //        breakTime--;

        //        TimerTB.Text = string.Format("00:0{0}:{1}", breakTime / 60, breakTime % 60);
        //        if (breakTime <= 10)
        //        {
        //            SystemSounds.Beep.Play();
        //            if (time % 2 == 0) TimerTB.Foreground = Brushes.Red;
        //            else TimerTB.Foreground = Brushes.Black;
        //        }
        //        if (time % 60 < 10)
        //        {
        //            TimerTB.Text = string.Format("00:0{0}:0{1}", time / 60, time % 60);
        //        }
        //    }
        //    else
        //    {
        //        timer2.Stop();
        //        breakTime = bufor2;
        //        timer.Start();
        //    }
        //}


            private void Timer_Tick(object sender, EventArgs e)
        {
            

            if (time > 0)
            {
                if (time == bufor)
                    SystemSounds.Hand.Play();

                working = true;
                time--;
                TimerTB.Text = string.Format("00:0{0}:{1}", time / 60, time % 60);
                if (time <= 10)
                {
                    SystemSounds.Beep.Play();
                    if (time % 2 == 0) TimerTB.Foreground = Brushes.Red;
                    else TimerTB.Foreground = Brushes.Black;
                }
                if (time % 60 < 10)
                {
                    TimerTB.Text = string.Format("00:0{0}:0{1}", time / 60, time % 60);
                }


            }
            else
            {


                if (i < rounds)
                {
                    TimerTB.Foreground = Brushes.Black;
                    System.Threading.Thread.Sleep(breakTime * 1000);
                    i++;
                    WhichRoundTB.Text = string.Format("{0} z {1}", i, rounds);
                    time = bufor;
                    
                }
                else
                {
                    timer.Stop();
                    SystemSounds.Hand.Play();
                    working = false;
                }
            }




        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            working = false;
            TimerTB.Text = "00:00:00";
            WhichRoundTB.Text = "1 z 1";
            breakTimeTB.Text = "0";

            timeInput.Text = "0";
            breakInput.Text = "0";
            RoundInput.Text = "1";
            timer.Stop();
            //timer2.Stop();
        }
    }
}
