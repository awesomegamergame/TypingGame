using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TypingGame
{
    public partial class MainWindow : Window
    {
        public static MainWindow gameWindow;
        public static Random random = new Random();
        public static string[] words = { "always", "American", "among", "amount", "analysis", "and", "animal", "another", "answer", "any", "anyone", "anything", "appear", "apply", "baby", "back", "bad", "bag", "ball", "bank", "bar", "base", "be", "beat", "beautiful", "because", "become", "bed", "before", "begin", "behavior", "camera", "campaign", "can", "cancer", "candidate", "capital", "car", "card", "care", "career", "carry", "case", "catch", "cause", "cell", "center", "central", "century", "certain", "certainly", "each", "early", "east", "easy", "eat", "economic", "economy", "edge", "education", "effect", "effort", "eight", "either", "election", "else", "employee", "face", "fact", "factor", "fail", "fall", "family", "far", "fast", "father", "fear", "federal", "feel", "feeling", "few", "field", "fight", "figure", "game", "garden", "gas", "general", "generation", "get", "girl", "give", "glass", "go", "goal", "good", "government", "hair", "half", "hand", "hang", "happen", "happy", "hard", "have", "he", "head", "health", "hear", "heart", "heat", "image", "imagine", "impact", "important", "improve", "in", "include", "including", "increase", "indeed", "indicate", "individual", "industry", "information", "inside", "instead", "institution", "image", "imagine", "impact", "important", "improve", "in", "include", "including", "increase", "indeed", "indicate", "individual", "industry", "information", "inside", "instead", "institution", "decide", "include" };
        public static int points = 0;
        public static Thread thread = new Thread(CounterThread);
        public static string currentWord = string.Empty;
        
        public MainWindow()
        {
            InitializeComponent();
            gameWindow = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            thread.Start();
        }

        public static void CounterThread()
        {
            currentWord = words[random.Next(0, words.Length)];
            gameWindow.Dispatcher.Invoke(() =>
            {
                gameWindow.wordBox.Content = currentWord;
            });
            int time = 10;
            while (time != -1)
            {
                TimeSpan timespan = TimeSpan.FromSeconds(time);
                gameWindow.Dispatcher.Invoke(() =>
                {
                    gameWindow.counter.Content = time;
                });
                Thread.Sleep(1000);
                time--;
            }
            points--;
            gameWindow.Dispatcher.Invoke(() =>
            {
                gameWindow.score.Content = points;
            });
            thread = new Thread(CounterThread);
            thread.Start();
        }

        private void type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                thread.Abort();
                if (type.Text.Equals(currentWord))
                {
                    points++;
                    score.Content = points;
                }
                else
                {
                    points--;
                    score.Content = points;
                }
                type.Text = null;
                thread = new Thread(CounterThread);
                thread.Start();
            }
        }
    }
}
