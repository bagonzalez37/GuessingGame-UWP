using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GuessGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {

            this.InitializeComponent();
        }
        
        
        //Generate a random number
        static Random rnd = new Random();
        int rndNumber = rnd.Next(1, 10);

        //Method to reset variables
        public void Reset()
        {
            rndNumber = rnd.Next(1, 10);
        }

        //Method to handle user input
        public async void Guess()
        {
            try
            {
                int userInput = int.Parse(txtGuess.Text);

                //Check if user input correct number
                if (userInput != rndNumber)
                {
                    if (userInput > rndNumber)
                    {

                        MessageDialog lower = new MessageDialog("The number is lower than " + userInput + ".");
                        lower.Commands.Clear();
                        lower.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                        var res = await lower.ShowAsync();
                        return;
                    }
                    else if (userInput < rndNumber)
                    {
                        MessageDialog higher = new MessageDialog("The number is higher than " + userInput + ".");
                        higher.Commands.Clear();
                        higher.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                        var res = await higher.ShowAsync();
                        return;
                    }

                }
                else
                {
                    //Prompt user to play again when they guess the number correctly
                    MessageDialog correct = new MessageDialog("You guessed correctly! The number was " + rndNumber + ". Play again?");
                    correct.Commands.Clear();
                    correct.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                    correct.Commands.Add(new UICommand { Label = "No", Id = 1 });
                    correct.DefaultCommandIndex = 0;
                    correct.DefaultCommandIndex = 1;
                    var res = await correct.ShowAsync();
                    if((int)res.Id == 0)
                    {
                        Reset();
                        txtGuess.Text = "";
                    }
                    else
                    {
                        
                        MessageDialog congrats = new MessageDialog("Congratulations!");
                        congrats.Commands.Clear();
                        congrats.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                        var resi = await congrats.ShowAsync();
                        return;
                    }
                  
                }
            }
            //Check for valid input from user
            catch (Exception)
            {
                MessageDialog incorrect = new MessageDialog("Your input was invalid. Please try again.");
                incorrect.Commands.Clear();
                incorrect.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                var res = await incorrect.ShowAsync();
                return;
            }

        }

        private void getValue_Click(object sender, RoutedEventArgs e)
        {
            // Call the Guess method to check user input when they click a button. 
            Guess();
        }
    }
}
