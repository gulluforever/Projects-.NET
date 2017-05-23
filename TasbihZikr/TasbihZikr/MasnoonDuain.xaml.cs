using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Telerik.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TasbihZikr
{
    public partial class MasnoonDuain : PhoneApplicationPage
    {
        public MasnoonDuain()
        {
            InitializeComponent();
            
        }

        private async void brazil_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("اللَّهُمَّ أَنْتَ حَسَّنْتَ خَلْقِي فَحَسِّنْ خُلُقِي", "O Allah Azzawajal as you made my outward appearance good make my character good too.");
        }
        private async Task CallAfterTap(string dua,string trans)
        {
            StackPanel sp = new StackPanel();
            TextBlock duabox = new TextBlock();
            duabox.LineHeight = 75;
            duabox.FontFamily = new System.Windows.Media.FontFamily("/Assets/Fonts/trado.ttf#Traditional Arabic");
            duabox.FontSize = 50;
            duabox.Text = dua;
            TextBlock transbox = new TextBlock();
            transbox.Text = Environment.NewLine+ "Translation : "+ trans;
            transbox.FontSize = 20;
            sp.Children.Add(duabox);
            sp.Children.Add(transbox);
            await RadMessageBox.ShowAsync(sp, vibrate: false);
        }

        private async void afterFajrMaghrib_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("اللَّهُمَّ أَجِرْنِي مِنَ النَّارِ", "O Allah Azzawajal save me from the fire of Hell.");
        }

        private async void fearEnemy_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("اللَّهُمَّ إِنَّا نَجْعَلُكَ فِي نُحُورِهِمْ وَنَعُوذُ بِكَ مِنْ شُرُورِهِمْ", "O Allah, we make you the turner of the (enemies) chest (heart) and seek refuge in You from their evils");
        }

        private async void visitSick_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("لَا بَأْسَ طَهُورٌ إِنْ شَاءَ اللَّهُ", "This is not a thing of harm In Sha Allah Azzawajal this illness purifies from sins");
        }

        private async void duaAEating_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("الْحَمْدُ لِلَّهِ الَّذِي أَطْعَمَنَا وَسَقَانَا وَجَعَلَنَا مِنَ الْمُسْلِمِينَ", "Thanks to Allah Azzawajal who fed us and made us among Muslims");
        }

        private async void duaDressing_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("الْحَمْدُ لِلَّهِ الَّذِي كَسَانِي هَذَا وَرَزَقَنِيهِ مِنْ غَيْرِ حَوْلٍ مِنِّي وَلَا قُوَّةٍ", "All excellencies are for Allah Azzawajal Who gave me this cloth to wear and granted me without (using) any strenfth and Power.");
        }

        private async  void duaJourney_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("سُبْحَانَ الَّذِي سَخَّرَ لَنَا هَـٰذَا وَمَا كُنَّا لَهُ مُقْرِنِينَ وَإِنَّا إِلَىٰ رَبِّنَا لَمُنقَلِبُونَ", "Thanks to Allah Azzawajal. Pure is he who subdude this, other wise we could not make this obedient.");
        }

        private async void duaSneezing_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("الْحَمْدُ لِلَّهِ", "All praise onto Allah Almighty.");
        }

        private async void becomeAngry_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("أَعُوذُ بِاللَّهِ مِنَ الشَّيْطَانِ الرَّجِيمِ", "I seek the refuge of Allah Almighty from the rejected Shetan.");
        }

        private async  void duaeToilet_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("اللَّهُمَّ إِنِّي أَعُوذُ بِكَ مِنَ الْخُبْثِ وَالْخَبَائِثِ", "O Allah (Almighty) I seek your refuge from impure Jinnat (male and female)");
        }

        private async void dualToilet_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("الْحَمْدُ لِلَّهِ الَّذِي أَذْهَبَ عَنِّي الْأَذَى وَعَافَانِي", "Thanks to Allah Almighty Who removed pain from me and granted me relief.");
        }

        private async void duaSleeping_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("اللَّهُمَّ بِاسْمِكَ أَمُوتُ وَأَحْيَا", "O Allah (Almighty) I live and die in your name.");
        }

        private async void seeSneezing_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("يَرْحَمُكَ اللَّهُ", "May Allah Almighty Have Mercy on you.");
        }

        private async  void wakingUp_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("الْحَمْدُ لِلَّهِ الَّذِي أَحْيَانَا بَعْدَ مَا أَمَاتَنَا وَإِلَيْهِ النُّشُورُ", "All Praise onto Allah (Almighty) Who granted us life after death (Sleep) and we are return to him.");
        }

        private async void enterHome_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("اللَّهُمَّ إِنِّي أَسْأَلُكَ خَيْرَ الْمَوْلِجِ وَخَيْرَ الْمَخْرَجِ بِسْمِ اللَّهِ وَلِجْنَا وَعَلَى اللَّهِ رَبِّنَا تَوَكَّلْنَا", "O Allah Almighty I seek the goodness of coming in and going out, from you. Allah Almighty in the name of we come in and Allah Almighty is the name of we go out and we trusted Allah Almighty Who is our Lord.");
        }

        private async void leaveHome_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await CallAfterTap("بِسْمِ اللَّهِ تَوَكَّلْتُ عَلَى اللَّهِ وَلَا حَوْلَ وَلَا قُوَّةَ إِلَّا بِاللَّهِ", "In the name of Allah Almighty (I comeout of my house) I trust Allah Almighty, there is no capability of saving oneself from sins and neither is there capability to do good deeds but from Allah Almighty.");
        }

        

      

        
    }
}