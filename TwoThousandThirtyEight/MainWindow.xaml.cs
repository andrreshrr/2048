//04.06.2020 Челлендж выполнен!!
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
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
using TwpThousanFirthyEight;

namespace TwoThousandThirtyEight
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Rectangle> rectangles;
        bool won;
        List<Label> texts;
        Many game;
        private void InitRectangles(List<Rectangle> rects)//инициализация списка прямоугольников 
        {           
            rects.Add(rect1);
            rects.Add(rect2);
            rects.Add(rect3);
            rects.Add(rect4);
            rects.Add(rect5);
            rects.Add(rect6);
            rects.Add(rect7);
            rects.Add(rect8);
            rects.Add(rect9);
            rects.Add(rect10);
            rects.Add(rect11);
            rects.Add(rect12);
            rects.Add(rect13);
            rects.Add(rect14);
            rects.Add(rect15);
            rects.Add(rect16);
        }
        private void InitTexts(List<Label> rects)//инициализация списка прямоугольников 
        {
            rects.Add(text1);
            rects.Add(text2);
            rects.Add(text3);
            rects.Add(text4);
            rects.Add(text5);
            rects.Add(text6);
            rects.Add(text7);
            rects.Add(text8);
            rects.Add(text9);
            rects.Add(text10);
            rects.Add(text11);
            rects.Add(text12);
            rects.Add(text13);
            rects.Add(text14);
            rects.Add(text15);
            rects.Add(text16);
        }
        private void UpdateField(Many game)
        {
            for (int i = 0; i < 16; i++)
            {
            
                if (game.all_ones[i].Value != -1)
                {
                    SolidColorBrush brush = new SolidColorBrush(Colors.IndianRed);
                    rectangles[i].Fill =brush;                   
                    texts[i].Content = game.all_ones[i].Value; 
                    if (game.all_ones[i].Value > 512)
                    {
                        texts[i].FontSize = 36;
                    }
                    else
                    {
                        texts[i].FontSize = 48;

                    }
                } else
                {
                    SolidColorBrush brush = new SolidColorBrush(Colors.DarkGray);
                    rectangles[i].Fill = brush;
                    texts[i].Content = "";
                }

            }
        }

        public MainWindow()
        {
            InitializeComponent();
            won = false;
            field.Visibility = Visibility.Hidden;
            rectangles = new List<Rectangle>();
            texts = new List<Label>();
            InitRectangles(rectangles);
            InitTexts(texts);
            retry.Visibility = Visibility.Hidden;
        }



        private void beginGame_Click(object sender, RoutedEventArgs e)
        {

        }
       
        private void beginPlay_Click(object sender, RoutedEventArgs e)
        {
            beginPlay.Visibility = Visibility.Hidden;
            title.Visibility = Visibility.Hidden;
            title1.Visibility = Visibility.Hidden;

            field.Visibility = Visibility.Visible;
            retry.Visibility = Visibility.Visible;
            game = new Many();            
            game.New(2);
            UpdateField(game);
            
            
        }

        private void field_KeyDown(object sender, KeyEventArgs e)
        {  
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (field.Visibility == Visibility.Visible)
            {
                switch (e.Key)
                {
                    case (Key.Up):
                        if (game.MovingUp())
                        {
                            game.New();                      
                        }
                        UpdateField(game);
                        break;
                    case (Key.Down):
                        if (game.MovingDown())
                        {
                            game.New();                           
                        }
                        UpdateField(game);
                        break;


                    case (Key.Left):
                        if (game.MovingLeft())
                        {
                            game.New();
                       
                        }
                        UpdateField(game);
                        break;

                    case (Key.Right):
                        if (game.MovingRight())
                        {
                            game.New();                           
                        }
                        UpdateField(game);
                        break;
                }
                if (game.HeIsAlreadyDead())
                {
                    MessageBox.Show("You lose! :c\nTry Again??", "OMG, hi, my name is rreshrr...", MessageBoxButton.OK);
                    Retry();                 
                }
                if (!won)
                {
                    if (game.HeIsAlreadyWon())
                    {
                        if (MessageBox.Show("You win!!\nContinue?", "Congratulations!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            won = true;
                        }
                        else
                        {
                            Retry();
                        }
                    }
                }

            }
        }


        private void Retry()
        {
            game.Clean();
            game = new Many();
            game.New(2);
            UpdateField(game);

        }
        private void retry_Click(object sender, RoutedEventArgs e)
        {
            Retry();

        }
    }
}
